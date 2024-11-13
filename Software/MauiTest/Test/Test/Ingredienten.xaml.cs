using MySqlX.XDevAPI.Common;
using System.ComponentModel;
using System.Diagnostics;

namespace Test;

public partial class Ingredienten : ContentPage
{
    private ArduinoConnectie arduinoConnectie;
    private List<string> FilteredProducts = new List<string>();
    private List<int> FilteredQuantities = new List<int>();
    private string Filters = "";
    private string filters = "";

    public Ingredienten()
	{
        arduinoConnectie = new ArduinoConnectie("COM3", 9600); // Zorg ervoor dat "COM3" overeenkomt met je Arduino-poort
        arduinoConnectie.UpCommandReceived += MoveUpInListBox;
        arduinoConnectie.DownCommandReceived += MoveDownInListBox;
        arduinoConnectie.SelectCommandReceived += SelectInListBox;

        // Open de seriële verbinding
        arduinoConnectie.OpenConnection();
        InitializeComponent();

        using (var dbConnector = new DatabaseConnection())
        {
            try
            {
                string query = "SELECT * FROM medewerkers";
                var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query, "naam", "hoeveelheid");

                List<Item> producten = new List<Item>();

                for (int i = 0; i < namen.Count; i++)
                {
                    producten.Add(new Item { Name = namen[i]});
                }

                BindingContext = this;
                ingredienten.ItemsSource = producten;
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Er is een fout opgetreden: {ex.Message}");
            }

            finally
            {
                dbConnector.CloseConnection();
            }
        }

        var items = (List<Item>)ingredienten.ItemsSource;
        ingredienten.SelectedItem = items[0];
    }

	private async void Terug(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new Recepten("Filter op: "));
    }
    
    private async void GenereerNieuwRecept(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Recepten("Filter op: " + filters));
    }

    private void MoveUpInListBox()
    {
        var items = (List<Item>)ingredienten.ItemsSource;

        if (ingredienten.SelectedItem is Item selectedItem)
        {
            var currentIndex = items.IndexOf(selectedItem);
            currentIndex--;
            if (currentIndex >= 0)
            {
                ingredienten.SelectedItem = items[currentIndex];
            }
        }
    }

    private void MoveDownInListBox()
    {
        var items = (List<Item>)ingredienten.ItemsSource;

        if (ingredienten.SelectedItem is Item selectedItem)
        {
            var currentIndex = items.IndexOf(selectedItem);
            currentIndex++;
            if (currentIndex < items.Count)
            {
                ingredienten.SelectedItem = items[currentIndex];
            }
        }
    }

    private void SelectInListBox()
    {
        filters = "";
        Filters = "";
        using (var dbConnector = new DatabaseConnection())
        {
            var items = (List<Item>)ingredienten.ItemsSource;

            if (ingredienten.SelectedItem is Item selectedItem)
            {
                var currentIndex = items.IndexOf(selectedItem);
                string query = "SELECT * FROM medewerkers";
                var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query, "naam", "hoeveelheid");

                List<Item> producten = new List<Item>();

                for (int i = 0; i < namen.Count; i++)
                {
                    producten.Add(new Item { Name = namen[i], Hoeveelheid = hoeveelheid[i] });
                }

                for (int i = 0; i < producten.Count; i++)
                {
                    if (i == currentIndex)
                    {
                        string naam = producten[i].Name;
                        int Hoeveelheid = producten[i].Hoeveelheid;
                        FilteredProducts.Add(naam);
                        FilteredQuantities.Add(Hoeveelheid);
                        for (int j = 0; j < FilteredProducts.Count && j < FilteredQuantities.Count; j++)
                        {
                            if (j == 0)
                            {
                                filters += FilteredProducts[j];
                                Filters += FilteredProducts[j] + " x" + FilteredQuantities[j];
                            }

                            else
                            {
                                filters += ", " + FilteredProducts[j];
                                Filters += ", " + FilteredProducts[j] + " x" + FilteredQuantities[j];
                            }
                        }
                        result.Text = "Filter op: " + filters;
                    }
                }
            }
        }
    }

    private void Klik(object sender, EventArgs e)
    {
        using (var dbConnector = new DatabaseConnection())
        {
            var items = (List<Item>)ingredienten.ItemsSource;

            if (ingredienten.SelectedItem is Item selectedItem)
            {
                var currentIndex = items.IndexOf(selectedItem);
                string query = "SELECT * FROM medewerkers";
                var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query, "naam", "hoeveelheid");

                List<Item> producten = new List<Item>();

                for (int i = 0; i < namen.Count; i++)
                {
                    producten.Add(new Item { Name = namen[i], Hoeveelheid = hoeveelheid[i] });
                }

                for (int i = 0; i < producten.Count; i++)
                {
                    if (i == currentIndex)
                    {
                        string naam = producten[i].Name;
                        int Hoeveelheid = producten[i].Hoeveelheid;
                        bool isDuplicate = false;

                        for(int x = 0; x < FilteredProducts.Count; x++)
                        {
                            if(naam == FilteredProducts[x])
                            {
                                isDuplicate = true;
                            }
                        }

                        if (!isDuplicate)
                        {
                            filters = "";
                            Filters = "";
                            FilteredProducts.Add(naam);
                            FilteredQuantities.Add(Hoeveelheid);
                            for (int j = 0; j < FilteredProducts.Count && j < FilteredQuantities.Count; j++)
                            {
                                if (j == 0)
                                {
                                    filters += FilteredProducts[j];
                                    Filters += FilteredProducts[j] + " x" + FilteredQuantities[j];
                                }

                                else
                                {
                                    filters += ", " + FilteredProducts[j];
                                    Filters += ", " + FilteredProducts[j] + " x" + FilteredQuantities[j];
                                }
                            }
                            result.Text = "Filter op: " + filters;
                        }
                    }
                }
            }
        }
    }
}