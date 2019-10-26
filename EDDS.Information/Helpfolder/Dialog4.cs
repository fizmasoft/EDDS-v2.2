using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fizmasoft.PostgreSQL;
using Fizmasoft.Drawing;

namespace EDDS.Information.Helpfolder
{
    public partial class Dialog4 : Form
    {
        PgSQL DB;
        string id;
        GranitsiChastey form;
        bool update;
        public Dialog4(PgSQL _DB, GranitsiChastey _form , bool _update, string _name = "", string _id = "")
        {
            InitializeComponent();
            this.Icon = Icon.FromHandle(Images.Get("plus").GetHicon());
            DB = _DB;
            form = _form;
            update = _update;
            id = _id;
            this.Text = "Добавление частей";
            if(update)
            {
                this.Icon = Icon.FromHandle(Images.Get("edit").GetHicon());
                this.Text = "Обновление частей";
                btn_add.Text = "Обновить";
                textBox1.Text = _name;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string y = "0";
            if (radioBtn_PChPB.Checked)
                y = "1";
            if (textBox1.Text != "")
            {
                string checkquery = @"SELECT name FROM edds_sections WHERE lower(name)=lower('" + textBox1.Text.Replace("'", "''") + "')";
                DataTable DT = DB.ExecuteReader(checkquery);
                if (DT.Rows.Count == 0)
                {
                    if (update)
                    {
                        string query = @"UPDATE edds_sections SET name='" + textBox1.Text.Replace("'", "''") + "' WHERE id=" + id;
                        DB.ExecuteReader(query);
                        MessageBox.Show("Обновлено");
                        this.Close();
                    }
                    else
                    {
                        int x = DB.ExecuteReader("SELECT * FROM edds_sections where args::jsonb->'order'->'y'='" + y + "'").Rows.Count;
                        string query = @"INSERT INTO edds_sections (name, args) VALUES('" + textBox1.Text.Replace("'", "''") + "', '{\"order\": {\"x\": " + x + ",\"y\": " + y + "},\"edited\": false}' )";
                        //string query2 = "UPDATE edds_sections SET args=jsonb_set(args,'{order, x}',"+x+"::text::jsonb) WHERE name='" + textBox1.Text +"\'";
                        DB.ExecuteReader(query);
                        //DB.ExecuteReader(query2);
                        MessageBox.Show("Добавлено");
                        textBox1.Text = "";
                    }
                    form.loadDatabase();
                    form.UpdateRows();
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
