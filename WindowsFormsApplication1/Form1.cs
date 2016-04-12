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
    public partial class Form1 : Form
    {
        SqlConnection spaceDatabaseConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;

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

            //fill combobox
            try
            {

                SqlCommand kvery2 = new SqlCommand("SELECT name FROM faction", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery2;
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

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;

            SqlCommand kvery = new SqlCommand("SELECT * FROM ship", spaceDatabaseConnection);
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
            //cbox 2 is ship class
            try
            {

                SqlCommand kvery2 = new SqlCommand("SELECT name FROM ship_class", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery2;
                DataTable dbatable = new DataTable();
                sda.Fill(dbatable);
                comboBox2.Items.Clear();
                foreach (DataRow dr in dbatable.Rows)
                {
                    comboBox2.Items.Add(dr["Name"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }
            //cbox 3 is dock
            try
            {

                SqlCommand kvery3 = new SqlCommand("SELECT name FROM docks", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery3;
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            if (comboBox1.SelectedItem != null)
            {
                String selectedfaction = comboBox1.SelectedItem.ToString();
                SqlCommand kvery = new SqlCommand("INSERT INTO pilot(Name, Birthday, OnHandCredits, FactionID) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "', 50000, (SELECT ID FROM faction WHERE faction.Name = '" + selectedfaction + "'))", spaceDatabaseConnection);
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

        private void button12_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if ((comboBox2.SelectedItem != null) && (comboBox3.SelectedItem != null))
                {
                panel3.Visible = false;

                String selectedclass = comboBox2.SelectedItem.ToString();
                String selecteddock = comboBox3.SelectedItem.ToString();
                SqlCommand insertshipandcertificate = new SqlCommand("BEGIN TRANSACTION INSERT INTO ship (Name, CargoSpace, ClassID ) VALUES('" + textBox4.Text + "', " + textBox3.Text + ", (SELECT ID FROM ship_class WHERE ship_class.Name = '" + selectedclass + "')); INSERT INTO docking_certificate(ShipID, DockID, ValidFrom) VALUES((SELECT ID FROM ship WHERE ship.Name = '" + textBox4.Text + "'), (SELECT ID FROM docks WHERE docks.Name = '" + selecteddock + "'),'36300915'); COMMIT", spaceDatabaseConnection);

                try
                {
                    insertshipandcertificate.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Do you know how to format?");
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;

            SqlCommand kvery = new SqlCommand("SELECT * FROM docks", spaceDatabaseConnection);
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
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;

            int capacity = Convert.ToInt32(numericUpDown1.Value);
            
            SqlCommand insertdock = new SqlCommand("INSERT INTO docks(Name, Location, Capacity) VALUES('"+ textBox6.Text +"', '"+ textBox5.Text +"', "+ capacity +")", spaceDatabaseConnection);
           try
            {
                insertdock.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Do you know how to format?");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;

            SqlCommand kvery = new SqlCommand("SELECT docks.Name AS dock, Location, Capacity, Commodity.name AS Commodity, Unitprice  FROM docks JOIN prices ON prices.DockID = ID JOIN commodity ON prices.CommodityID = commodity.ID ORDER BY dock DESC", spaceDatabaseConnection);
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
            
            try
            {
                SqlCommand kvery2 = new SqlCommand("SELECT name FROM commodity", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery2;
                DataTable dbatable = new DataTable();
                sda.Fill(dbatable);
                comboBox9.Items.Clear();
                foreach (DataRow dr in dbatable.Rows)
                {
                    comboBox9.Items.Add(dr["Name"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }

            try
            {
                SqlCommand kvery3 = new SqlCommand("SELECT name FROM docks", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery3;
                DataTable dbatable = new DataTable();
                sda.Fill(dbatable);
                comboBox8.Items.Clear();
                foreach (DataRow dr in dbatable.Rows)
                {
                    comboBox8.Items.Add(dr["Name"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if ((comboBox9.SelectedItem != null) && (comboBox8.SelectedItem != null) && (Convert.ToInt32(numericUpDown1.Value) > 0))
            {
                int price = Convert.ToInt32(numericUpDown1.Value);
                SqlCommand insertprices = new SqlCommand("INSERT INTO prices(CommodityID, DockID, UnitPrice) VALUES((SELECT ID FROM commodity WHERE commodity.Name = '" + comboBox9.SelectedItem.ToString() +"'), (SELECT ID FROM docks WHERE docks.Name = '"+ comboBox8.SelectedItem.ToString() +"'),"+ price + ")", spaceDatabaseConnection);
                try
                {
                    insertprices.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nope");
                }

                SqlCommand kvery = new SqlCommand("SELECT docks.Name AS dock, Location, Capacity, Commodity.name AS Commodity, Unitprice  FROM docks JOIN prices ON prices.DockID = ID JOIN commodity ON prices.CommodityID = commodity.ID ORDER BY dock DESC", spaceDatabaseConnection);
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
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;

            SqlCommand kvery = new SqlCommand("SELECT Name, commodity_type.category AS Category FROM commodity JOIN commodity_type ON commodity_type.ID = commodity.Category ORDER BY Category ASC", spaceDatabaseConnection);
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

            try
            {
                SqlCommand kvery2 = new SqlCommand("SELECT category FROM commodity_type", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery2;
                DataTable dbatable = new DataTable();
                sda.Fill(dbatable);
                comboBox4.Items.Clear();
                foreach (DataRow dr in dbatable.Rows)
                {
                    comboBox4.Items.Add(dr["Category"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {

            if (comboBox4.SelectedItem != null)
            {
                SqlCommand insertcategory = new SqlCommand("INSERT INTO commodity(Name, Category) VALUES('" + textBox14.Text +"', (SELECT ID FROM commodity_type WHERE commodity_type.category = '"+ comboBox4.SelectedItem.ToString() +"'))", spaceDatabaseConnection);
                try
                {
                    insertcategory.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nope");
                }

                SqlCommand kvery = new SqlCommand("SELECT Name, commodity_type.category AS Category FROM commodity JOIN commodity_type ON commodity_type.ID = commodity.Category ORDER BY Category ASC", spaceDatabaseConnection);
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
            }


        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SqlCommand insertcategory = new SqlCommand("INSERT INTO commodity_type(Category) VALUES('"+ textBox13.Text + "')", spaceDatabaseConnection);
            try
            {
                insertcategory.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nope");
            }
           
            try
            {
                SqlCommand kvery2 = new SqlCommand("SELECT category FROM commodity_type", spaceDatabaseConnection);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = kvery2;
                DataTable dbatable = new DataTable();
                sda.Fill(dbatable);
                comboBox4.Items.Clear();
                foreach (DataRow dr in dbatable.Rows)
                {
                    comboBox4.Items.Add(dr["Category"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("nope");
            }
        }
    }
}
