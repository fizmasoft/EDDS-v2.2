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
        public event MoveCrewHandler MoveCrew;

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
                    Tag = new SectionCrew((int)row["id"], (long)row["left_count"], (long)row["total_count"]),
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
            SectionCrew sc = (SectionCrew)lb.Tag;
            MoveItem.From = sc.ID;

            if (sc.LeftCount != 0)
                lb.DoDragDrop(lb, DragDropEffects.Move);
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            Label lb = sender as Label;
            SectionCrew sc = (SectionCrew)lb.Tag;
            MoveItem.To = sc.ID;

            MoveCrew?.Invoke(new MoveCrewArgs(MoveItem.From, MoveItem.To));
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            Label lb = sender as Label;
            SectionCrew sc = (SectionCrew)lb.Tag;

            if (sc.ID != MoveItem.From && sc.LeftCount != sc.TotalCount)
                e.Effect = DragDropEffects.Move;
        }
    }

    public delegate void MoveCrewHandler(MoveCrewArgs e);

    public class MoveCrewArgs : EventArgs
    {
        public int FromSection { get; private set; }
        public int ToSection { get; private set; }

        public MoveCrewArgs(int fromsection, int tosection)
        {
            FromSection = fromsection;
            ToSection = tosection;
        }
    }

    public class SectionCrew
    {
        public SectionCrew(int id, long leftcount, long totalcount)
        {
            ID = id;
            LeftCount = leftcount;
            TotalCount = totalcount;
        }

        public int ID { get; private set; }
        public long LeftCount { get; private set; }
        public long TotalCount { get; private set; }
    }

    public static class MoveItem
    {
        public static int From { get; set; }
        public static int To { get; set; }
    }
}
