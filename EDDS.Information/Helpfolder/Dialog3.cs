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
    public partial class Dialog3 : Form
    {
        PgSQL DB;
        Signal form;
        string id;
        bool update;
        string name;
        public Dialog3(PgSQL _DB, Signal _form, bool _update, string _name = "", string _description = "", string _id = "")
        {
            InitializeComponent();
            DB = _DB;
            form = _form;
            id = _id;
            update = _update;
            name = _name;
            this.Icon = Icon.FromHandle(Images.Get("plus").GetHicon());
            if (update)
            {
                this.Icon = Icon.FromHandle(Images.Get("edit").GetHicon());
                this.Text = "Обновление сигналов";
                btn_add.Text = "Обновить";
                textBox1.Text = _name;
                txtbox_desc.Text = _description;
            }
            else
            {
                this.Text = "Добавление сигналов";
            }
        }


        private void btn_add_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (update)
                {
                    string checkquery = @"SELECT * FROM edds_keywords WHERE lower(name)=lower('" + textBox1.Text.Replace("'", "''") + "')";
                    DataTable DT = DB.ExecuteReader(checkquery);
                    if (name.ToLower() == textBox1.Text.ToLower())
                    {
                        string query = "UPDATE edds_keywords SET name='" + textBox1.Text.Replace("'", "''") + "', \"desc\"='" + txtbox_desc.Text.Replace("'", "''") + "' WHERE id=" + id;
                        DB.ExecuteReader(query);
                        MessageBox.Show("Обновлено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        form.loadDatabase();
                        form.UpdateRows();
                    }
                    else if (DT.Rows.Count == 0)
                    {
                        string query = "UPDATE edds_keywords SET name='" + textBox1.Text.Replace("'", "''") + "', \"desc\"='" + txtbox_desc.Text.Replace("'", "''") + "' WHERE id=" + id;
                        DB.ExecuteReader(query);
                        MessageBox.Show("Обновлено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        form.loadDatabase();
                        form.UpdateRows();
                    }
                    else { MessageBox.Show("Такой сигнал существует !!!"); }
                }
                else
                {
                    string checkquery = @"SELECT * FROM edds_keywords WHERE lower(name)=lower('" + textBox1.Text.Replace("'", "''") + "')";
                    DataTable DT = DB.ExecuteReader(checkquery);
                    if (DT.Rows.Count == 0)
                    {
                        string query = "INSERT INTO edds_keywords (name, \"desc\") VALUES('" + textBox1.Text.Replace("'", "''") + "', '" + txtbox_desc.Text.Replace("'", "''") + "')";
                        DB.ExecuteReader(query);
                        MessageBox.Show("Добавлено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtbox_desc.Text = "";
                        textBox1.Text = "";
                        textBox1.Focus();
                        form.loadDatabase();
                        form.UpdateRows();
                    }
                    else { MessageBox.Show("Такой сигнал существует !!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            else
            {
                MessageBox.Show("Вы должны напечатать что-нибудь", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }


}

