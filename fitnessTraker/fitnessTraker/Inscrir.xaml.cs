using fitnessTraker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fitnessTraker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inscrir : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public Inscrir()
        {
            InitializeComponent();
            /*    this.BindingContext = this;
                  this.IsBusy = false;*/
            sex.SelectedIndex = 0;
        }

        private async void InscrirTap(object sender, EventArgs e)
        {
            try
            {
                loader.IsRunning = true;
                Console.WriteLine(email.Text);
                if (email.Text == "")
                {
                    loader.IsRunning = false;
                    await DisplayAlert("Erreur", "Email est vide !", "OK");
                    return;
                }

                if (nom.Text == "")
                {
                    loader.IsRunning = false;
                    await DisplayAlert("Erreur", "Nom est vide !", "OK");

                    return;
                }

                if (password.Text != rpassword.Text || password.Text == "" || rpassword.Text == "")
                {
                    loader.IsRunning = false;
                    await DisplayAlert("Erreur", "Les mots de passe sont pas identique ou sont vide !", "OK");
                    return;
                }
                if (sex.Items[sex.SelectedIndex] == "")
                {
                    loader.IsRunning = false;
                    await DisplayAlert("Erreur", "Sex est vide !", "OK");
                    return;
                }
                //ajouter dans firbase
                string id = Guid.NewGuid().ToString("N");
                await firebaseHelper.AddUtilisateur(new Utilisateur { _id=id,nom = nom.Text, email = email.Text, password = password.Text, date = DateTime.Now.ToString(), sex = sex.Items[sex.SelectedIndex],topDistance="0"});
                loader.IsRunning = false;
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                loader.IsRunning = false;
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
            }
    }
}