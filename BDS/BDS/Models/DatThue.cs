using AFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.Models
{
    public class DatThue
    {
        BatDongSanContext db = new BatDongSanContext();
        public int? iMaBDS { get; set; }
        public string iTenBDS { get; set; }
        public int? iMaNhom { get; set; }
        public int? iMaLoaiBDS { get; set; }
        public DatThue(int MaBDS)
        {
            iMaBDS = MaBDS;
            BatDongSan tv = db.BatDongSans.Single(n => n.MaBDS == iMaBDS);
            iTenBDS = tv.TenBDS;
            iMaNhom = tv.MaNhom;
            iMaLoaiBDS = tv.MaLoaiBDS;
        }

    }
}