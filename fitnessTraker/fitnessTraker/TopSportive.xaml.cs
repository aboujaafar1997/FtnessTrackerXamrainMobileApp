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
    public partial class TopSportive : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public TopSportive()
        {
            InitializeComponent();
        }
        async protected override void OnAppearing()
        {
            base.OnAppearing();
            List<Utilisateur> data = await firebaseHelper.GetAllUtilisateurs();
            ListViewTop.ItemsSource = data.OrderBy(o => o.topDistance).ToList();
        }
    }
}