using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;

namespace Login.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, IActivatableViewModel, IValidatableViewModel
    {
        protected BaseViewModel()
        {
            Activator = new ViewModelActivator();
        }

        public ViewModelActivator Activator { get; }
        public ValidationContext ValidationContext => new ValidationContext();
    }
}
