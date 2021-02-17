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
    public partial class Historique : ContentPage
    {
        FirebaseHelperSession firebaseHelper = new FirebaseHelperSession();
        Utilisateur utilisateur;
        public Historique(Utilisateur utilisateur0)
        {
            InitializeComponent();
            utilisateur = utilisateur0;
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            List<MaSession> data= await firebaseHelper.GetSessions(utilisateur._id);
            ListViewSession.ItemsSource = data;
        }

        async private void ListViewSession_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            MaSession maSession= (MaSession)lv.SelectedItem;
            Console.WriteLine(maSession._id);
            await Navigation.PushAsync(new Resultat(utilisateur,maSession,false))  ;
        }
  
    }
}