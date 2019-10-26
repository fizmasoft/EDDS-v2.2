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
    public partial class Dialog5 : Form
    {
        PgSQL DB;
        TelefonniySpravochnik TSform;
        bool update;
        string id;

        public Dialog5(PgSQL _DB, TelefonniySpravochnik _form, bool _update, string _id = "", string fullname="",
            string nickname="", string position="", string home="",string mobile="",string work="", string address="", 
            string title="", string department="")
        {
            InitializeComponent();
            this.Text = "Добавление справочника";
            DB = _DB;
            TSform = _form;
            id = _id;
            update = _update;
            txtbox_fullname.Text = fullname;
            txtbox_nickname.Text = nickname;
            txtbox_position.Text = position;
            txtbox_home.Text = home;
            txtbox_mobile.Text = mobile;
            txtbox_work.Text = work;
            txtbox_address.Text = address;
            txtbox_title.Text = title;
            txtbox_department.Text = department;

            this.Icon = Icon.FromHandle(Images.Get("plus").GetHicon());
            if (update)
            {
                this.Icon = Icon.FromHandle(Images.Get("edit").GetHicon());
                this.Text = "Обновление справочника";
                btnSave.Text = "Обновить";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                if (update)
                {
                string query = @"UPDATE edds_contacts SET full_name='" + txtbox_fullname.Text.Replace("'", "''")
                + "',nickname='" + txtbox_nickname.Text.Replace("'", "''")
                + "',position='" + txtbox_position.Text.Replace("'", "''")
                + "',phone_number='{\"home\":\"" + txtbox_home.Text
                + "\", \"mobile\":\"" + txtbox_mobile.Text
                + "\", \"work\":\"" + txtbox_work.Text
                + "\"}', address='" + txtbox_address.Text.Replace("'", "''")
                + "', title='" + txtbox_title.Text.Replace("'", "''")
                + "', department='" + txtbox_department.Text.Replace("'", "''")
                + "' WHERE id=" + id;
                    DB.ExecuteReader(query);
                    MessageBox.Show("Обновлено");
                    this.Close();
                }
                else
                {
                string query = "INSERT INTO edds_contacts (full_name, nickname, position, phone_number, address, title, department)" +
                " VALUES('" + txtbox_fullname.Text.Replace("'", "''")
                + "', '" + txtbox_nickname.Text.Replace("'", "''")
                + "', '" + txtbox_position.Text.Replace("'", "''")
                + "', '{\"home\":" + txtbox_home.Text
                + ", \"mobile\":" + txtbox_mobile.Text
                + ", \"work\":" + txtbox_work.Text
                + "}', '" + txtbox_address.Text.Replace("'", "''")
                + "', '" + txtbox_title.Text.Replace("'", "''")
                + "', '" + txtbox_department.Text.Replace("'", "''") + "')";
                    DB.ExecuteReader(query);
                    MessageBox.Show("Добавлено");
                }
                TSform.loadDatabase();
                TSform.UpdateRows();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
