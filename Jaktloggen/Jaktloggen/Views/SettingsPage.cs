using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Jaktloggen.Models;
using Jaktloggen.ViewModels;
using Jaktloggen.Views.Base;
using Jaktloggen.Views.Cells;
using Jaktloggen.Views.Input;
using Xamarin.Forms;

namespace Jaktloggen.Views
{
    public class SettingsPage : Base.ContentPageJL
    {
        private SettingsVM VM;
        public SettingsPage()
        {
            BindingContext = VM = new SettingsVM();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        private void Init()
        {
            Title = VM.Title;
            
            Content = new TableViewJL
            {
                HasUnevenRows = true,
                Root = new TableRoot
                {
                    new TableSection()
                    {
                        new JL_ButtonCell("Sikkerhetskopier data", ButtonExport_OnClicked),
                        new JL_ButtonCell("Erstatt data fra sikkerhetskopi", ButtonImport_OnClicked),
                        new JL_TextCell("Se alle loggføringer på kartet", ">>", ViewLogsOnMap_OnTapped)
        },
                },
            };
        }

        private async void ViewLogsOnMap_OnTapped(object sender, EventArgs eventArgs)
{
    await Navigation.PushAsync(new PositionLogsPage(App.Database.GetLoggs()));
}
private async void ButtonExport_OnClicked(object sender, EventArgs e)
        {
            var ok = await DisplayAlert("Bekreft eksport", "All data blir lastet ned på disk", "OK", "Avbryt");
            if (ok)
            {
                await VM.Export();
            }
        }
        private async void ButtonImport_OnClicked(object sender, EventArgs e)
        {
            var ok = await DisplayAlert("Bekreft import", "All data blir erstattet av importdata.", "OK", "Avbryt");
            if (ok)
            {
                await VM.Import();
            }
        }
    }
}
