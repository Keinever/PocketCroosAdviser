using ReactiveUI;

namespace PocketCroosAdviser.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public virtual void OnInitialized()
    {
        throw new System.NotImplementedException();
    }
}