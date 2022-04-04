using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.ViewModel
{
    [Serializable]
    public class QuanTriViewModel
    {
        public string UserName { get; set; }

        public string Pw { get; set; }

        public int? MaPQ { get; set; }
    }
}