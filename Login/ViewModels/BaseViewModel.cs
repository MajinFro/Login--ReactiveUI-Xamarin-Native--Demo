using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Helpers;

namespace Login.ViewModels
{
    public abstract class BaseViewModel : ReactiveValidationObject, IActivatableViewModel
    {
        protected BaseViewModel()
        {
            Activator = new ViewModelActivator();
        }

        public ViewModelActivator Activator { get; }
    }
}
