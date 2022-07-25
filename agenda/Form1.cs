using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglan;
        SqlCommand komut;
        SqlDataReader oku;

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.Columns.Add("TASKS");
            listView1.Columns.Add("DATE");

            string sorgu = "select * from agenda where isActive = 1";
            baglan = new SqlConnection("server=.; Initial Catalog=login; Integrated Security=True;");
            komut = new SqlCommand(sorgu, baglan);
            baglan.Open();

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                string[] row = { oku["description"].ToString(), oku["date"].ToString() };
                var s = new ListViewItem(row);
                listView1.Items.Add(s);
            }
            baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Remove(listView1.SelectedItems[0]);
    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into agenda(description,date,isActive) values (@description,@date,@isActive)";
            baglan = new SqlConnection("server=.; Initial Catalog=login; Integrated Security=True;");
            komut = new SqlCommand(sorgu, baglan);
            komut.Parameters.AddWithValue("@description", desctextBox.Text);
            komut.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@isActive","True");
            
            baglan.Open();
            oku = komut.ExecuteReader();
            baglan.Close();

            string[] row = { desctextBox.Text, dateTimePicker1.Text };
            var s = new ListViewItem(row);
            listView1.Items.Add(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
