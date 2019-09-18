using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDDS.Monitoring
{
    public partial class SectionControl : UserControl
    {
        public SectionControl()
        {
            InitializeComponent();
        }

        public void UpdateDataPanel1(DataTable DT)
        {
            Color[] color = new Color[2];
            Label label;
            flowLayoutPanel1.Controls.Clear();
            foreach (DataRow row in DT.Rows)
            {
                if ((long)row["left_count"] == 0)
                {
                    color[0] = Color.Red;
                    color[1] = Color.Yellow;
                } else if ((long)row["total_count"] - (long)row["left_count"] > 0)
                {
                    color[0] = Color.Gold;
                    color[1] = Color.Black;
                } else
                {
                    color[0] = Color.PaleGreen;
                    color[1] = Color.Black;
                }

                label = new Label()
                {
                    Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold),
                    AllowDrop = true,
                    BackColor = color[0],
                    ForeColor = color[1],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = string.Format("{0}\n({1})", row["name"], row["left_count"]),
                    Tag = string.Format("{0}\n({1})", 1, 2),
                    Margin = new Padding(3),
                    Size = new Size(88, 48),
                    AutoSize = false,
                    BorderStyle = BorderStyle.FixedSingle
                };
                label.DragDrop += new DragEventHandler(OnDragDrop);
                label.DragEnter += new DragEventHandler(OnDragEnter);
                label.MouseDown += new MouseEventHandler(OnMouseDown);
                flowLayoutPanel1.Controls.Add(label);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Label lb = sender as Label;
            /*MoveItem.From = lb.Tag.ToString().Split('|').FirstOrDefault().ToInt();
            if (lb.Tag.ToString().Split('|').LastOrDefault().ToInt() != 0)
                lb.DoDragDrop(lb, DragDropEffects.Move);*/
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            Label lb = sender as Label;
            /*MoveItem.To = lb.Tag.ToString().Split('|').FirstOrDefault().ToInt();
            new DivisionCars(DB, MoveItem.From).ShowDialog();
            if (DB.Update("UPDATE chast_ekip SET kod_chast = {0}, kod_chast_old = {1}, ekipaj = 'Экипаж дислок.(' || ekipaj || ')' WHERE kod = {2}".TextFormat(MoveItem.To, MoveItem.From, MoveItem.Object)))
            {
                UpdateDivisionStatus();
            }*/
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
