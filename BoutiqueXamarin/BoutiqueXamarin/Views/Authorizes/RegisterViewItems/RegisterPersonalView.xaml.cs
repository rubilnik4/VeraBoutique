using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ResultFunctional.Models.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Authorizes.RegisterViewItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPersonalView : RegisterPersonalBase
    {
        public RegisterPersonalView()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.Name, x => x.NameEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.NameEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.NameValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Surname, x => x.SurnameEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.SurnameEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.SurnameValid).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.Address, x => x.AddressEntry.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.AddressEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.AddressValid).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.PhoneEntry.Text).
                     Select(phone => PhoneCodeEntry.Text + phone).
                     BindTo(this, x => x.ViewModel!.Phone).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.PhoneValid, x => x.PhoneEntry.IsValid).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.PhoneEntry.IsValid).
                     BindTo(this, x => x.ViewModel!.PhoneValid).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterPersonalCommand).
                     WhereNotNull().
                     Select(result => result.HasErrors && !NameEntry.IsValid).
                     BindTo(this, x => x.NameEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterPersonalCommand).
                     WhereNotNull().
                     Select(result => result.HasErrors && !SurnameEntry.IsValid).
                     BindTo(this, x => x.SurnameEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterPersonalCommand).
                     WhereNotNull().
                     Select(result => result.HasErrors && !AddressEntry.IsValid).
                     BindTo(this, x => x.AddressEntry.HasError).
                     DisposeWith(disposable);

                this.WhenAnyObservable(x => x.ViewModel!.RegisterPersonalCommand).
                     WhereNotNull().
                     Select(result => result.HasErrors && !PhoneEntry.IsValid).
                     BindTo(this, x => x.PhoneEntry.HasError).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Изменение модели личных данных
        /// </summary>
        public IObservable<Unit> RegisterLoginObservable =>
            this.WhenAnyValue(x => x.NameEntry.Text, x => x.SurnameEntry.Text,
                              x => x.AddressEntry.Text, x => x.PhoneEntry.Text).
                 Select(_ => Unit.Default);
    }
}