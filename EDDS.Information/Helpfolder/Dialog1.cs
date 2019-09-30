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
        int id;
        bool update = false;
        public Dialog1(string _formname, string _tablename, PgSQL _DB, KategoriObyektov form, string _text = "", int _id = 0)
        {
            InitializeComponent();
            DB = _DB;
            this.Text = _formname;
            tablename = _tablename;
            KO = form;
            textBox1.Text = _text;
            id = _id;
            if (_text != "")
            {
                update = true;
                btn_add.Text = "Обновить";
            }
                
        }

        private void btn_add_Click(object sender, EventArgs e)
        { 
            if (textBox1.Text != "")
            {
                string checkquery = @"SELECT name FROM " + tablename + " WHERE name=\'" + textBox1.Text + "\'";
                DataTable DT = DB.ExecuteReader(checkquery);
                if(DT.Rows.Count == 0)
                {
                    if (update)
                    {
                        string query = @"UPDATE " + tablename + " SET name=\'" + textBox1.Text + "\' WHERE id=" + id;
                        DB.ExecuteReader(query);
                        MessageBox.Show("Обновлено");
                    }
                    else
                    {
                        string query = @"INSERT INTO " + tablename + " (name) VALUES(\'" + textBox1.Text + "\')";
                        DB.ExecuteReader(query);
                        MessageBox.Show("Добавлено");
                    }
                    KO.UpdateRows();
                }
                else
                {
                    MessageBox.Show("Такая имя существует !!!");
                }
                
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
