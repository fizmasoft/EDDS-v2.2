﻿using Fizmasoft.PostgreSQL;
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

namespace EDDS.Information
{
    public partial class Helpform : Form
    {
        PgSQL DB;
        public Helpform()
        {
            DB = Utils.DB;
            InitializeComponent();
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
                        panel1.Controls.Add(new Helpfolder.KategoriObyektov(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Категории улиц":
                        panel1.Controls.Add(new Helpfolder.KategoriUlits(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Категории техники":
                        panel1.Controls.Add(new Helpfolder.KategoriTexniki(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Границы частей":
                        panel1.Controls.Add(new Helpfolder.GranitsiChastey(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Сигнал":
                        panel1.Controls.Add(new Helpfolder.Signal(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Марка техники":
                        panel1.Controls.Add(new Helpfolder.MarkaTexniki(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Местонахождение экипажа":
                        panel1.Controls.Add(new Helpfolder.MestonaxojdeniyeEkipaja(DB) { Dock = DockStyle.Fill });
                        break;
                    case "График дежурств":
                        panel1.Controls.Add(new Helpfolder.GrafikDejurstv { Dock = DockStyle.Fill });
                        break;
                    case "ВЧПБ":
                        panel1.Controls.Add(new Helpfolder.VChPB { Dock = DockStyle.Fill });
                        break;
                    case "Дежурный по штабу":
                        panel1.Controls.Add(new Helpfolder.DejurniyPoShtabu(DB) { Dock = DockStyle.Fill });
                        break;
                    case "Телефонный справочник":
                        panel1.Controls.Add(new Helpfolder.TelefonniySpravochnik { Dock = DockStyle.Fill });
                        break;

                }
            }

            Cursor = Cursors.Default;
        }

       
    }
}
