using AFModels;
using BDS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BDS.DAO
{
    public class LienHeDAO
    {
        BatDongSanContext db = new BatDongSanContext();
        public List<LienHeViewModel> list()
        {
            List<LienHeViewModel> kq = (from lh in db.LienHes
                                          select new LienHeViewModel
                                          {
                                              MaLienHe = lh.MaLienHe,
                                              HoTen = lh.HoTen,
                                              Email = lh.Email,
                                              TieuDe = lh.TieuDe,
                                              LoiNhan = lh.LoiNhan
        

                                          }).ToList();
            return kq;
        }


        public IEnumerable<LienHe> ListAll(int pageNumber, int pageSize)
        {
            return db.LienHes.OrderByDescending(s => s.MaLienHe).ToPagedList(pageNumber, pageSize);
        }
        public long Insert(LienHe lh)
        {
            db.LienHes.Add(lh);
            db.SaveChanges();
            return lh.MaLienHe;
        }

        public LienHe GetByID(int id)
        {
            return db.LienHes.Find(id);
        }
        public IEnumerable<LienHe> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<LienHe> model = db.LienHes;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TieuDe.Contains(searchString) || x.TieuDe.Contains(searchString));
            }

            return model.OrderByDescending(x => x.MaLienHe).ToPagedList(page, pageSize);
        }


     

       




        public void Reply(string toEmailAddress, string subject, string content)
        {

            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"].ToString());

            string body = content;

            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enableSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}