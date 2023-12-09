using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using PocketCroosAdviser.Data;
using PocketCroosAdviser.Models;
using ReactiveUI;
using Splat;

namespace PocketCroosAdviser.ViewModels
{

    public class MarkersWindowViewModel: ViewModelBase, IRoutableViewModel
    {

        public string UrlPathSegment => "/marks";
        
        public MarkersWindowViewModel(PocketContext db, IScreen? screen = null)
        {
            _dbFilms = db;
            HostScreen = (screen ?? Locator.Current.GetService<IScreen>()) ?? throw new InvalidOperationException();
            _films = new();
        }
        
        public IScreen HostScreen { get; }
        
        private static PocketContext _dbFilms = null!;
            
        private  ObservableCollection<Film> _films;
        public ObservableCollection<Film> Films
        {
            get => _films;
            set => this.RaiseAndSetIfChanged(ref _films, value);
        }
        
        public async Task LoadData()
        {
            try
            {
                var data = await _dbFilms.Films.ToArrayAsync();

                Films.AddRange(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteMarker(int id)
        {
            try
            {
                await _dbFilms.Films.Where(x => x.Id == id).ExecuteDeleteAsync();
                await _dbFilms.SaveChangesAsync();
                var filmToRemove = Films.FirstOrDefault(f => f.Id == id);
                if (filmToRemove != null)
                {
                    Films.Remove(filmToRemove);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}