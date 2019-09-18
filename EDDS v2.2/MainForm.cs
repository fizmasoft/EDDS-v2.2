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
        private Search search;
        private SectionControl SC;

        public MainForm()
        {
            InitializeComponent();
            SC = new SectionControl() { Dock = DockStyle.Fill };
            this.Icon = Images.Get("edds").ToIcon();
            //SearchAlgorithm.Initizalize();
            //search = new Search(this, Utility.Utils.DB.Clone(), 0.3, 1000) { Dock = DockStyle.Fill };
            //search.SelectedEvent += new SelectedEventHandler(SelectedItem);
        }

        private void SelectedItem(object source, SelectedEventArgs e)
        {
            Console.WriteLine("{0}, {1}, {2}", e.Item.Key, e.Item.Name, e.Item.Table);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(SC);
            //panel.Controls.Add(search);
        }
    }
}
