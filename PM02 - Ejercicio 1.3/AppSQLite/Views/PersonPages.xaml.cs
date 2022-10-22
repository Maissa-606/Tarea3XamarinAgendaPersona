using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonPages : ContentPage
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM02.db3");
        public PersonPages()
        {
            InitializeComponent();
        }

        private async void btnSavePerson_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbPath);

            var person = new Models.Personas
            {
                firstNames = txtFirstnames.Text,
                lastNames = txtLastnames.Text,
                age = Convert.ToInt32(txtAge.Text),
                address = txtAddress.Text,
                email = txtEmail.Text
            };

            // db.Insert(person);
            var result = await App.BaseDatos.savePerson(person);
            // DisplayAlert(null, person.firstNames + " " + person.lastNames + " guardado correctamente.", "OK");
            if(result == 1)
            {
                await DisplayAlert("Agregar", "Ingresado correctamente", "OK");
                var ListPersons = await App.BaseDatos.getListPersons();
            } 
            else
            {
                await DisplayAlert("Agregar", "No se pudo guardar la información", "OK");
            }
            // Navigation.PopAsync();
            clearFormPerson();
        }


        private void clearFormPerson()
        {
            this.txtFirstnames.Text = String.Empty;
            this.txtLastnames.Text = String.Empty;
            this.txtAge.Text = String.Empty;
            this.txtAddress.Text = String.Empty;
            this.txtEmail.Text = String.Empty;

        }
    }
}