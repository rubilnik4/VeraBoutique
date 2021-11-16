using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ResultFunctional.Models.Interfaces.Errors.RestErrors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Profiles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalPage : PersonalPageBase
    {
        public PersonalPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.RegisterPersonalViewModel, x => x.RegisterPersonalView.ViewModel).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.UpdateCommand, x => x.UpdateButton, x => x.RegisterPersonalViewModel).
                     DisposeWith(disposable);

                var restErrors = this.WhenAnyValue(x => x.ViewModel!.PersonalErrors).
                    WhereNotNull().
                    Where(result => result.HasError<IRestErrorResult>());

                restErrors.
                     Select(result => result.HasErrors).
                     BindTo(this, x => x.PersonalErrorLabel.IsVisible).
                     DisposeWith(disposable);

                restErrors.
                    Select(result => result.Errors.FirstOrDefault()?.Description ?? "Неверные личные данные").
                    BindTo(this, x => x.PersonalErrorLabel.Text).
                    DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected override BackLeftMenuView BackLeftMenuView =>
            this.BackLeftMenu;
    }
}