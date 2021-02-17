using fitnessTraker.Model;
using Newtonsoft.Json;
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
    public partial class StartEcrant : ContentPage
    {
        public StartEcrant()
        {
            InitializeComponent();
            animation();
        }
        public async Task animation()
        {
            img.Opacity = 0;
            await img.FadeTo(1, 4000);
            var previousPage = Navigation.NavigationStack.LastOrDefault();
            string dataUtilisateur = await SecureStorage.GetAsync("utilisateur");
            Console.WriteLine("***************************");
            Console.WriteLine(dataUtilisateur);
            if (dataUtilisateur != null)
            {
                Utilisateur utilisateurCache = JsonConvert.DeserializeObject<Utilisateur>(dataUtilisateur);
                Console.WriteLine("cache " + utilisateurCache);
                await Navigation.PushAsync(new HomePage(utilisateurCache));
                Navigation.RemovePage(previousPage);
            }
            else
            {

            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(previousPage);
            }
           
        }
    }
}