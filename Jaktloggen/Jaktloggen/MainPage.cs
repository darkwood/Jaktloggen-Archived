using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Jaktloggen.Views;
using Xamarin.Forms;

namespace Jaktloggen
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Children.Add(new NavigationPage(new JaktListPage()) { Title = "Jaktloggen" });
            Children.Add(new NavigationPage(new StatsListPage()) { Title = "Statistikk" });
            Children.Add(new NavigationPage(new LoggTypeListPage()) { Title = "Flere felter", Icon = "add_property.png" });
            Children.Add(new NavigationPage(new ArtListPage()) { Title = "Arter" });
            Children.Add(new NavigationPage(new JegerListPage()) { Title = "Jegere" });
            Children.Add(new NavigationPage(new DogListPage()) { Title = "Hunder" });
            Children.Add(new NavigationPage(new SettingsPage()) { Title = "Verktøy" });

        }
    }
}
