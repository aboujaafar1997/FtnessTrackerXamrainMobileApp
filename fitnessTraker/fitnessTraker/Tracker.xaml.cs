using fitnessTraker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace fitnessTraker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tracker : ContentPage
    {
        Polyline polyline = new Polyline { StrokeColor = Color.Blue,StrokeWidth = 12 };
        CancellationTokenSource cts;
        int hour = 0, min = 0, sec = 0;
        Position old = new Position(0, 0);
        Position premierePosition ;
        MaSession masession;
        Utilisateur utilisateur1;
        bool starting = true;

        public Tracker(Utilisateur utilisateur)
        {
            InitializeComponent();
            masession = new MaSession();
            utilisateur1 = utilisateur;
            masession._id = utilisateur1._id;
            this.BindingContext = masession;
            initMonMap();
            DisplayLocation();
        }



        public async void initMonMap()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Position p = new Position(location.Latitude, location.Longitude);
                    masession.startPosition = new Location(location.Latitude, location.Longitude);
                    MapSpan mapspan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(.444));
                    mylocalmap.MoveToRegion(mapspan);
                    Console.WriteLine($"Latitude: {premierePosition.Latitude}, Longitude: {premierePosition.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erreur", fnsEx.Message, "OK");

            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Erreur", fneEx.Message, "OK");

            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erreur", pEx.Message, "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", "All   umer le GPS pour accées a la location " + ex.Message, "OK");

            }
        }

        private void fini_Tapped(object sender, EventArgs e)
        {
            starting=false;
            var previousPage = Navigation.NavigationStack.LastOrDefault();
            Navigation.PushAsync(new Resultat(utilisateur1,masession,true));
            Navigation.RemovePage(previousPage);
        }

        public async void DisplayLocation()
        {

            try {
                
                     Device.StartTimer(new TimeSpan(0, 0, 1), () =>
                       {
                           this.montimer();
                           Device.BeginInvokeOnMainThread( () =>
                           {
                               masession.Temps = hour + ":" + min + ":" + sec;
                               if (DependencyService.Get<IStepCounter>().IsAvailable())
                               {
                                   DependencyService.Get<IStepCounter>().InitSensorService();
                                   masession.Pas = DependencyService.Get<IStepCounter>().Steps;
                               }

                           });

                           return starting; 
                       });

              
            
                Device.StartTimer(new TimeSpan(0, 0, 2), () =>
                {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    var location = await Geolocation.GetLocationAsync(request, cts.Token);
                    if (location != null)
                    {
                        Position p = new Position(location.Latitude, location.Longitude);
                        if (((old.Longitude == 0) && old.Latitude == 0) || ((old.Latitude != p.Latitude) && (old.Longitude != p.Longitude)))
                        {
                            Location currentloc = new Location(p.Latitude, p.Longitude);
                            double distanceEn2s = Math.Round(Location.CalculateDistance(new Location(old.Latitude, old.Longitude), currentloc, DistanceUnits.Kilometers), 2);
                            old = p;
                            Console.WriteLine(distanceEn2s);
                            if (distanceEn2s < 0.03) { 
                                polyline.Geopath.Add(p);
                                masession.positions.Add(p);
                                mylocalmap.MapElements.Add(polyline);
                                masession.Distance= Math.Round(Location.CalculateDistance(masession.startPosition, currentloc, DistanceUnits.Kilometers),2);
                            }
                        }
                    }

                });
                    return starting;
                });
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erreur", fnsEx.Message, "OK");

            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Erreur", fneEx.Message, "OK");

            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erreur", pEx.Message, "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", "All   umer le GPS pour accées a la location " + ex.Message, "OK");

            }
        }
        public async void montimer()
        {
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 0;
                await TextToSpeech.SpeakAsync("vous avez passer "+ masession.Distance+ "KM dans "+hour+ "heur et"+min+"min" +"Bravo Continue comme ça");
            }
            if (min == 60)
            {
                hour++;
                min = 0;
            }

        }
    }
   
}