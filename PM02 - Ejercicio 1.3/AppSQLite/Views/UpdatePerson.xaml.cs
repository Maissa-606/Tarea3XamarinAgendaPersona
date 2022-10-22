using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSQLite.Views;
using AppSQLite.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePerson : ContentPage
    {
        public UpdatePerson()
        {
            InitializeComponent();
        }

        private async void btnConfirmUpdate_Clicked(object sender, EventArgs e)
        {
            var personForUpdating = new Personas()
            {
                code = Convert.ToInt32(txtCode.Text),
                firstNames = txtFirstnames.Text,
                lastNames = txtLastnames.Text,
                age = Convert.ToInt32(txtAge.Text),
                address = txtAddress.Text,
                email = txtEmail.Text
            };

            var result = await App.BaseDatos.savePerson(personForUpdating);
            if(result == 1)
            {
                await DisplayAlert("Actualizar", "Actualizado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Actualizar", "No se pudo actualizar la información", "OK");
            }
            
        }

        private async void btnCancelUpdate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}