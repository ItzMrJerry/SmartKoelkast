using MySql.Data.MySqlClient;

namespace SmartKitchen
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            using (var dbConnector = new DatabaseConnection())
            {
                try
                {
                    string query = "SELECT * FROM tabelnaam";
                    dbConnector.ExecuteQuery(query);
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Er is een fout opgetreden: {ex.Message}");
                }
            }

            MessageBox.Show("Programma beëindigd.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            this.Hide();
            form4.ShowDialog();
        }
    }
}
