using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fitnessTraker.Model;
using Xamarin.Essentials;

namespace fitnessTraker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        Utilisateur utilisateur;
        public Profile(Utilisateur myutilisateur )
        {
            InitializeComponent();
            this.utilisateur = myutilisateur;
            this.BindingContext = this.utilisateur;

        }
       

       async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                loader.IsRunning = true;
                Console.WriteLine(email.Text);
                if (utilisateur.email == "")
                {
                    loader.IsRunning = false;
                    await DisplayAlert("Erreur", "Email est vide !", "OK");
                    return;
                }

                if (utilisateur.nom == "")
                {
                    loader.IsRunning = false;
                    await DisplayAlert("Erreur", "Nom est vide !", "OK");

                    return;
                }

                if (password.Text != utilisateur.password || password.Text == "" || rpassword.Text == "")
                {
                    loader.IsRunning = false;
                    await DisplayAlert("Erreur", "L'ensiene mot de passe et incorect ou sont vide !", "OK");
                    return;
                }

                //Modifier dans firbase
                this.utilisateur.password = rpassword.Text;
                await firebaseHelper.UpdateUtilisateur(this.utilisateur._id, this.utilisateur);
                loader.IsRunning = false;
                SecureStorage.Remove("utilisateur");
                var previousPage = Navigation.NavigationStack.LastOrDefault();
                await Navigation.PushAsync(new MainPage());
                (Application.Current).MainPage = new NavigationPage(new StartEcrant());
            }
            catch (Exception ex)
            {
                loader.IsRunning = false;
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

    }
}