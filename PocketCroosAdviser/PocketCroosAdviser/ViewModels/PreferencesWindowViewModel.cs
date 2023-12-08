using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using PocketCroosAdviser.Data;
using PocketCroosAdviser.Models;
using ReactiveUI;

namespace PocketCroosAdviser.ViewModels
{

    public class PreferencesWindowViewModel : ViewModelBase
    {
        private static PocketContext _dbGanres = null!;
        public PreferencesWindowViewModel(PocketContext db)
        {
            _dbGanres = db;
            _ganres = new(_dbGanres.Ganres.Select(x => x).ToList());
        }
        
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