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

namespace EDDS.Information.Helpfolder
{
    public partial class Signal : UserControl
    {
        PgSQL DB;
        public Signal(PgSQL _DB)
        {
            InitializeComponent();
            DB = _DB;
        }

        private void Signal_Load(object sender, EventArgs e)
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
            dataGridView1.Columns[dataGridView1.Columns.Add("Name", "Наименование")].Width = 300;
            dataGridView1.Columns[dataGridView1.Columns.Add("Description", "Описание")].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewImageColumn img1 = new DataGridViewImageColumn();
            DataGridViewImageColumn img2 = new DataGridViewImageColumn();
            dataGridView1.Columns[dataGridView1.Columns.Add(img1)].Width = 25;
            dataGridView1.Columns[dataGridView1.Columns.Add(img2)].Width = 25;
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
            DataTable DT = DB.ExecuteReader("SELECT id, name, \"desc\" as description FROM edds_keywords ORDER BY id");
            Image image1 = Images.Get("edit");
            Image image2 = Images.Get("trash");
            Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
            Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));

            foreach (DataRow row in DT.Rows)
            {
                dataGridView1.Rows[dataGridView1.Rows.Add(row["id"], row["name"], row["description"], objimage1, objimage2)].ReadOnly = true;
            }
            dataGridView1.Select();
            Cursor = Cursors.Default;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (e.ColumnIndex == 3 || e.ColumnIndex == 4))
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string description = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (e.ColumnIndex == 3)
                {
                    Dialog3 dg3 = new Dialog3(DB, this, true, name, description, id);
                    dg3.ShowDialog(this);
                }

                if (e.ColumnIndex == 4)
                    DB.ExecuteReader("DELETE FROM edds_keywords WHERE id=" + id);
                UpdateRows();
            }
        }

        private void btn_dobavit_Click(object sender, EventArgs e)
        {
           Dialog3 dg3 = new Dialog3(DB, this, false);
           dg3.ShowDialog(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            dataGridView1.Rows.Clear();
            DataTable DT = DB.ExecuteReader("SELECT id, name, \"desc\" FROM edds_keywords WHERE lower(name) LIKE lower('%" + textBox1.Text + "%') ORDER BY id");

            Image image1 = Images.Get("edit");
            Image image2 = Images.Get("trash");
            Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
            Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));
            foreach (DataRow row in DT.Rows)
            {
                dataGridView1.Rows[dataGridView1.Rows.Add(row["id"], row["name"], row["desc"], objimage1, objimage2)].ReadOnly = true;
            }
            Cursor = Cursors.Default;
        }
    }
}
