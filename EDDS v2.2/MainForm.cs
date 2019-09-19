using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fizmasoft.Search;
using Fizmasoft.Drawing;
using Fizmasoft;
using EDDS.Monitoring;

namespace EDDS
{
    public partial class MainForm : Form
    {
        //private Search search;
        private string tmp = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            this.Icon = Images.Get("edds").ToIcon();
            sectionControl1.MoveCrew += new MoveCrewHandler(MoveCrew);

            //SearchAlgorithm.Initizalize();
            //search = new Search(this, Utility.Utils.DB.Clone(), 0.3, 1000) { Dock = DockStyle.Fill };
            //search.SelectedEvent += new SelectedEventHandler(SelectedItem); 
        }

        private void UpdateDataPanel()
        {
            string _t = (Utility.Utils.DB.ExecuteScalar(string.Format("SELECT MD5(string_agg(action_id::text, ',')) FROM {0}_crew;", Utility.Utils.PREFIX)).ToString());
            if (tmp != _t)
            {
                tmp = _t;
                sectionControl1.UpdateDataPanel1(Utility.Utils.DB.ExecuteReader(string.Format("SELECT {0}_sections.id, {0}_sections.name, COUNT(*) total_count, COUNT(*) FILTER (WHERE {0}_crew.action_id IN (2, 9)) left_count FROM {0}_crew JOIN {0}_sections ON {0}_crew.section_id = {0}_sections.id WHERE args->'order'->'y' = '1' GROUP BY {0}_sections.id, {0}_sections.name, {0}_sections.args->'order'->'x' ORDER BY {0}_sections.args->'order'->'x';", Utility.Utils.PREFIX)));
            }
        }

        #region Events

        private void MoveCrew(MoveCrewArgs e)
        {
            Console.WriteLine("From: {0}, To: {1}", e.FromSection, e.ToSection);
        }

        private void SelectedItem(object source, SelectedEventArgs e)
        {
            Console.WriteLine("{0}, {1}, {2}", e.Item.Key, e.Item.Name, e.Item.Table);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateDataPanel();
            //panel.Controls.Add(search);
        }

        private void SectionDataInterval_Tick(object sender, EventArgs e)
        {
            UpdateDataPanel();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new Information.Help().ShowDialog();
        }

        #endregion
    }
}
