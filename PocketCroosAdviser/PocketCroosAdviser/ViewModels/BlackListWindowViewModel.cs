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
        public class BlackListWindowViewModel: ViewModelBase, IRoutableViewModel
        {

            public string UrlPathSegment => "/blackl";
        
            public BlackListWindowViewModel(PocketContext db, IScreen? screen = null)
            {
                _dbFilms = db;
                HostScreen = (screen ?? Locator.Current.GetService<IScreen>()) ?? throw new InvalidOperationException();
                BlFilms = new();
            }
        
            public IScreen HostScreen { get; }
            private static PocketContext _dbFilms = null!;
            private ObservableCollection<BlFilm> BlFilms { get; }



            public async Task LoadData()
            {
                try
                {
                    var data = await _dbFilms.BlFilms.ToArrayAsync();

                    BlFilms.AddRange(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            public async Task DeleteBlFilm(int id)
            {
                try
                {
                    await _dbFilms.BlFilms.Where(x => x.Id == id).ExecuteDeleteAsync();
                    await _dbFilms.SaveChangesAsync();
                    var filmToRemove = BlFilms.FirstOrDefault(f => f.Id == id);
                    if (filmToRemove != null)
                    {
                        BlFilms.Remove(filmToRemove);
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