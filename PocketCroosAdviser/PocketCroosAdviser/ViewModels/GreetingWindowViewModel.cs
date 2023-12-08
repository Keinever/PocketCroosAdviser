using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PocketCroosAdviser.Data;
using PocketCroosAdviser.Models;
using ReactiveUI;

namespace PocketCroosAdviser.ViewModels
{
    public class GreetingWindowViewModel: ViewModelBase
    {
        public GreetingWindowViewModel(PocketContext db)
        {
            _dbFilms = db;
            HideFilm();
        }
        
        public async Task HideFilm()
        {
            Func<Task> asyncAction = async () =>
            {
                try { await GetAnsync(); }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception handled: " + ex.Message);
                    FilmName = "Произошла ошибка...";
                    FilmDescription = string.Empty;
                    FilmDate = string.Empty;
                    FilmCountry = string.Empty;
                    FilmPhoto = string.Empty;
                    FilmRaiting = string.Empty;
                    await HideFilm();
                }
            };
            await Task.Run(asyncAction).ContinueWith( _ =>
                {
                    Console.WriteLine("Continuing after handling the exception");
                }
            );
        }

        private string? _url = string.Empty;
        private static string? _name = "Идёт поиск фильма...";
        private static string? _country = string.Empty;
        private static string? _date = string.Empty;
        private string? _raiting = string.Empty;
        private string? _description = string.Empty;
        private static PocketContext _dbFilms = null!;
        

        public string? FilmPhoto
        {
            get => _url != "" ? _url : null;
            set => this.RaiseAndSetIfChanged(ref _url, value);
        }
        public string? FilmDescription
        {
            get => _description != "" ? "Описание:" + _description : null;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public string? FilmName
        {
            get => _name != "" ? _name : null;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        
        public string? FilmCountry
        {
            get => _country != "" ? "Страна:" + _country : null;
            set => this.RaiseAndSetIfChanged(ref _country, value);
        }
        
        public string? FilmDate
        {
            get => _date != "" ? "Год выхода:" + _date : null;
            set => this.RaiseAndSetIfChanged(ref _date, value);
        }
        
        public string? FilmRaiting
        {
            get => _raiting != "" ? "Рейтинг:" + _raiting : null;
            set => this.RaiseAndSetIfChanged(ref _raiting, value);
        }

        public async Task Change_Film()
        {
            await HideFilm();
        }

        public async Task SaveFilm()
        {
            Film filmMy = new Film { Name = _name, Photo =_url, Description = _description, Raiting = _raiting, Country = _country, Date = _date};
            var myFilms = _dbFilms.Films.Where(x => x != null && x.Name == filmMy.Name).Select(x => x).ToList();
            if (!myFilms.Any())
            {
                await _dbFilms.Films.AddAsync(filmMy);
                await _dbFilms.SaveChangesAsync();
            }
            await HideFilm();
        }
        
        public async Task AddInBl()
        {
            BlFilm filmBl = new BlFilm { Name = _name, Photo =_url, Description = _description, Raiting = _raiting, Country = _country, Date = _date};
            var blFilms = _dbFilms.BlFilms.Where(x => x.Name == filmBl.Name).Select(x => x).ToList();
            if (!blFilms.Any() )
            {
                await _dbFilms.BlFilms.AddAsync(filmBl);
                await _dbFilms.SaveChangesAsync();
            }
            await HideFilm();
        }

        //move out
        public const string ApiUrl = "https://api.kinopoisk.dev/v1.4/movie?{0}page={1}";
        private readonly Random _rnd = new();
        public async Task GetAnsync()
        {
            var ganre = _dbFilms.Ganres.Where(x => x.Picked == true).OrderBy(r => EF.Functions.Random()).Select(x => x.Name).Take(1).FirstOrDefaultAsync();
            FilmName = "Идёт поиск фильма...";
            FilmDescription = string.Empty;
            FilmDate = string.Empty;
            FilmCountry = string.Empty;
            FilmPhoto = string.Empty;
            FilmRaiting = string.Empty;
            using var client = new HttpClient();
            int page = _rnd.Next(1, 100);
            string? ganreResult = ganre.Result;
            string ganreForApi;
            if (ganreResult != null)
            {
                ganreForApi = $"genres.name={ganreResult}&";
            }
            else
            {
                ganreForApi = string.Empty;
            }
            client.DefaultRequestHeaders.Add("X-API-KEY", ConfigurationManager.AppSettings["X-API-KEY"]);
            Console.WriteLine(String.Format(ApiUrl,ganreForApi, page));
            HttpResponseMessage response = await client.GetAsync(String.Format(ApiUrl,ganreForApi, page));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(responseBody);
            if (data == null)
            {
                FilmName = "Произошла ошибка...";
                FilmDescription = string.Empty;
                FilmDate = string.Empty;
                FilmCountry = string.Empty;
            
                FilmPhoto = string.Empty;
                FilmRaiting = string.Empty;
                await HideFilm();
                return;
            }
            var movie = data.docs[0];
            FilmName = movie.name;
            FilmDescription = movie.description;
            FilmDate = movie.year;
            FilmCountry = movie.countries[0].name;
            FilmPhoto = movie.backdrop.previewUrl;
            FilmRaiting = movie.rating.kp;
            Console.WriteLine(FilmPhoto);
        }
        
    }
}