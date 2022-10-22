using System;
using AppSQLite.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSQLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class ShowPersons : ContentPage
    {
      
        private Personas temporalPerson = new Personas();
      
        public ShowPersons()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            chargeListView();
        }
        private async void chargeListView()
        {

            var ListViewPersons = await App.BaseDatos.getListPersons();
           if(ListViewPersons!=null)
            {
                listPersons.ItemsSource = ListViewPersons;
            }
        }

        private async void listPersons_ItemSelected(object sender, SelectedItemChangedEventArgs eventListener)
        {
            var itemSelected = (Personas) eventListener.SelectedItem;

            btnUpdatePerson.IsVisible = true;
            btnDeletePerson.IsVisible = true;

         
            var idPersonSelected = itemSelected.code;

     
            if (idPersonSelected != 0) 
            {
                  var personObtained = await App.BaseDatos.getPersonByCode(idPersonSelected);
                if(personObtained != null)
                {
                    
                     temporalPerson.code = personObtained.code;
                    temporalPerson.firstNames = personObtained.firstNames;
                    temporalPerson.lastNames = personObtained.lastNames;
                    temporalPerson.age = personObtained.age;
                    temporalPerson.address = personObtained.address;
                    temporalPerson.email = personObtained.email;
            
                }
            }

        }

        private async void btnUpdatePerson_Clicked(object sender, EventArgs e)
        {
             var secondPage = new UpdatePerson();

            secondPage.BindingContext = temporalPerson;

            btnUpdatePerson.IsVisible = false;
            btnDeletePerson.IsVisible = false;
            await Navigation.PushAsync(secondPage);
            
            
        }

        private async void btnDeletePerson_Clicked(object sender, EventArgs e)
        {
           
            var personObtained = await App.BaseDatos.getPersonByCode(temporalPerson.code);

            if(personObtained != null)
            {
              
                await App.BaseDatos.deletePerson(personObtained);

                await DisplayAlert("Eliminar", "El registro se elimino correctamente", "OK");

               btnUpdatePerson.IsVisible = false;
                btnDeletePerson.IsVisible = false;
                chargeListView();
            }
            
        }
    }
}