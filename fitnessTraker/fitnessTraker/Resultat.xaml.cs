using fitnessTraker.Helper;
using fitnessTraker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fitnessTraker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resultat : ContentPage
    {
        FirebaseHelperSession helperSession = new FirebaseHelperSession();
        FirebaseHelper utilisateurHelper = new FirebaseHelper();
        Utilisateur utilisateur1;
        MaSession maSession1;
        public Resultat(Utilisateur utilisateur,MaSession maSession,bool firstTime )
        {

            InitializeComponent();
            frameEnregistre.IsVisible = firstTime;
            this.BindingContext = maSession;
            utilisateur1 = utilisateur;
            bravo.Text = "Bravo " + utilisateur.nom;
            if (maSession.Pas > 0)
            {
            klorie.Text = maSession.Pas * 0.04 + "(kcal)";
            }
            else
            {
                klorie.Text = maSession.Distance * 0.4 + "(kcal)";
            }
            maSession1 = maSession;

        }

       async private void enregitrer_Tapped(object sender, EventArgs e)
        {
            try
            {
             loader.IsRunning = true;
    
                if (maSession1.Distance > double.Parse(utilisateur1.topDistance))
                {
                    utilisateur1.topDistance = maSession1.Distance.ToString();
                    await utilisateurHelper.UpdateUtilisateur(utilisateur1._id, utilisateur1);
                }

                // get mon adresse
                var placemarks = await Geocoding.GetPlacemarksAsync(maSession1.startPosition.Latitude, maSession1.startPosition.Longitude);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    string add = placemark.Locality + " " + placemark.AdminArea + "," + placemark.CountryCode;
                    maSession1.adresse = add;
                }
                else
                {
                    maSession1.adresse = "N/A votre location et inconu";
                }

            await helperSession.AddSession(maSession1);
            loader.IsRunning = false;
            await DisplayAlert("Cool","Bien enregistré", "OK");
            await Navigation.PopAsync();
            }
            catch (Exception ex)
            {

                loader.IsRunning = false;
                await DisplayAlert("Erreur", ex.Message, "OK");

            }
        
        }
    }
}