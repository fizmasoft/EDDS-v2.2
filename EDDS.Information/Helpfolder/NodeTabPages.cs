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
    public partial class NodeTabPages : UserControl
    {
        PgSQL DB;
        DataTable DT;
        GrafikDejurstv GD;
        string shiftid;
        public NodeTabPages(PgSQL _DB, string _shiftid, GrafikDejurstv _GD)
        {
            InitializeComponent();
            DB = _DB;
            GD = _GD;
            shiftid = _shiftid;

            Image image = Images.Get("plus");
            Bitmap objimage = new Bitmap(image, new Size(17, 17));
            btn_dobavit.Image = objimage;
            btn_dobavit.ImageAlign = ContentAlignment.MiddleLeft;
            btn_dobavit.TextAlign = ContentAlignment.MiddleRight;
            if (User.Can.Write == false)
                btn_dobavit.Enabled = false;
            loadDatabase();
            InitializeTable();
        }

        public void loadDatabase()
        {
            DT = DB.ExecuteReader("SELECT * FROM edds_users ORDER BY id");
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
            DataTable dtResult2 = new DataTable();
            if (DT.Select("shift_id=" + shiftid).Count<DataRow>() != 0)
                dtResult = DT.Select("shift_id=" + shiftid).CopyToDataTable();
            if (dtResult.Rows.Count > 0)
            {
                if (dtResult.Select("full_name LIKE '%" + textBox1.Text + "%'").Count<DataRow>() != 0)
                    dtResult2 = dtResult.Select("full_name LIKE '%" + textBox1.Text + "%'", "id").CopyToDataTable();
                Image image1 = Images.Get("edit");
                Image image2 = Images.Get("trash");
                Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
                Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));
                foreach (DataRow row in dtResult2.Rows)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Add(row["id"], row["full_name"], objimage1, objimage2)].ReadOnly = true;
                }              
            }
            Cursor = Cursors.Default;
        }

        private void btn_dobavit_Click(object sender, EventArgs e)
        {
            DialogDejurstv dgD = new DialogDejurstv(DB, "Добавление ", this, GD, false, false);
            dgD.ShowDialog(this);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (e.ColumnIndex == 2)
                {
                    DialogDejurstv dgD = new DialogDejurstv(DB, "Обновление", this, GD ,true, false, id);
                    dgD.ShowDialog(this);
                    loadDatabase();
                    UpdateRows();
                }

                if (e.ColumnIndex == 3)
                {
                    DialogResult myResult;
                    myResult = MessageBox.Show("Вы действительно хотите удалить\n" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (myResult == DialogResult.OK)
                    {
                        DB.ExecuteReader("DELETE FROM edds_users WHERE id=" + id);
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
        }
    }
}
