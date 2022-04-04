using AFModels;
using BDS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.DAO
{
    public class LoginDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<LoginViewModel> Login()
        {
            List<LoginViewModel> kq = (from a in db.QuanTris
                                       select new LoginViewModel
                                       {
                                           UserName = a.UserName,
                                           Pw = a.Pw,
                                           MaPQ = a.MaPQ

                                       }).ToList();

            return kq;

        }

       
    }
}

