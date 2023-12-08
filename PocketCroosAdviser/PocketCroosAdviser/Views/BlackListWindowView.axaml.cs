using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using PocketCroosAdviser.ViewModels;

namespace PocketCroosAdviser.Views;

public partial class BlackListWindowView : UserControl
{

    public BlackListWindowView()
    {
        InitializeComponent();
    }

    protected async override void OnInitialized()
    {
        var blackListWindowViewModel = DataContext as BlackListWindowViewModel;
        if (blackListWindowViewModel == null) throw new ArgumentNullException(nameof(blackListWindowViewModel));
        await LoadData(blackListWindowViewModel);
        base.OnInitialized();
    }

    private async Task LoadData(BlackListWindowViewModel blackListWindowViewModel)
    {
        await blackListWindowViewModel.LoadData().ContinueWith( t =>
        {
            if (t.IsFaulted) // Проверка на наличие исключения
            {
                Console.WriteLine("An exception occurred: " + t.Exception);

            }
            else
            {
                Console.WriteLine("Continuing after handling the exception");
            }
        });
    }

}