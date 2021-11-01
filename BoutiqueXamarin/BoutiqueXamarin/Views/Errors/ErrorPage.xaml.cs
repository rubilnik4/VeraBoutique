using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueXamarin.ViewModels.Errors;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ReactiveUI;
using ReactiveUI.XamForms;
using ResultFunctional.Models.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Errors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorPage : ErrorPageBase
    {
        public ErrorPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.Error, x => x.ErrorLabel.Text, error => error.Description).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.ReloadCommand, x => x.ReloadButton).
                     DisposeWith(disposable);
            });
        }
    }
}