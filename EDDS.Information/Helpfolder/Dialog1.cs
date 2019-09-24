using Fizmasoft.PostgreSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDDS.Information.Helpfolder
{
    public partial class Dialog1 : Form
    {
        PgSQL DB = new PgSQL();
        string tablename;
        private KategoriObyektov KO;
        public Dialog1(string _formname, string _tablename, PgSQL _DB, KategoriObyektov form)
        {
            InitializeComponent();
            DB = _DB;
            this.Text = _formname;
            tablename = _tablename;
            KO = form;
        }

        private void btn_add_Click(object sender, EventArgs e)
        { 
            if (textBox1.Text != "")
            {
                string query = @"INSERT INTO " + tablename + " (name) VALUES(\'" + textBox1.Text + "\')";
                DB.ExecuteReader(query);
                MessageBox.Show("Добавлено");
                KO.UpdateRows();
            }
            else
            {
                MessageBox.Show("Вы должны напечатать что-нибудь");
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
