using System.Reactive;
using System.Runtime.Serialization;
using System.Windows.Input;
using PocketCroosAdviser.Data;
using ReactiveUI;

namespace PocketCroosAdviser.ViewModels;

[DataContract]
public class MainWindowViewModel: ReactiveObject, IScreen
{
    private readonly ReactiveCommand<Unit, Unit> _greeting;
    private readonly ReactiveCommand<Unit, Unit> _profile;
    private readonly ReactiveCommand<Unit, Unit> _marks;
    private readonly ReactiveCommand<Unit, Unit> _blackl;
    private readonly ReactiveCommand<Unit, Unit> _pref;
    private RoutingState _router = new RoutingState();
    
    
    private static PocketContext _db = new();
    
    public MainWindowViewModel()
    {
        _db.Database.EnsureCreated();
        _greeting = ReactiveCommand.Create(
            () => { Router.Navigate.Execute(new GreetingWindowViewModel(_db)); });
        _profile = ReactiveCommand.Create(
            () => { Router.Navigate.Execute(new ProfileWindowViewModel(_db)); });
        _marks = ReactiveCommand.Create(
            () => { Router.Navigate.Execute(new MarkersWindowViewModel(_db)); });
        _blackl = ReactiveCommand.Create(
            () => { Router.Navigate.Execute(new BlackListWindowViewModel(_db)); });
        _pref = ReactiveCommand.Create(
            () => { Router.Navigate.Execute(new PreferencesWindowViewModel(_db)); });
        
    }
    
    [DataMember]
    public RoutingState Router
    {
        get => _router;
        set => this.RaiseAndSetIfChanged(ref _router, value);
    }
    
    public ICommand Greeting => _greeting;
    public ICommand Profile => _profile;
    public ICommand Marks => _marks;
    public ICommand Blackl => _blackl;
    public ICommand Pref => _pref;
    
}