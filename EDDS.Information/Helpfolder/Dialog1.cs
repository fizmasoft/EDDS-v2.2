using Fizmasoft.Drawing;
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
        string name = "name";
        private Node1 nodeObj;
        int id;
        bool update = false;
        public Dialog1(string _formname, string _tablename, PgSQL _DB, Node1 form, string _text = "", int _id = 0)
        {
            InitializeComponent();
            DB = _DB;
            this.Text = _formname;
            tablename = _tablename;
            nodeObj = form;
            textBox1.Text = _text;
            id = _id;
            this.Icon = Icon.FromHandle(Images.Get("plus").GetHicon());
            if (_text != "")
            {
                this.Icon = Icon.FromHandle(Images.Get("edit").GetHicon());
                update = true;
                btn_add.Text = "Обновить";
            }
            if (tablename == "edds_main_duty")
            {
                name = "full_name";
            }
                
        }

        private void btn_add_Click(object sender, EventArgs e)
        { 
            if (textBox1.Text != "")
            {
               string checkquery = @"SELECT "+ name +" FROM " + tablename + " WHERE lower("+ name +")=lower('" + textBox1.Text.Replace("'", "''") + "')";
               DataTable DT = DB.ExecuteReader(checkquery);
                if(DT.Rows.Count == 0)
                {
                    if (update)
                    {
                        string query = @"UPDATE " + tablename + " SET "+ name +"=\'" + textBox1.Text.Replace("'", "''") + "\' WHERE id=" + id;
                        DB.ExecuteReader(query);
                        MessageBox.Show("Обновлено");
                        this.Close();
                    }
                    else
                    {
                        string query = @"INSERT INTO " + tablename + " ("+ name +") VALUES(\'" + textBox1.Text.Replace("'", "''") + "\')";
                        DB.ExecuteReader(query);
                        MessageBox.Show("Добавлено");
                        textBox1.Text = "";
                    }
                    nodeObj.UpdateRows();
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
