using MySql.Data.MySqlClient;

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

            // Open de seriële verbinding
            arduinoConnectie.OpenConnection();
        }

        List<string> result = new List<string>();

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

                    for(int i = 0; i < namen.Count; i++)
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
    }
}
