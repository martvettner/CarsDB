using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace CarsDB
{
    public partial class Form1 : Form
    {
        string connectionStrings;
        SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connectionStrings = ConfigurationManager.ConnectionStrings["CarsDB.Properties.Settings.CarsConnectionString"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateCarsTable();
        }
        private void PopulateCarsTable()
        {
            using (connection = new SqlConnection(connectionStrings)) 
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Car", connection))
            {
                DataTable carTable = new DataTable();
                adapter.Fill(carTable);

                listCar.DisplayMember = "CarMarkName";
                listCar.ValueMember = "Id";
                listCar.DataSource = carTable;
            }

        }

        private void listCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCarsNames();
        }

        private void PopulateCarsNames()
        {
            string query = "SELECT CarInGarage.CarModelName FROM CarInGarage INNER JOIN Car ON CarInGarage.CarMarkId = Car.Id WHERE CarInGarage.CarMarkId = @CarId";
            using (connection = new SqlConnection(connectionStrings))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CarId", listCar.SelectedValue);
                DataTable CarsTable = new DataTable();
                adapter.Fill(CarsTable);

                listModels.DisplayMember = "CarModelName";
                listModels.ValueMember = "Id";
                listModels.DataSource = CarsTable;

            }
        }
    }
}
