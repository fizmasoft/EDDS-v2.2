using Fizmasoft.PostgreSQL;
using Newtonsoft.Json.Linq;
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
    public partial class DialogDejurstv : Form
    {
        PgSQL DB;
        NodeTabPages NTP;
        DataTable dtshift = new DataTable();
        GrafikDejurstv GD;
        bool update;
        string id, shiftid = "", shiftname = "";

        public DialogDejurstv(PgSQL _DB, string dialogname, NodeTabPages _NTP, GrafikDejurstv _GD, bool _update, bool _read, string _id = "")
        {
            InitializeComponent();
            this.Text = dialogname + " пользователя";
            DB = _DB;
            NTP = _NTP;
            GD = _GD;
            update = _update;
            id = _id;

            dtshift = DB.ExecuteReader("SELECT * FROM edds_shift ORDER BY id");

            foreach (DataRow row in dtshift.Rows)
                cmbox_smena.Items.Add(row["name"].ToString());
            if (update)
            {
                DataTable dtuser = DB.ExecuteReader("SELECT * FROM edds_users WHERE id=" + id);

                shiftid = dtuser.Rows[0][1].ToString();
                shiftname = dtshift.Select("id=" + shiftid)[0][1].ToString();
                cmbox_smena.Text = shiftname;
                txtbox_username.Text = dtuser.Rows[0][2].ToString();
                txtbox_fullname.Text = dtuser.Rows[0][4].ToString();
                JObject json = JObject.Parse(dtuser.Rows[0][5].ToString());
                chbox_delete.Checked = (bool)json["delete"];
                chbox_read.Checked = (bool)json["read"];
                chbox_write.Checked = (bool)json["write"];
                chbox_status.Checked = (bool)dtuser.Rows[0][6];
            }

            if (_read)
            {
                this.ShowIcon = false;
                this.Text = "Информация о пользователь";
                txtbox_username.ReadOnly = true;
                txtbox_password.ReadOnly = true;
                txtbox_fullname.ReadOnly = true;
                chbox_read.Enabled = false;
                chbox_write.Enabled = false;
                chbox_delete.Enabled = false;
                chbox_status.Enabled = false;

                txtbox_username.BackColor = Color.White;
                txtbox_password.BackColor = Color.White;
                txtbox_fullname.BackColor = Color.White;

                btn_save.Visible = false;
                btn_cancel.Visible = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string checkquery = @"SELECT username FROM edds_users WHERE lower(username)=lower('" + txtbox_username.Text.Replace("'", "''") + "')";
            DataTable DT = DB.ExecuteReader(checkquery);
            if (txtbox_username.Text != "" && txtbox_fullname.Text != "" && cmbox_smena.Text != "")
            {
                if (DT.Rows.Count > 0)
                {
                    if (Passwordcheck() || (txtbox_password.Text == "" && txtbox_reenter.Text == ""))
                    {
                        if (txtbox_password.Text == txtbox_reenter.Text)
                        {
                            string text = cmbox_smena.Text;
                            shiftid = dtshift.Select("name='" + text + "'")[0][0].ToString();
                            if (update)
                            {
                                if (txtbox_password.Text != "")
                                {
                                    string query = "UPDATE edds_users SET shift_id='" + shiftid
                                    + "', full_name='" + txtbox_fullname.Text.Replace("'", "''")
                                    + "', username='" + txtbox_username.Text.Replace("'", "''")
                                    + "', password='" + txtbox_password.Text.Replace("'", "''")
                                    + "', permission='{\"read\": " + chbox_read.Checked.ToString().ToLower() + ",\"write\": " + chbox_write.Checked.ToString().ToLower() + ",\"delete\": " + chbox_delete.Checked.ToString().ToLower()
                                    + "}', status=" + chbox_status.Checked.ToString().ToLower() + " WHERE id=" + id;
                                    DB.ExecuteReader(query);
                                }
                                else
                                {
                                    string query = "UPDATE edds_users SET shift_id='" + shiftid
                                    + "', full_name='" + txtbox_fullname.Text.Replace("'", "''")
                                    + "', username='" + txtbox_username.Text.Replace("'", "''")
                                    + "', permission='{\"read\": " + chbox_read.Checked.ToString().ToLower() + ",\"write\": " + chbox_write.Checked.ToString().ToLower() + ",\"delete\": " + chbox_delete.Checked.ToString().ToLower()
                                    + "}', status=" + chbox_status.Checked.ToString().ToLower() + " WHERE id=" + id;
                                    DB.ExecuteReader(query);
                                }

                                MessageBox.Show("Обновлено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                string query = "INSERT INTO edds_users(shift_id, username, password, full_name, permission, status)" +
                                " VALUES('" + shiftid + "', '" + txtbox_username.Text + "', '" + txtbox_password.Text + "', '" + txtbox_fullname.Text
                                + "', '{\"read\": " + chbox_read.Checked.ToString().ToLower() + ",\"write\": " + chbox_write.Checked.ToString().ToLower()
                                + ",\"delete\": " + chbox_delete.Checked.ToString().ToLower()
                                + "}', " + chbox_status.Checked.ToString().ToLower() + ")";
                                DB.ExecuteReader(query);
                                MessageBox.Show("Добавлено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtbox_fullname.Text = "";
                                txtbox_username.Text = "";
                                txtbox_password.Text = "";
                                txtbox_reenter.Text = "";
                            }

                            GD.LoadDatabase();
                            GD.Loadtabpanel();
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать только цифры, прописные и строчные буквы", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Такая имя ползователь существует", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пополните все поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Passwordcheck()
        {
            string pass = txtbox_password.Text;
            bool lowercase = false;
            bool uppercase = false;
            bool digit = false;
            bool incorrect = false;

            foreach (char a in pass)
            {
                if ((Char.IsLetter(a)) && (Char.IsLower(a)))
                    lowercase = true;
                if ((Char.IsLetter(a)) && (Char.IsUpper(a)))
                    uppercase = true;
                if (Char.IsDigit(a))
                    digit = true;
                if (!(Char.IsLetter(a)) && !(Char.IsDigit(a)))
                    incorrect = true;
            }

            if (lowercase && uppercase && digit && !incorrect)
                return true;
            else
                return false;
        }
    }
}
