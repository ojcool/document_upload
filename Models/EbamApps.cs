using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLogin.Models
{
    public class EbamApps
    {

        public int Id { get; set; }
        public string AppName { get; set; }
        public string Usage { get; set; }
        public string Host_IP { get; set; }
        public string Public_IP { get; set; }
        public string DB_name { get; set; }
        public string DB_IP { get; set; }
        public string DB_Type { get; set; }
        public string DB_user { get; set; }
        public string Server_name { get; set; }
        public string Type { get; set; }
        public string OS_Type { get; set; }
        public string Status { get; set; }
        public Nullable<int> SupportId { get; set; }
        public Nullable<int> FileId { get; set; }

    }
}


