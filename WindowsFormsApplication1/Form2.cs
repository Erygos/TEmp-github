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
        public String selecteddocktime;
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
            if (comboBox1.SelectedItem != null)
            {
                panel2.Visible = true;
                panel1.Visible = false;
                selectedpilot = comboBox1.SelectedItem.ToString();
                label2.Text = "Pilot: " + selectedpilot;

                try
                {
                    SqlDataReader pilotreader;
                    SqlCommand pilotkvery = new SqlCommand("SELECT ship.name, faction.name, docks.name, docking_certificate.ValidFrom FROM pilot JOIN faction ON pilot.FactionID = Faction.ID JOIN ship on Pilot.ShipID = ship.ID join docking_certificate ON docking_certificate.shipID = ship.ID JOIN docks ON docks.ID = docking_certificate.dockID WHERE pilot.name = '" + selectedpilot + "'", spaceDatabaseConnection);
                    pilotreader = pilotkvery.ExecuteReader();
                    while (pilotreader.Read())
                    {
                        selectedship = pilotreader.GetString(0);
                        selectedfaction = pilotreader.GetString(1);
                        selecteddock = pilotreader.GetString(2);
                        selecteddocktime = pilotreader.GetDateTime(3).ToString();
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
                label6.Text = "docked since:" + selecteddocktime;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button8.Visible = true;
            button9.Visible = false;
            comboBox4.Visible = true;
            label9.Visible = true;
            numericUpDown1.Visible = true;
            button12.Visible = true;

            SqlCommand kvery = new SqlCommand("SELECT Commodityname, NumberOfUnits, localprice, currentdock, rumored_min_price FROM (SELECT Commodity.name AS Commodityname, NumberOfUnits, prices.unitPrice AS localprice, docks.name AS currentdock FROM ship JOIN cargo_card On ship.id = cargo_card.shipID JOIN commodity ON cargo_card.commodityID = commodity.ID JOIN prices ON commodity.ID = prices.CommodityID JOIN docks ON prices.DockID = Docks.ID JOIN docking_certificate ON docking_certificate.DockID = docks.ID AND docking_certificate.ShipID = ship.ID WHERE ship.name = '" + selectedship + "') AS temptable JOIN((SELECT commodity.name AS rumoredname, MIN(prices.Unitprice) AS rumored_min_price FROM commodity JOIN prices ON commodity.ID = prices.CommodityID JOIN Docks ON docks.ID = prices.DockID GROUP BY commodity.name)) AS temptable2 ON Commodityname = rumoredname", spaceDatabaseConnection);

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
            catch (Exception a)
            {
                MessageBox.Show(a.Message.ToString());
            }


            try
            {
                SqlCommand kvery2 = new SqlCommand("SELECT commodity.name AS commodity FROM docks JOIN prices ON docks.ID = prices.dockID JOIN commodity ON prices.commodityID = commodity.ID WHERE docks.name = '" + selecteddock + '\'', spaceDatabaseConnection);
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
            catch (Exception a)
            {
                MessageBox.Show(a.Message.ToString());
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;

            SqlCommand kvery = new SqlCommand("SELECT docks.Name AS Current_dock, Location, ValidFrom, ValidThrough FROM docks JOIN docking_certificate ON docks.ID = docking_certificate.DockID join ship ON ship.ID = docking_certificate.ShipID WHERE docks.Name = '"+ selecteddock +"' AND ship.Name = '"+ selectedship +"'", spaceDatabaseConnection);

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
            catch (Exception a)
            {
                MessageBox.Show(a.Message.ToString());
            }

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
            int toSell = Convert.ToInt32(numericUpDown1.Value);
            int have = 0;
            if (comboBox4.SelectedItem != null)
            {
                String selectedsellitem = comboBox4.SelectedItem.ToString();
                try
                {
                    SqlDataReader pilotreader;
                    SqlCommand pilotkvery = new SqlCommand("SELECT NumberOFUnits FROM cargo_card JOIN ship ON ship.ID = cargo_card.ShipID JOIN commodity ON commodity.ID = cargo_card.CommodityID WHERE Ship.Name = '" + selectedship + "' AND Commodity.Name = '" + selectedsellitem + "'", spaceDatabaseConnection);
                    pilotreader = pilotkvery.ExecuteReader();
                    while (pilotreader.Read())
                    {
                        have = pilotreader.GetInt32(0);
                    }
                    pilotreader.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message.ToString());
                }

                if (have - toSell == 0)
                {

                    String selecteditem = comboBox4.SelectedItem.ToString();
                    SqlCommand kvery3 = new SqlCommand("DELETE FROM cargo_card WHERE ShipID = (SELECT ID FROM ship Where ship.Name = '" + selectedship + "') AND CommodityID = (SELECT ID FROM commodity Where commodity.Name = '" + selectedsellitem + "'); ", spaceDatabaseConnection);
                    try
                    {
                        kvery3.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Do you know how to format?");
                    }
                }
                else
                {
                    if (have - toSell > 0)
                    {
                        int newnumber = have - toSell;
                        String selecteditem = comboBox4.SelectedItem.ToString();
                        SqlCommand kvery2 = new SqlCommand("UPDATE cargo_card SET NumberOfUnits = " + newnumber + " WHERE ShipID = (SELECT ID FROM ship Where ship.Name = '" + selectedship + "') AND CommodityID = (SELECT ID FROM commodity Where commodity.Name = '" + selectedsellitem + "')", spaceDatabaseConnection);
                        try
                        {
                            kvery2.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Do you know how to format?");
                        }

                    }
                }

                SqlCommand kvery = new SqlCommand("SELECT Commodityname, NumberOfUnits, localprice, currentdock, rumored_max_price FROM (SELECT Commodity.name AS Commodityname, NumberOfUnits, prices.unitPrice AS localprice, docks.name AS currentdock FROM ship JOIN cargo_card On ship.id = cargo_card.shipID JOIN commodity ON cargo_card.commodityID = commodity.ID JOIN prices ON commodity.ID = prices.CommodityID JOIN docks ON prices.DockID = Docks.ID JOIN docking_certificate ON docking_certificate.DockID = docks.ID AND docking_certificate.ShipID = ship.ID WHERE ship.name = '" + selectedship + "') AS temptable JOIN((SELECT commodity.name AS rumoredname, MAX(prices.Unitprice) AS rumored_max_price FROM commodity JOIN prices ON commodity.ID = prices.CommodityID JOIN Docks ON docks.ID = prices.DockID GROUP BY commodity.name)) AS temptable2 ON Commodityname = rumoredname", spaceDatabaseConnection);

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
            }



        }

        private void button8_Click(object sender, EventArgs e)
        {
            int tobuy = Convert.ToInt32(numericUpDown1.Value);
            int have = 0;
            if (comboBox4.SelectedItem != null)
            {
                String selectedbuyitem = comboBox4.SelectedItem.ToString();
                try
                {
                    SqlDataReader pilotreader;
                    SqlCommand pilotkvery = new SqlCommand("SELECT NumberOFUnits FROM cargo_card JOIN ship ON ship.ID = cargo_card.ShipID JOIN commodity ON commodity.ID = cargo_card.CommodityID WHERE Ship.Name = '" + selectedship + "' AND Commodity.Name = '" + selectedbuyitem + "'", spaceDatabaseConnection);
                    pilotreader = pilotkvery.ExecuteReader();
                    while (pilotreader.Read())
                    {
                        have = pilotreader.GetInt32(0);
                    }
                    pilotreader.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message.ToString());
                }
                if (have == 0)
                {
                    String selecteditem = comboBox4.SelectedItem.ToString();
                    SqlCommand kvery = new SqlCommand("INSERT INTO cargo_card(ShipID, CommodityID, NumberOfUnits) VALUES((SELECT ID FROM ship WHERE ship.Name = '"+ selectedship +"'), (SELECT ID FROM commodity WHERE commodity.Name = '"+ selectedbuyitem +"'),"+ tobuy +")", spaceDatabaseConnection);
                    try
                    {
                        kvery.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Do you know how to format?");
                    }
                   
                }
                else
                {
                    int newnumber = have + tobuy;
                    String selecteditem = comboBox4.SelectedItem.ToString();
                    SqlCommand kvery = new SqlCommand("UPDATE cargo_card SET NumberOfUnits = " + newnumber + " WHERE ShipID = (SELECT ID FROM ship Where ship.Name = '" + selectedship + "') AND CommodityID = (SELECT ID FROM commodity Where commodity.Name = '" + selectedbuyitem + "')", spaceDatabaseConnection);
                    try
                    {
                        kvery.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Do you know how to format?");
                    }
                }

            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;

            if (comboBox3.SelectedItem != null)
            {
                //in the future exited at and other timestamps should be handled by a new app for space calendar
                SqlCommand insertnewdockingcertificate = new SqlCommand("BEGIN TRANSACTION UPDATE docking_certificate SET ExitedAt = '63600920' WHERE docking_certificate.DockID = (SELECT ID FROM docks WHERE docks.Name = '"+ selecteddock +"') AND docking_certificate.ShipID = (SELECT ID FROM ship WHERE ship.Name = '"+ selectedship +"') AND docking_certificate.ValidFrom = '"+ selecteddocktime +"'; INSERT INTO docking_certificate(ShipID, DockID, ValidFrom, ValidThrough) VALUES((SELECT ID FROM ship WHERE ship.Name = '" + selectedship + "'), (SELECT ID FROM docks WHERE docks.Name = '" + comboBox3.SelectedItem.ToString() + "'),'36300921','36300923'); COMMIT", spaceDatabaseConnection);

                try
                {
                    insertnewdockingcertificate.ExecuteNonQuery();
                    selecteddock = comboBox3.SelectedItem.ToString();
                    label5.Text = "dock: " + selecteddock;
                    label6.Text = "docked since: 36300921";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }


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
