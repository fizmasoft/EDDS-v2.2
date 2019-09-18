using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fizmasoft.PostgreSQL;

namespace EDDS.Utility
{
    public static class Utils
    {
        public static PgSQL DB { get; set; }
        public static string PREFIX { get; set; }
    }

    public static class User
    {
        public static int ID { get; set; }
        public static string FullName { get; set; }
        public static int ShiftID { get; set; }
        public static Permission Can { get; set; }

        public class Permission
        {
            public bool Read { get; private set; }
            public bool Write { get; private set; }
            public bool Delete { get; private set; }

            public Permission(bool read, bool write, bool delete)
            {
                Read = read;
                Write = write;
                Delete = delete;
            }

            public override string ToString()
            {
                return string.Format("Read: {0}, Write: {1}, Delete: {2}", Read ? "Yes" : "No", Write ? "Yes" : "No", Delete ? "Yes" : "No");
            }
        }
    }

}
