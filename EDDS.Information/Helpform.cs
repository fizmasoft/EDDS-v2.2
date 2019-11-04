using Fizmasoft.PostgreSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDDS.Utility;
using Fizmasoft.Drawing;

namespace EDDS.Information
{
    public partial class Helpform : Form
    {
        PgSQL DB;
        public Helpform()
        {
            DB = Utils.DB;
            InitializeComponent();
            //if (User.Can.Read == false)
            //    listView1.Items.Clear();
            label1.Text = "";
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ListView listview = sender as ListView;

            foreach(ListViewItem item in listview.SelectedItems)
            {
                panel1.Controls.Clear();
                label1.Text = item.Text;
                switch(item.Text)
                {
                    case "Вызов":
                        panel1.Controls.Add(new Helpfolder.Vizov { Dock = DockStyle.Fill });
                        break;
                    case "Авто вызов":
                        panel1.Controls.Add(new Helpfolder.Avtovizov { Dock = DockStyle.Fill });
                        break;
                    case "Категории вызова":
                        panel1.Controls.Add(new Helpfolder.KategoriVizova(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Категории объекта":
                        panel1.Controls.Add(new Helpfolder.Node1(DB, "edds_object_type", "категории объекта") { Dock = DockStyle.Fill });
                        break;
                    case "Категории улиц":
                        panel1.Controls.Add(new Helpfolder.Node1(DB, "edds_street_type" ,"категории улиц") { Dock = DockStyle.Fill });
                        break;
                    case "Категории техники":
                        panel1.Controls.Add(new Helpfolder.Node1(DB, "edds_technics_type", "категории техники") { Dock = DockStyle.Fill });
                        break;
                    case "Границы частей":
                        panel1.Controls.Add(new Helpfolder.GranitsiChastey(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Сигнал":
                        panel1.Controls.Add(new Helpfolder.Signal(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Марка техники":
                        panel1.Controls.Add(new Helpfolder.Node1(DB, "edds_car_model", "марка техники") { Dock = DockStyle.Fill });
                        break;
                    case "Местонахождение экипажа":
                        panel1.Controls.Add(new Helpfolder.Node1(DB, "edds_crew_actions", "местонахождение экипажа") { Dock = DockStyle.Fill });
                        break;
                    case "График дежурств":
                        panel1.Controls.Add(new Helpfolder.GrafikDejurstv(DB) { Dock = DockStyle.Fill });
                        break;
                    case "ВЧПБ":
                        panel1.Controls.Add(new Helpfolder.VChPB { Dock = DockStyle.Fill });
                        break;
                    case "Дежурный по штабу":
                        panel1.Controls.Add(new Helpfolder.Node1(DB, "edds_main_duty", "дежурный по штабу") { Dock = DockStyle.Fill });
                        break;
                    case "Телефонный справочник":
                        panel1.Controls.Add(new Helpfolder.TelefonniySpravochnik(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Список дежурств":
                        panel1.Controls.Add(new Helpfolder.Node1(DB, "edds_shift", "список дежурств") { Dock = DockStyle.Fill });
                        break;
                }
            }

            Cursor = Cursors.Default;
        }

       
    }
}
