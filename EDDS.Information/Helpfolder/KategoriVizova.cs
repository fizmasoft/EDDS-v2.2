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
using Fizmasoft.Drawing;
using EDDS.Utility;

namespace EDDS.Information.Helpfolder
{
    public partial class KategoriVizova : UserControl
    {
        private PgSQL DB;
        DataTable DT;

        public KategoriVizova(PgSQL _DB)
        {
            InitializeComponent();
            DB = _DB;
            Image image = Images.Get("plus");
            Bitmap objimage = new Bitmap(image, new Size(17, 17));
            btn_dobavit.Image = objimage;
            btn_dobavit.ImageAlign = ContentAlignment.MiddleLeft;
            btn_dobavit.TextAlign = ContentAlignment.MiddleRight;
            if (User.Can.Write == false)
                btn_dobavit.Enabled = false;
            loadDatabase();
        }

        public void loadDatabase()
        {
            DT = DB.ExecuteReader("SELECT id, number, auto FROM edds_call_type ORDER BY auto, number ASC;");
        }

        private void KategoriVizova_Load(object sender, EventArgs e)
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
            dataGridView1.Columns[dataGridView1.Columns.Add("number", "Номер")].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[dataGridView1.Columns.Add("auto", "Авто")].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            else if (DT.Select("number=" + textBox1.Text).Count<DataRow>() != 0)
                dtResult = DT.Select("number=" + textBox1.Text).CopyToDataTable();
            Image image1 = Images.Get("edit");
            Image image2 = Images.Get("trash");
            Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
            Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));

            foreach (DataRow row in dtResult.Rows)
            {
                string vizov = "Вызов";
                if (Convert.ToBoolean(row["auto"]))
                    vizov = "Автовызов";
                dataGridView1.Rows[dataGridView1.Rows.Add(row["id"], row["number"], vizov, objimage1, objimage2)].ReadOnly = true;
            }
            Cursor = Cursors.Default;
        }

        private void btn_dobavit_Click(object sender, EventArgs e)
        {
            Dialog2 dg2 = new Dialog2("Добавление вызовы", DB, this, false, 1, false);
            dg2.ShowDialog(this);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (e.ColumnIndex == 3 || e.ColumnIndex == 4))
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                int number = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                bool checkstate = false;
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Автовызов")
                    checkstate = true;
                if (e.ColumnIndex == 3)
                {
                    Dialog2 dg2 = new Dialog2("Обновление вызовы", DB, this, true, number, checkstate, id);
                    dg2.ShowDialog(this);
                    loadDatabase();
                    UpdateRows();
                }

                if (e.ColumnIndex == 4)
                {
                    DialogResult myResult;
                    myResult = MessageBox.Show("Вы действительно хотите удалить\n" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (myResult == DialogResult.OK)
                    {
                        DB.ExecuteReader("DELETE FROM edds_call_type WHERE id=" + id);
                        MessageBox.Show("Удалено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDatabase();
                        UpdateRows();
                    }
                }   
               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateRows();
            //if (textBox1.Text == "")
            //    UpdateRows();
            //else
            //{
            //    Cursor = Cursors.WaitCursor;
            //    dataGridView1.Rows.Clear();
            //    DataTable DT = DB.ExecuteReader("SELECT id, number, auto FROM edds_call_type WHERE number = '" + textBox1.Text + "' ORDER BY auto, number ASC;");
            //    Image image1 = Images.Get("edit");
            //    Image image2 = Images.Get("trash");
            //    Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
            //    Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));

            //    foreach (DataRow row in DT.Rows)
            //    {
            //        string vizov = "Вызов";
            //        if (Convert.ToBoolean(row["auto"]))
            //            vizov = "Автовызов";
            //        dataGridView1.Rows[dataGridView1.Rows.Add(row["id"], row["number"], vizov, objimage1, objimage2)].ReadOnly = true;
            //    }
            //    Cursor = Cursors.Default;
            //}
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }           
        }
    }
}
