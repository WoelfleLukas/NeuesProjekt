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
                //OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();

                //bldr.Provider = "Microsoft.ACE.OLEDB.12.0";
                //bldr.DataSource = "Bestellung.accdb";
                con = new OleDbConnection(Properties.Settings.Default.DBcon);
                con.Open();
            }
            catch(Exception b)
            {
                MessageBox.Show(b.Message,"Exception",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            while(reader.Read())
            {
                listBox1.Items.Add(mkArikelObject(reader));
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
                listBox1.Items.Add(reader["ArtikelNr"].ToString() + reader["Bezeichnung"]);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from tArtikel where artikelgruppe = AGR";
            cmd.Parameters.Add("AGR",OleDbType.Integer);
            cmd.Parameters["AGR"].Value = Convert.ToInt32(textBoxArtikelGruppe.Text);
            cmd.CommandType = CommandType.Text;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (InvalidOperationException b)
            {
                MessageBox.Show(b.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            while (reader.Read())
            {
                listBox1.Items.Add(reader["ArtikelNr"].ToString() + reader["Bezeichnung"]);
            }
        }
        private Artikel mkArikelObject(OleDbDataReader reader)
        {
            Artikel a = new Artikel();
            int i = 0;
            if (reader != null)
            {
                a.ArtikelOid = Convert.ToInt32(reader[i++]); 
            }
            else
            {
                a.ArtikelOid = 0;
            }
            if (reader != null)
            {
                a.ArtikelNr = Convert.ToInt32(reader[i++]);
            }
            else
            {
                a.ArtikelNr = 0;
            }
            if (reader != null)
                {
                    a.ArikelGruppe = Convert.ToInt32(reader[i++]);
                }
            else
            {
                a.ArikelGruppe = 0;            }
            if (reader != null)
                    {
                        a.Bezeichnung = reader[i++].ToString();
                    }
            else
            {
                a.Bezeichnung = null;
            }
            if (reader != null)
                        {
                            a.Bestand = Convert.ToInt16(reader[i++]);
                        }
            else
            {
                a.Bestand = 0;
            }
            if (reader != null)
                            {
                                a.Meldebestand = Convert.ToInt16(reader[i++]);
                            }
                            else
            {
                a.Meldebestand = 0;
            }
                                if (reader != null)
                                {
                                    a.Verpackung = Convert.ToInt16(reader[i++]);
                                }
            else
            {
                a.Verpackung = 0;
            }
            if (reader != null)
                                    {
                                        a.VkPreis = Convert.ToDecimal(reader[i++]);
                                    }
            else
            {
                a.VkPreis = 0;
            }
            if (reader != null)
                                        {
                                            a.letzteEntnahme = Convert.ToDateTime(reader[i]);
                                        }
            else
            {
                a.letzteEntnahme = new DateTime();
            }
            return a;

        }
    }
}
