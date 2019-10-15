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
    public partial class GranitsiChastey : UserControl
    {
        PgSQL DB;
        public GranitsiChastey(PgSQL _DB)
        {
            InitializeComponent();
            DB = _DB;
            Image image1 = Images.Get("up");
            Image image2 = Images.Get("down");
            Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
            Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));
            btn_up.Image = objimage1;
            btn_down.Image = objimage2;
        }

        private void GranitsiChastey_Load(object sender, EventArgs e)
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

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.MultiSelect = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, GraphicsUnit.Pixel);
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            UpdateColumns();
        }

        private void UpdateColumns()
        {
            DataGridViewImageColumn img1 = new DataGridViewImageColumn();
            DataGridViewImageColumn img2 = new DataGridViewImageColumn();
            DataGridViewImageColumn img3 = new DataGridViewImageColumn();
            img3.HeaderText = "Координата";
            dataGridView1.Columns.Clear();
            dataGridView1.Columns[dataGridView1.Columns.Add("id", "№")].Width = 50;
            dataGridView1.Columns[dataGridView1.Columns.Add("name", "Наименование")].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[dataGridView1.Columns.Add(img3)].Width = 125;
            dataGridView1.Columns[dataGridView1.Columns.Add("args", "Аргументы")].Visible = false;
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

            dataGridView2.Columns.Clear();
            dataGridView2.Columns[dataGridView2.Columns.Add("id", "№")].Width = 50;
            dataGridView2.Columns[dataGridView2.Columns.Add("name", "Наименование")].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[dataGridView2.Columns.Add(new DataGridViewImageColumn())].Width = 125;
            dataGridView2.Columns[dataGridView2.Columns.Add("args", "Аргументы")].Visible = false;
            dataGridView2.Columns[dataGridView2.Columns.Add(new DataGridViewImageColumn())].Width = 25;
            dataGridView2.Columns[dataGridView2.Columns.Add(new DataGridViewImageColumn())].Width = 25;
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(209, 232, 255);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 0, 0);

            UpdateRows();
        }

        public void UpdateRows(int index = 0, int gridnumber=3)
        {
            Cursor = Cursors.WaitCursor;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            DataTable DT = DB.ExecuteReader("SELECT id, name, coordinate, args FROM edds_sections WHERE args::jsonb->'order'->'y'='0' ORDER BY args::jsonb->'order'->'x';");
            DataTable DT2 = DB.ExecuteReader("SELECT id, name, coordinate, args FROM edds_sections WHERE args::jsonb->'order'->'y'='1' ORDER BY args::jsonb->'order'->'x';");
            Image image1 = Images.Get("edit");
            Image image2 = Images.Get("trash");
            Image image3 = Images.Get("check");
            Image image4 = Images.Get("cross");
            Bitmap objimage1 = new Bitmap(image1, new Size(17, 17));
            Bitmap objimage2 = new Bitmap(image2, new Size(17, 17));
            Bitmap objimage3 = new Bitmap(image3, new Size(17, 17));
            Bitmap objimage4 = new Bitmap(image4, new Size(17, 17));
            Bitmap objimage5 = objimage4;
            
                foreach (DataRow row in DT.Rows)
                {
                    if (row["coordinate"] == null)
                        objimage5 = objimage3;
                    dataGridView1.Rows[dataGridView1.Rows.Add(row["id"], row["name"], objimage5, row["args"], objimage1, objimage2)].ReadOnly = true;
                }
                foreach (DataRow row in DT2.Rows)
                {
                    if (row["coordinate"] == null)
                        objimage5 = objimage3;
                    dataGridView2.Rows[dataGridView2.Rows.Add(row["id"], row["name"], objimage5, row["args"], objimage1, objimage2)].ReadOnly = true;
                }
            if (gridnumber == 1)
            {
                dataGridView1.Rows[index].Selected = true;
                dataGridView2.ClearSelection();
            }
            if (gridnumber == 2)
            {
                dataGridView2.Rows[index].Selected = true;
                dataGridView1.ClearSelection();
            }
                
            Cursor = Cursors.Default;
        }

        private void btn_dobavit_Click(object sender, EventArgs e)
        {
            Dialog4 dg4 = new Dialog4(DB, this, false);
            dg4.ShowDialog(this);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.ClearSelection();
            if (e.RowIndex > -1 && (e.ColumnIndex == 4 || e.ColumnIndex == 5))
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (e.ColumnIndex == 4)
                {
                    Dialog4 dg4 = new Dialog4(DB, this, true, name, id);
                    dg4.ShowDialog(this);
                }

                if (e.ColumnIndex == 5)
                    DB.ExecuteReader("DELETE FROM edds_sections WHERE id=" + id);
                UpdateRows();
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id1 = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int index = dataGridView1.SelectedRows[0].Index;
                if (index > 0)
                {
                    string id2 = dataGridView1.Rows[index - 1].Cells[0].Value.ToString();
                    string x1 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id1).Rows[0][0].ToString();
                    string x2 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id2).Rows[0][0].ToString();

                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x2 + "') WHERE id=" + id1);
                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x1 + "') WHERE id=" + id2);
                    UpdateRows(index - 1, 1);
                }
            }

            if (dataGridView2.SelectedRows.Count > 0)
            {
                string id1 = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                int index = dataGridView2.SelectedRows[0].Index;
                if (index > 0)
                {
                    string id2 = dataGridView2.Rows[index - 1].Cells[0].Value.ToString();
                    string x1 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id1).Rows[0][0].ToString();
                    string x2 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id2).Rows[0][0].ToString();

                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x2 + "') WHERE id=" + id1);
                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x1 + "') WHERE id=" + id2);
                    UpdateRows(index - 1, 2);
                }
            }

        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id1 = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int index = dataGridView1.SelectedRows[0].Index;
                if (index < dataGridView1.RowCount - 1)
                {
                    string id2 = dataGridView1.Rows[index + 1].Cells[0].Value.ToString();
                    string x1 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id1).Rows[0][0].ToString();
                    string x2 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id2).Rows[0][0].ToString();

                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x2 + "') WHERE id=" + id1);
                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x1 + "') WHERE id=" + id2);
                    UpdateRows(index + 1, 1);
                }
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string id1 = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                int index = dataGridView2.SelectedRows[0].Index;
                if (index < dataGridView2.RowCount - 1)
                {
                    string id2 = dataGridView2.Rows[index + 1].Cells[0].Value.ToString();
                    string x1 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id1).Rows[0][0].ToString();
                    string x2 = DB.ExecuteReader("SELECT args::jsonb->'order'->'x' FROM edds_sections WHERE id=" + id2).Rows[0][0].ToString();

                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x2 + "') WHERE id=" + id1);
                    DB.ExecuteReader("UPDATE edds_sections SET args = jsonb_set(args, '{order, x}','" + x1 + "') WHERE id=" + id2);
                    UpdateRows(index + 1, 2);
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ClearSelection();
            if (e.RowIndex > -1 && (e.ColumnIndex == 4 || e.ColumnIndex == 5))
            {
                string id = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                string name = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (e.ColumnIndex == 4)
                {
                    Dialog4 dg4 = new Dialog4(DB, this, true, name, id);
                    dg4.ShowDialog(this);
                }

                if (e.ColumnIndex == 5)
                    DB.ExecuteReader("DELETE FROM edds_sections WHERE id=" + id);
                UpdateRows();
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {

        }
    }
}
