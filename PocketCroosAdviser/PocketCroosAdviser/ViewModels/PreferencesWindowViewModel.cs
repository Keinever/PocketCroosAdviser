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

    public class PreferencesWindowViewModel : ViewModelBase, IRoutableViewModel
    {

        public string UrlPathSegment => "/pref";
        
        public PreferencesWindowViewModel(PocketContext db, IScreen? screen = null)
        {
            _dbGanres = db;
            HostScreen = (screen ?? Locator.Current.GetService<IScreen>()) ?? throw new InvalidOperationException();
            _ganres = new();
        }
        
        public async Task LoadData()
        {
            try
            {
                var data = await _dbGanres.Ganres.ToArrayAsync();

                Ganres.AddRange(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public IScreen HostScreen { get; }
        private static PocketContext _dbGanres = null!;
        
        ObservableCollection<Ganres> _ganres;

        public ObservableCollection<Ganres> Ganres
        {
            get => _ganres;
            set => this.RaiseAndSetIfChanged(ref _ganres, value);
        }

        public async Task PickedGanre(int id)
        {
            try
            {
                var ganr = Ganres.First(x => x.Id == id);
                var upGanres = new Ganres { Name = ganr.Name, Picked = !ganr.Picked, Id = id};
                Ganres.Replace(ganr, upGanres);
                var genreDb = await _dbGanres.Ganres.FirstAsync(x => x.Id == id);
                genreDb.Picked = !genreDb.Picked;
                await _dbGanres.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
    }
}