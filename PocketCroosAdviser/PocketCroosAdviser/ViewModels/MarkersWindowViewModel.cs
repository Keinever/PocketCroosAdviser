using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PocketCroosAdviser.Data;
using PocketCroosAdviser.Models;
using ReactiveUI;

namespace PocketCroosAdviser.ViewModels
{

    public class MarkersWindowViewModel: ViewModelBase
    {
        private static PocketContext _dbFilms = null!;

        public MarkersWindowViewModel(PocketContext db)
        {
            _dbFilms = db;
            _films = new(_dbFilms.Films.Select(x => x).ToList()!);
        }
            
        private  ObservableCollection<Film> _films;
        public ObservableCollection<Film> Films
        {
            get => _films;
            set => this.RaiseAndSetIfChanged(ref _films, value);
        }

        public async Task DeleteMarker(int id)
        {
            try
            {
                await _dbFilms.Films.Where(x => x != null && x.Id == id).ExecuteDeleteAsync();
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