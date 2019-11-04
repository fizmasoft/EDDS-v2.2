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

namespace EDDS.Information.Helpfolder
{
    public partial class GrafikDejurstv : UserControl
    {
        PgSQL DB;
        DataTable DT;
        DataTable DTuser;
        public GrafikDejurstv(PgSQL _DB)
        {
            InitializeComponent();
            DB = _DB;
            LoadDatabase();
            Loadtabpanel();
        }

        public void LoadDatabase()
        {
            DT = DB.ExecuteReader("SELECT * FROM edds_shift ORDER BY id");
            DTuser = DB.ExecuteReader("SELECT * FROM edds_users ORDER BY id");
        }

        public void Loadtabpanel()
        {
            tabControl1.Controls.Clear();
            string[] shifts = new string[DT.Rows.Count];
            for(int i = 0; i < DT.Rows.Count; i++)
            {
                shifts[i] = DT.Rows[i][1].ToString();
            }
            foreach (DataRow row in DT.Rows)
            {
                string id = row["id"].ToString();
                TabPage tabpg = new TabPage();
                tabpg.Text = row["name"].ToString();
                tabpg.Controls.Add(new NodeTabPages(DB, id, this) { Dock = DockStyle.Fill});
                tabControl1.Controls.Add(tabpg);
            }
        }
    }
}
