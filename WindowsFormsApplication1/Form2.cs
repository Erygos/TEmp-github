using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public SqlConnection spaceDatabaseConnection;
        public String selectedpilot;
        public String selectedship;
        public String selectedfaction;
        public String selecteddock;
        public Form2()
        {
            InitializeComponent();
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                spaceDatabaseConnection = new SqlConnection("user id=Erygos;" +
                                     "password=Dragon;server=ILIREJ-1-1-2\\DBSSQLSERVER;" +
                                     "Trusted_Connection=yes;" +
                                     "database=SpaceDockDatabase; " +
                                     "connection timeout=15");
                spaceDatabaseConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("wrong");
            }

            // load dropdown
            try
            {

                SqlCommand kvery = new SqlCommand("SELECT name FROM pilot", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery;
                DataTable dbatable = new DataTable();
                sda.Fill(dbatable);
                comboBox1.Items.Clear();
                foreach (DataRow dr in dbatable.Rows)
                {
                    comboBox1.Items.Add(dr["Name"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            selectedpilot = comboBox1.SelectedItem.ToString();
            label2.Text = "Pilot: " + selectedpilot;

            try
            {
                SqlDataReader pilotreader;
                SqlCommand pilotkvery = new SqlCommand("SELECT ship.name, faction.name, docks.name FROM pilot JOIN faction ON pilot.FactionID = Faction.ID JOIN ship on Pilot.ShipID = ship.ID join docking_certificate ON docking_certificate.shipID = ship.ID JOIN docks ON docks.ID = docking_certificate.dockID WHERE pilot.name = '"+ selectedpilot +"'", spaceDatabaseConnection);
                pilotreader = pilotkvery.ExecuteReader();
                while (pilotreader.Read())
                {
                    selectedship = pilotreader.GetString(0);
                    selectedfaction = pilotreader.GetString(1);
                    selecteddock = pilotreader.GetString(2);
                }
                pilotreader.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message.ToString());
            }

            label3.Text = "Ship: " + selectedship;
            label4.Text = "Faction: " + selectedfaction;
            label5.Text = "dock: " + selecteddock;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            button8.Visible = true;
            button9.Visible = false;
            comboBox4.Visible = true;
            label9.Visible = true;
            numericUpDown1.Visible = true;
            button12.Visible = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            /*
            try
            {
                SqlCommand kvery2 = new SqlCommand("SELECT name FROM docks", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery2;
                DataTable dbatable = new DataTable();
                sda.Fill(dbatable);
                comboBox3.Items.Clear();
                foreach (DataRow dr in dbatable.Rows)
                {
                    comboBox3.Items.Add(dr["Name"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }
            */
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button8.Visible = false;
            button9.Visible = true;
            comboBox4.Visible = true;
            label9.Visible = true;
            numericUpDown1.Visible = true;
            button12.Visible = true;


            
            //    SqlCommand kvery = new SqlCommand("SELECT Name, commodity_type.category AS Category FROM commodity JOIN commodity_type ON commodity_type.ID = commodity.Category ORDER BY Category ASC", spaceDatabaseConnection);
            //this really long and possibly bad select selects the price of current commodities of your ship and displays it together with your dock and rumored max price
            SqlCommand kvery = new SqlCommand("SELECT Commodityname, NumberOfUnits, localprice, currentdock, rumored_max_price FROM (SELECT Commodity.name AS Commodityname, NumberOfUnits, prices.unitPrice AS localprice, docks.name AS currentdock FROM ship JOIN cargo_card On ship.id = cargo_card.shipID JOIN commodity ON cargo_card.commodityID = commodity.ID JOIN prices ON commodity.ID = prices.CommodityID JOIN docks ON prices.DockID = Docks.ID JOIN docking_certificate ON docking_certificate.DockID = docks.ID AND docking_certificate.ShipID = ship.ID WHERE ship.name = '"+selectedship+"') AS temptable JOIN((SELECT commodity.name AS rumoredname, MAX(prices.Unitprice) AS rumored_max_price FROM commodity JOIN prices ON commodity.ID = prices.CommodityID JOIN Docks ON docks.ID = prices.DockID GROUP BY commodity.name)) AS temptable2 ON Commodityname = rumoredname", spaceDatabaseConnection);

            try
            {
                SqlDataAdapter sda3 = new SqlDataAdapter();
                sda3.SelectCommand = kvery;
                DataTable dbatest3 = new DataTable();
                sda3.Fill(dbatest3);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dbatest3;
                dataGridView1.DataSource = bSource;
                sda3.Update(dbatest3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }


            try
            {
                SqlCommand kvery2 = new SqlCommand("SELECT commodity.name AS commodity FROM pilot JOIN ship ON pilot.shipID = ship.ID JOIN Cargo_Card ON Cargo_card.ShipID = Ship.ID JOIN commodity ON commodity.ID = cargo_card.CommodityID WHERE pilot.name = '" + selectedpilot + '\'', spaceDatabaseConnection);
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = kvery2;
                DataTable dbatable2 = new DataTable();
                sda2.Fill(dbatable2);
                comboBox4.Items.Clear();
                foreach (DataRow dr in dbatable2.Rows)
                {
                    comboBox4.Items.Add(dr["Commodity"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }
            /*
            SqlCommand kvery = new SqlCommand("SELECT * FROM pilot", spaceDatabaseConnection);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery;
                DataTable dbatest = new DataTable();
                sda.Fill(dbatest);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dbatest;
                dataGridView1.DataSource = bSource;
                sda.Update(dbatest);
            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }

            */
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /*
            int toSell = numericUpDown1.Value;
            int have;
            try
            {
                SqlDataReader pilotreader;
                SqlCommand pilotkvery = new SqlCommand("SELECT", spaceDatabaseConnection);
                pilotreader = pilotkvery.ExecuteReader();
                while (pilotreader.Read())
                {
                    selectedship = pilotreader.GetString(0);
                    selectedfaction = pilotreader.GetString(1);
                    selecteddock = pilotreader.GetString(2);
                }
                pilotreader.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message.ToString());
            }
            */
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button8.Visible = false;
            button9.Visible = false;
            comboBox4.Visible = false;
            label9.Visible = false;
            numericUpDown1.Visible = false;
            button12.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
