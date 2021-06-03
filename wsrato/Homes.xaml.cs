using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace wsrato
{
    /// <summary>
    /// Логика взаимодействия для Homes.xaml
    /// </summary>
    public partial class Homes : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable House;
        public Homes()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Def"].ConnectionString;
        }
        private void Gridus_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM House";
            House = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(House);
                Gridus.ItemsSource = House.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        private void Up()
        {
            SqlCommandBuilder sqlcombil = new SqlCommandBuilder(adapter);
            adapter.Update(House);
        }
        private void Del()
        {
            if (Gridus.SelectedItem != null)
            {
                for (int i = 0; i < Gridus.SelectedItems.Count; i++)
                {
                    DataRowView drv = Gridus.SelectedItems[i] as DataRowView;
                    if (drv != null)
                    {
                        DataRow dr = (DataRow)drv.Row;
                        dr.Delete();
                    }
                }
            }
            Up();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Up();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Del();
        }
    }
}
