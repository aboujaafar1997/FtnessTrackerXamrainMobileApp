using fitnessTraker.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fitnessTraker
{
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public MainPage()
        {
            InitializeComponent();
        }
       
        async void LoginClick(System.Object sender, System.EventArgs e)
        {
            loader.IsRunning = true;
            
            if (email.Text=="" || password.Text == "") { 

                loader.IsRunning = false;
                await DisplayAlert("Erreur", "Email ou mot de passe est vide !", "OK");
            }

            Utilisateur utilisateur = await firebaseHelper.GetUtilisateur(email.Text);
            if (utilisateur == null)
            {
                loader.IsRunning = false;
                await DisplayAlert("Erreur", "Erreur de email ou mot de passe !", "OK");
                return;
            }
            if(utilisateur.email==email.Text && utilisateur.password == password.Text)
            {
                loader.IsRunning = false;
                var data = JsonConvert.SerializeObject(utilisateur);
                Console.WriteLine("save in cache ....");
                Console.WriteLine(data);
                await SecureStorage.SetAsync("utilisateur", data);
                var previousPage = Navigation.NavigationStack.LastOrDefault();
                await Navigation.PushAsync(new HomePage(utilisateur));
                Navigation.RemovePage(previousPage);
            }
            else
            {
                loader.IsRunning = false;
                await DisplayAlert("Erreur", "Erreur de email ou mot de passe !", "OK");
            }
        }

        private void Inscrir_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Inscrir());

        }

        private void Inscrir_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Inscrir());

        }
    }
}
