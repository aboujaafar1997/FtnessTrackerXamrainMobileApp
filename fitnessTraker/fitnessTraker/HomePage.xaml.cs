using fitnessTraker.Helper;
using fitnessTraker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace fitnessTraker
{
    public partial class HomePage : ContentPage
    {
        Utilisateur utilisateur;
        public HomePage(Utilisateur utilisateur)
        {
            InitializeComponent();
            this.utilisateur = utilisateur;

        }

        private void make_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile(utilisateur));

        }

        private void tracker_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Tracker(utilisateur));

        }

        private void historique_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Historique(utilisateur));

        }

        private void TopSportive_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TopSportive());

        }

        async private void déconnecté_Tapped(object sender, EventArgs e)
        {
            SecureStorage.Remove("utilisateur");
            var previousPage = Navigation.NavigationStack.LastOrDefault();
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(previousPage);

        }

        private async void messtatistique_Tapped(object sender, EventArgs e)
        {
            FirebaseHelperSession firebaseHelperSession = new FirebaseHelperSession();
            List<MaSession> data = await firebaseHelperSession.GetSessions(utilisateur._id);
            await Navigation.PushAsync(new statistique(utilisateur,data.OrderByDescending(c => c.date).Take(7).OrderBy(c => c.date).ToList()));
        }
    }
}
