using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Test
{
    public partial class MainPage : ContentPage
    {
        private ArduinoConnectie arduinoConnectie;
        public MainPage()
        {
            InitializeComponent();

            arduinoConnectie = new ArduinoConnectie("COM3", 9600); // Zorg ervoor dat "COM3" overeenkomt met je Arduino-poort
            arduinoConnectie.UpCommandReceived += MoveUpInListBox;
            arduinoConnectie.DownCommandReceived += MoveDownInListBox;
            arduinoConnectie.plusCommandReceived += PlusOneListBox;
            arduinoConnectie.minusCommandReceived += MinusOneListBox;

            // Open de seriële verbinding
            arduinoConnectie.OpenConnection();

            using (var dbConnector = new DatabaseConnection())
            {
                try
                {
                    string query = "SELECT * FROM medewerkers";
                    var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query, "naam", "hoeveelheid");

                    List<Item> producten = new List<Item>();

                    for (int i = 0;i < namen.Count && i < hoeveelheid.Count; i++)
                    {
                        producten.Add(new Item { Name = namen[i], Hoeveelheid = hoeveelheid[i] });
                    }

                    BindingContext = this;
                    NamesListView.ItemsSource = producten;
                }

                catch (Exception ex)
                {
                    Debug.WriteLine($"Er is een fout opgetreden: {ex.Message}");
                }
            }

            var items = (List<Item>)NamesListView.ItemsSource;
            NamesListView.SelectedItem = items[0];
        }

        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }

        private void MoveUpInListBox()
        {
            var items = (List<Item>)NamesListView.ItemsSource;

            if (NamesListView.SelectedItem is Item selectedItem) 
            {
                var currentIndex = items.IndexOf(selectedItem);
                currentIndex--;
                if (currentIndex >= 0)
                {
                    NamesListView.SelectedItem = items[currentIndex];
                }
            }
        }

        private void MoveDownInListBox()
        {
            var items = (List<Item>)NamesListView.ItemsSource;

            if (NamesListView.SelectedItem is Item selectedItem)
            {
                var currentIndex = items.IndexOf(selectedItem);
                currentIndex++;
                if (currentIndex < items.Count)
                {
                    NamesListView.SelectedItem = items[currentIndex];
                }
            }
        }

        private void PlusOneListBox()
        {
            var items = (List<Item>)NamesListView.ItemsSource;
            if (NamesListView.SelectedItem is Item selectedItem)
            {
                var currentIndex = items.IndexOf(selectedItem);
                selectedItem.Hoeveelheid++;
                NamesListView.ItemsSource = new List<Item>((List<Item>)NamesListView.ItemsSource);

                NamesListView.SelectedItem = items[currentIndex];

                using (var dbConnector = new DatabaseConnection())
                {
                    string naam = selectedItem.Name;
                    int hoeveelheid = selectedItem.Hoeveelheid;
                    dbConnector.UpdateDatabase(naam, hoeveelheid);
                }
            }
        }

        private void MinusOneListBox()
        {
            var items = (List<Item>)NamesListView.ItemsSource;
            if (NamesListView.SelectedItem is Item selectedItem)
            {
                if (selectedItem.Hoeveelheid > 0)
                {
                    var currentIndex = items.IndexOf(selectedItem);
                    selectedItem.Hoeveelheid--;
                    NamesListView.ItemsSource = new List<Item>((List<Item>)NamesListView.ItemsSource);
                    NamesListView.SelectedItem= items[currentIndex];

                    using (var dbConnector = new DatabaseConnection())
                    {
                        string naam = selectedItem.Name;
                        int hoeveelheid = selectedItem.Hoeveelheid;
                        dbConnector.UpdateDatabase(naam, hoeveelheid);
                    }
                }
            }
        }

        private async void NaarRecepten(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Recepten("Filter op: "));
            arduinoConnectie.CloseConnection();
        }
    }
}
