using System;
using Avalonia.Controls;
using PocketCroosAdviser.ViewModels;

namespace PocketCroosAdviser.Views;

public partial class ProfileWindowView : UserControl
{
    public ProfileWindowView()
    {
        InitializeComponent();
    }
    protected async override void OnInitialized()
    {
        var blackListWindowViewModel = this.DataContext as ProfileWindowViewModel;
        if (blackListWindowViewModel == null) throw new ArgumentNullException(nameof(blackListWindowViewModel));
        await blackListWindowViewModel.LoadData();
        base.OnInitialized();
    }
}