using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Microcharts;
using fitnessTraker.Helper;
using fitnessTraker.Model;

namespace fitnessTraker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class statistique : ContentPage
    {
        Utilisateur utilisateur;
        List<String> colors = new List<String>
        {
            "00FED1",
            "FF1493",
            "00CED1",
            "00AED1",
            "F0CED1",
            "E0CED1",
            "A0CED1",
        };
        int i = 0;
           
        public statistique(Utilisateur utilisateur0, List<MaSession> data)
        {
            InitializeComponent();
            utilisateur = utilisateur0;
            List<Entry> entries = new List<Entry>();
            List<Entry> entries2 = new List<Entry>();
            foreach (var item in data)
            {
                Console.WriteLine(colors[i]);
                DateTime oDate = DateTime.Parse(item.date);
                entries.Add(new Entry((float)item.Pas)
                {
                    Color = SKColor.Parse(colors[i++]),
                    Label = oDate.Day.ToString()+'/'+oDate.Month.ToString(),
                    ValueLabel = item.Pas.ToString()
                }) ;

                entries2.Add(new Entry((float)item.Distance)
                {
                    Color = SKColor.Parse(colors[i++]),
                    Label = oDate.Day.ToString() + '/' + oDate.Month.ToString(),
                    ValueLabel = item.Distance.ToString()
                });
            }
            Console.WriteLine("push");
            BarChart br = new BarChart();
            br.LabelTextSize = 50;
            br.Entries = entries2;
            mystatistique.Chart =br;

            LineChart br2 = new LineChart();
            br2.LabelTextSize = 50;
            br2.Entries = entries;
            mystatistique2.Chart = br2;
            Console.WriteLine("fin");


        }


    }
}