using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        OleDbConnection con = null;
        OleDbCommand cmd;
        OleDbDataReader reader;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();

                bldr.Provider = "Microsoft.ACE.OLEDB.12.0";
                bldr.DataSource = "Bestellung.accdb";
                con = new OleDbConnection(bldr.ConnectionString);
                con.Open();
            }
            catch(Exception b)
            {
                MessageBox.Show(b.Message,"Exception",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from tArtikel";
            cmd.CommandType = CommandType.Text;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch(InvalidOperationException b)
            {
                MessageBox.Show(b.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            while(reader.Read())
            {
                listBox1.Items.Add(reader["ArtikelNr"].ToString());
            }
            
        }
    }
}
