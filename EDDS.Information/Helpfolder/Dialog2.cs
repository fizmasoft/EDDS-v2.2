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
    public partial class Dialog2 : Form
    {
        PgSQL DB;
        KategoriVizova KV;
        string id;
        bool update;
        public Dialog2(string formname, PgSQL _DB, KategoriVizova _KV, bool _update, int number, bool _checkstate, string _id = "")
        {
            InitializeComponent();
            this.Text = formname;
            DB = _DB;
            KV = _KV;
            id = _id;
            update = _update;
            numericUpDown1.Value = number;
            this.Icon = Icon.FromHandle(Images.Get("plus").GetHicon());

            if (_checkstate)
                checkBox1.CheckState = CheckState.Checked;
            if (update)
            {
                this.Icon = Icon.FromHandle(Images.Get("edit").GetHicon());
                btn_add.Text = "Обновить";
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            bool vizov = false;
            if (checkBox1.Checked)
                vizov = true;
            string checkquery = @"SELECT * FROM edds_call_type WHERE number=" + numericUpDown1.Value.ToString() + " AND auto=" + vizov.ToString();
            DataTable DT = DB.ExecuteReader(checkquery);
            if (DT.Rows.Count == 0)
            {
                if (update)
                {
                    string query = @"UPDATE edds_call_type SET number='" + numericUpDown1.Value.ToString() + "', auto='" + Convert.ToBoolean(checkBox1.CheckState) + "' WHERE id=" + id;
                    DB.ExecuteReader(query);
                    MessageBox.Show("Обновлено");
                    this.Close();
                }
                else
                {
                    string query = @"INSERT INTO edds_call_type (number, auto) VALUES('" + numericUpDown1.Value.ToString() + "', '" + Convert.ToBoolean(checkBox1.CheckState) + "' )";
                    DB.ExecuteReader(query);
                    MessageBox.Show("Добавлено");
                    numericUpDown1.Value = 1;
                    checkBox1.CheckState = CheckState.Unchecked;
                }
                KV.loadDatabase();
                KV.UpdateRows();
            }
            else
            {
                MessageBox.Show("Такой вызов существует !!!");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
