using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fizmasoft.PostgreSQL;
using EDDS.Utility;
using Fizmasoft.Drawing;
using Newtonsoft.Json.Linq;

namespace EDDS.Information.Helpfolder
{
    public partial class TelefonniySpravochnik : UserControl
    {
        PgSQL DB;
        DataTable DT;
     
        public TelefonniySpravochnik(PgSQL _DB)
        {
            InitializeComponent();
            DB = _DB;
            Image image = Images.Get("plus");
            Bitmap objimage = new Bitmap(image, new Size(17, 17));
            btnAdd.Image = objimage;
            btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdd.TextAlign = ContentAlignment.MiddleRight;
            if (User.Can.Write == false)
                btnAdd.Enabled = false;
            loadDatabase();
        }

        public void loadDatabase()
        {
            DT = DB.ExecuteReader("SELECT * FROM edds_contacts ORDER BY id;");
        }

        private void TelefonniySpravochnik_Load(object sender, EventArgs e)
        {
            InitializeTable();
        }

        private void InitializeTable()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, GraphicsUnit.Pixel);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            UpdateColumns();
        }

        private void UpdateColumns()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns[dataGridView1.Columns.Add("id", "№")].Width = 50;
            dataGridView1.Columns[dataGridView1.Columns.Add("full_name", "Польное имя")].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[dataGridView1.Columns.Add("position", "Должность")].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewImageColumn img1 = new DataGridViewImageColumn();
            DataGridViewImageColumn img2 = new DataGridViewImageColumn();
            dataGridView1.Columns[dataGridView1.Columns.Add(img1)].Width = 25;
            dataGridView1.Columns[dataGridView1.Columns.Add(img2)].Width = 25;
            if (User.Can.Delete == false)
                img2.Visible = false;
            if (User.Can.Write == false)
                img1.Visible = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(209, 232, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 0, 0);
            UpdateRows();
        }

        public void UpdateRows()
        {
            Cursor = Cursors.WaitCursor;
            dataGridView1.Rows.Clear();

            DataTable dtResult = new DataTable();
            if (textBox1.Text == "")
                dtResult = DT;
            else if (DT.Select("full_name=" + textBox1.Text).Count<DataRow>() != 0)
                dtResult = DT.Select("full_name=" + textBox1.Text).CopyToDataTable();
            Image image1 = Images.Get("edit");
            Image image2 = Images.Get("trash");
            Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
            Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));

            foreach (DataRow row in dtResult.Rows)
            {
                dataGridView1.Rows[dataGridView1.Rows.Add(row["id"], row["full_name"], row["position"], objimage1, objimage2)].ReadOnly = true;
            }
            Cursor = Cursors.Default;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Dialog5 dg5 = new Dialog5(DB, this, false);
            dg5.ShowDialog(this);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (e.ColumnIndex == 3 || e.ColumnIndex == 4))
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (e.ColumnIndex == 3)
                {
                    DataTable dtResult = new DataTable();
                    if (DT.Select("id=" + id).Count<DataRow>() != 0)
                        dtResult = DT.Select("id=" + id).CopyToDataTable();
                    string fullname = dtResult.Rows[0][1].ToString();
                    string nickname = dtResult.Rows[0][2].ToString();
                    string position = dtResult.Rows[0][3].ToString();
                    JObject json = JObject.Parse(dtResult.Rows[0][4].ToString());
                    string home = json["home"].ToString();
                    string mobile = json["mobile"].ToString();
                    string work = json["work"].ToString();
                    string address = dtResult.Rows[0][5].ToString();
                    string title = dtResult.Rows[0][6].ToString();
                    string department = dtResult.Rows[0][7].ToString();

                    Dialog5 dg5 = new Dialog5(DB, this, true, id,fullname,nickname,position, home,mobile,work,address,title,department);
                    dg5.ShowDialog(this);
                }

                if (e.ColumnIndex == 4)
                    DB.ExecuteReader("DELETE FROM edds_contacts WHERE id=" + id);
                loadDatabase();
                UpdateRows();
            }
        }
    }
}
