using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jaktloggen.Views.Input
{
    public class EntryPage : ContentPage
    {
        private string _value;

        public string Value
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_value))
                {
                    return string.Empty;
                }
                var arr = _value.ToCharArray();
                arr[0] = Char.ToUpperInvariant(arr[0]);
                return new String(arr);

            }
            set { _value = value; }
        }
        public bool Multiline { get; set; }
        public Entry entry = new Entry();
        public Editor editor = new Editor();
        public Action<EntryPage> Callback;
        public IEnumerable<string> AutoCompleteEntries;
        public EntryPage(string title, string value, bool isNumeric = false)
        {
            Title = title;
            ToolbarItems.Add(new ToolbarItem("Ferdig", null, () =>
            {
                Navigation.PopAsync(true);
            }, ToolbarItemOrder.Default));

            var layout = new StackLayout
            {
                Padding = 5,
                Children = {
                    new Label { Text = title, HorizontalTextAlignment = TextAlignment.Center }
                }
            };

            if (Multiline)
            {
                editor.Text = value;
                editor.Completed += EntryOnCompleted;
                layout.Children.Add(editor);
            }
            else
            {
                entry.Text = value;
                
                if (isNumeric)
                {
                    entry.Keyboard = Keyboard.Numeric;
                    entry.FontSize = 24;
                    entry.HorizontalTextAlignment = TextAlignment.Center;
                    if (value == "0")
                    {
                        entry.Text = "";
                    }
                }
                
                
                entry.Completed += EntryOnCompleted;
                layout.Children.Add(entry);
            }

            if (AutoCompleteEntries != null && AutoCompleteEntries.Any())
            {
                var autoEntryListView = new ListView();
                autoEntryListView.ItemsSource = AutoCompleteEntries;
                autoEntryListView.ItemSelected += delegate(object sender, SelectedItemChangedEventArgs args)
                {
                    if (args.SelectedItem != null)
                    {
                        entry.Text = args.SelectedItem as string;
                        ((ListView)sender).SelectedItem = null;
                        SaveEntryAndExit();
                    }
                };
                layout.Children.Add(autoEntryListView);
            }

            Content = layout;
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (AutoCompleteEntries == null || !AutoCompleteEntries.Any())
            {
                entry.Focus();
            }
        }

        private async void EntryOnCompleted(object sender, EventArgs eventArgs)
        {
            SaveEntryAndExit();
        }

        private void SaveEntryAndExit()
        {
            Value = entry.Text;
            Callback?.Invoke(this);
            Navigation.PopAsync();
        }
    }
}
