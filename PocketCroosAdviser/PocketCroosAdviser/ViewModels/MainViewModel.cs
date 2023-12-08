using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using PocketCroosAdviser.Data;
using ReactiveUI;

namespace PocketCroosAdviser.ViewModels;

public class MainViewModel: ViewModelBase
{
    private static ViewModelBase? _content;
    private static PocketContext _db = new();
    
    public MainViewModel()
    {
        _db.Database.EnsureCreated();
        Content = new  ProfileWindowViewModel(_db);
    }
    
    public ViewModelBase? Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }
    
    public void OpenProfile()
    {
        Console.WriteLine("hahahahahahahahahahahahahahhaha");
        Content = new ProfileWindowViewModel(_db);
    }
    
    public void OpenGreeting()
    {
        Content = new GreetingWindowViewModel(_db);
    }

    public void OpenPreferences()
    {
        Console.WriteLine("NONONOJONONONO");
        Content = new PreferencesWindowViewModel(_db);
    }
    
    public void OpenMarkers()
    {
        Content = new MarkersWindowViewModel(_db);
    }
    
    public void OpenBlackList()
    {
        Content = new BlackListWindowViewModel(_db);
    }
    
}