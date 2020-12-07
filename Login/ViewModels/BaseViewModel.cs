using ReactiveUI;

namespace Login.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IActivatableViewModel
    {
        protected BaseViewModel()
        {
            Activator = new ViewModelActivator();
        }

        public ViewModelActivator Activator { get; }
    }
}
