using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Xml.Linq;

namespace SmartKitchen
{
    public partial class HomePage : Form
    {

        private ArduinoConnectie arduinoConnectie;
        public HomePage()
        {
            InitializeComponent();

            // Instantieer de ArduinoConnection en sluit evenementen aan
            arduinoConnectie = new ArduinoConnectie("COM3", 9600); // Zorg ervoor dat "COM3" overeenkomt met je Arduino-poort
            arduinoConnectie.UpCommandReceived += MoveUpInListBox;
            arduinoConnectie.DownCommandReceived += MoveDownInListBox;
            arduinoConnectie.plusCommandReceived += PlusOneListBox;
            arduinoConnectie.minusCommandReceived += MinusOneListBox;

            // Open de seriële verbinding
            arduinoConnectie.OpenConnection();
        }

        List<string> result = new List<string>();

        private void UpdateListBox()
        {
            lb_producten.Items.Clear();
            using (var dbConnector = new DatabaseConnection())
            {
                string query1 = "SELECT * FROM medewerkers";
                var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query1, "naam", "hoeveelheid");

                for (int i = 0; i < namen.Count; i++)
                {
                    if (namen[i] == "")
                    {
                        break;
                    }

                    lb_producten.Items.Add($"{namen[i]} - {hoeveelheid[i]}");
                }
            }
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            using (var dbConnector = new DatabaseConnection())
            {
                try
                {
                    //string query1 = "INSERT INTO medewerkers (naam, hoeveelheid) VALUES ('Andor', 5)";
                    //string query1 = "DELETE FROM medewerkers WHERE naam = 'Daan';";
                    //string query1 = "INSERT INTO medewerkers (hoeveelheid) VALUES (3)";
                    //dbConnector.ExecuteQuery(query1, "naam", "hoeveelheid");
                    string query = "SELECT * FROM medewerkers";
                    var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query, "naam", "hoeveelheid");

                    for (int i = 0; i < namen.Count; i++)
                    {
                        if (namen[i] == "")
                        {
                            break;
                        }

                        lb_producten.Items.Add($"{namen[i]} - {hoeveelheid[i]}");
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Er is een fout opgetreden: {ex.Message}");
                }
            }

            if (lb_producten.Items.Count != 0)
            {
                lb_producten.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arduinoConnectie.CloseConnection();
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            arduinoConnectie.CloseConnection();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arduinoConnectie.CloseConnection();
            Form4 form4 = new Form4();
            this.Hide();
            form4.ShowDialog();
        }

        private void MoveUpInListBox()
        {
            if (lb_producten.InvokeRequired)
            {
                lb_producten.Invoke(new Action(MoveUpInListBox));
            }
            else
            {
                if (lb_producten.SelectedIndex > 0)
                {
                    lb_producten.SelectedIndex--;
                }
            }
        }

        private void MoveDownInListBox()
        {
            if (lb_producten.InvokeRequired)
            {
                lb_producten.Invoke(new Action(MoveDownInListBox));
            }
            else
            {
                if (lb_producten.SelectedIndex < lb_producten.Items.Count - 1)
                {
                    lb_producten.SelectedIndex++;
                }
            }
        }

        private void PlusOneListBox()
        {
            if (lb_producten.InvokeRequired)
            {
                lb_producten.Invoke(new Action(PlusOneListBox));
            }

            else
            {
                int geselecteerdeIndex = lb_producten.SelectedIndex;

                using (var dbConnector = new DatabaseConnection())
                {
                    string query = "SELECT * FROM medewerkers";
                    var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query, "naam", "hoeveelheid");

                    if (geselecteerdeIndex < 0 || geselecteerdeIndex >= hoeveelheid.Count)
                    {
                        return;
                    }

                    int geselecteerdeWaarde = hoeveelheid[geselecteerdeIndex];
                    geselecteerdeWaarde++;

                    string geselecteerdeNaam = namen[geselecteerdeIndex];

                    dbConnector.UpdateDatabase(geselecteerdeNaam, geselecteerdeWaarde);

                    lb_producten.Items.Clear();

                    UpdateListBox();

                    lb_producten.SelectedIndex = geselecteerdeIndex;
                }
            }
        }

        private void MinusOneListBox()
        {
            if (lb_producten.InvokeRequired)
            {
                lb_producten.Invoke(new Action(MinusOneListBox));
            }

            else
            {
                int geselecteerdeIndex = lb_producten.SelectedIndex;

                using (var dbConnector = new DatabaseConnection())
                {
                    string query = "SELECT * FROM medewerkers";
                    var (namen, hoeveelheid) = dbConnector.ExecuteQuery(query, "naam", "hoeveelheid");

                    if (geselecteerdeIndex < 0 || geselecteerdeIndex >= hoeveelheid.Count)
                    {
                        return;
                    }

                    int geselecteerdeWaarde = hoeveelheid[geselecteerdeIndex];
                    geselecteerdeWaarde--;
                    if (geselecteerdeWaarde <= 0)
                    {
                        geselecteerdeWaarde = 0;
                        //verwijder product
                    }

                    string geselecteerdeNaam = namen[geselecteerdeIndex];

                    dbConnector.UpdateDatabase(geselecteerdeNaam, geselecteerdeWaarde);

                    lb_producten.Items.Clear();

                    UpdateListBox();

                    lb_producten.SelectedIndex = geselecteerdeIndex;
                }
            }
        }
    }
}
