using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartKitchen
{
    public partial class Form3 : Form
    {

        private ArduinoConnectie arduinoConnectie;
        public Form3()
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

        private void button1_Click(object sender, EventArgs e)
        {
            arduinoConnectie.CloseConnection();
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arduinoConnectie.CloseConnection();
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
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

                        lb_ingredienten.Items.Add($"{namen[i]} - {hoeveelheid[i]}");
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Er is een fout opgetreden: {ex.Message}");
                }
            }

            if (lb_ingredienten.Items.Count != 0)
            {
                lb_ingredienten.SelectedIndex = 0;
            }
        }

        private void MoveUpInListBox()
        {
            if (lb_ingredienten.InvokeRequired)
            {
                lb_ingredienten.Invoke(new Action(MoveUpInListBox));
            }
            else
            {
                if (lb_ingredienten.SelectedIndex > 0)
                {
                    lb_ingredienten.SelectedIndex--;
                }
            }
        }

        private void MoveDownInListBox()
        {
            if (lb_ingredienten.InvokeRequired)
            {
                lb_ingredienten.Invoke(new Action(MoveDownInListBox));
            }
            else
            {
                if (lb_ingredienten.SelectedIndex < lb_ingredienten.Items.Count - 1)
                {
                    lb_ingredienten.SelectedIndex++;
                }
            }
        }
    }
}
