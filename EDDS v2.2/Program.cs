using System;
using System.Windows.Forms;
using Fizmasoft;
using Fizmasoft.Config;
using Fizmasoft.Drawing;
using Fizmasoft.PostgreSQL;
using EDDS.Login;
using EDDS.Utility;
using System.Data;

namespace EDDS
{
    class Program
    {
        private static Auth t;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PostgreSQL.Initizalize();
            Images.Initialize();
            Config.Initialize();

            t = new Auth();
            t.Authenticate += new AuthHandler(T_Authenticate);
            t.Icon = Images.Get("login").ToIcon();

            Section section = Config.Section("Configuration");

            Utils.DB = new PgSQL(Host: section.Key("Server").Value.Get<string>(), Database: "edds");
            Utils.PREFIX = "edds";

            if (Utils.DB.OpenConnection())
            {
                if (t.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm());
                }
            } else
            {
                FS.Notify.Error("Связь потеряно!");
            }
        }

        private static void T_Authenticate(object source, AuthArgs e)
        {
            Form form = source as Form;
            try
            {
                DataTable DT = Utils.DB.ExecuteReader(string.Format("SELECT id, shift_id, full_name, (permission->>'read')::boolean, (permission->>'write')::boolean, (permission->>'delete')::boolean FROM {0}_users WHERE username = '{1}' AND password = MD5(MD5(MD5('{2}'))) AND status", Utils.PREFIX, e.Username, e.Password));
                if (DT.Rows.Count > 0)
                {
                    User.ID = (int)DT.Rows[0][0];
                    User.ShiftID = (int)DT.Rows[0][1];
                    User.FullName = (string)DT.Rows[0][2];
                    User.Can = new User.Permission((bool)DT.Rows[0][3], (bool)DT.Rows[0][4], (bool)DT.Rows[0][5]);
                    Console.WriteLine("{0}; {1}; {2}; {3};", User.ID, User.ShiftID, User.FullName, User.Can);
                    form.DialogResult = DialogResult.OK;
                }
                else
                {
                    FS.Notify.Error(form, "Логин и/или пароль неверно!");
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                FS.Notify.Error(form, "Database error!!!");
            }
        }
    }
}
