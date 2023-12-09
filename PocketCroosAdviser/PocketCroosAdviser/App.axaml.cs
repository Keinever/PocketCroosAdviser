using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PocketCroosAdviser.ViewModels;
using PocketCroosAdviser.Views;
using ReactiveUI;
using Splat;

namespace PocketCroosAdviser;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Locator.CurrentMutable.RegisterConstant<IScreen>(new MainViewModel());
            Locator.CurrentMutable.Register<IViewFor<GreetingWindowViewModel>>(() => new GreetingWindowView());
            Locator.CurrentMutable.Register<IViewFor<ProfileWindowViewModel>>(() => new ProfileWindowView());
            Locator.CurrentMutable.Register<IViewFor<MarkersWindowViewModel>>(() => new MarkersWindowView());
            Locator.CurrentMutable.Register<IViewFor<BlackListWindowViewModel>>(() => new BlackListWindowView());
            Locator.CurrentMutable.Register<IViewFor<PreferencesWindowViewModel>>(() => new PreferencesWindowView());
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            Locator.CurrentMutable.RegisterConstant<IScreen>(new MainViewModel());
            Locator.CurrentMutable.Register<IViewFor<GreetingWindowViewModel>>(() => new GreetingWindowView());
            Locator.CurrentMutable.Register<IViewFor<ProfileWindowViewModel>>(() => new ProfileWindowView());
            Locator.CurrentMutable.Register<IViewFor<MarkersWindowViewModel>>(() => new MarkersWindowView());
            Locator.CurrentMutable.Register<IViewFor<BlackListWindowViewModel>>(() => new BlackListWindowView());
            Locator.CurrentMutable.Register<IViewFor<PreferencesWindowViewModel>>(() => new PreferencesWindowView());
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}