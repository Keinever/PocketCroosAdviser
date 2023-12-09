using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PocketCroosAdviser.Data;
using PocketCroosAdviser.Models;
using ReactiveUI;
using Splat;

namespace PocketCroosAdviser.ViewModels
{

    public class ProfileWindowViewModel : ViewModelBase, IRoutableViewModel
    {
        private PocketContext _dbFilms;

        public string UrlPathSegment => "/profile";
        
        public ProfileWindowViewModel(PocketContext db, IScreen? screen = null)
        {
            _dbFilms = db;
            HostScreen = (screen ?? Locator.Current.GetService<IScreen>()) ?? throw new InvalidOperationException();
        }
        
        public IScreen HostScreen { get; }

        public async Task LoadData()
        {
            Film? film = await _dbFilms.Films.OrderBy(x => x.Id).LastOrDefaultAsync();
            if (film != null)
            {
                FilmName = film.Name;
                FilmPhoto = film.Photo;
                FilmDate = film.Date;
            }
            else
            {
                FilmName = "Закладок нет";
                FilmDate = string.Empty;
                FilmPhoto = "https://cs10.pikabu.ru/post_img/2020/07/16/9/1594910561159115420.jpg";

            }
        }

        private string? _url = string.Empty;

        private string? _name = string.Empty;
        private string? _data = string.Empty;
        

        public string? FilmPhoto
        {
            get => _url;
            set => this.RaiseAndSetIfChanged(ref _url, value);
        }

        public string? FilmName
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string? FilmDate
        {
            get => !string.IsNullOrEmpty(_url) ? _data : null;
            set => this.RaiseAndSetIfChanged(ref _data, value);
        }
    }
}