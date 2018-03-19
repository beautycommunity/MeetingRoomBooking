using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MeetingRoomBooking.DATA;
using MeetingRoomBooking.Controllers;
using MeetingRoomBooking.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Models
{
    public class Documents
    {
   
        public List<CheckBox> getchk { get; set; }
        public List<CheckBox> drinkchk { get; set; }
        public List<CheckBox> toolkchk { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SDATE { get; set; }
        public string STCODE { get; set; }

        public SelectList ROOM_LIST { get; set; }
        [Required(ErrorMessage = "กรุณาเลือกสถานะงาน")]
        [Display(Name = "สถานะ")]
        public int? STATUS_ID { get; set; }


        public Documents()
        {
            dbMeetingRoomBookingDataContext db = new dbMeetingRoomBookingDataContext();

            this.getchk = new List<CheckBox>();
            this.drinkchk = new List<CheckBox>();
            this.toolkchk = new List<CheckBox>();
            this.ROOM_LIST = new SelectList(db.MAS_ROOMs, "RMCODE", "RMNAME");
        }


        public class CheckBox
        {
            public string topic { get; set; }
            public string id { get; set; }
            public bool Checked { get; set; }
        }

        public string DocRuning()
        {
            string docrun = "";
            string _stcode = "";

            LoginController _log = new LoginController();

            _stcode = GetStcode("bbStcode");
            //useronline = _stcode;

            using (dbMeetingRoomBookingDataContext db = new dbMeetingRoomBookingDataContext())
            {
                var doc = db.MP_GET_ID(_stcode, "RB").FirstOrDefault();

                docrun = doc.Column1;
            }
            return (docrun);
        }

        public string GetStcode(string key)
        {
            string value = string.Empty;
            HttpCookie cookie =  HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
            {
                value = cookie.Value;
            }

            return value;
            //string cookievalue = string.Empty;

            //if (Request.Cookies["bbStcode"] != null)
            //{
            //    cookievalue = Request.Cookies["bbStcode"].Value.ToString();
            //}

            //return cookievalue;

        }

        public string GetStname(string _stcode)
        {
            string value = string.Empty;
            using (Data_UserDataContext db = new Data_UserDataContext())
            {
                var stname = db.MAS_USERs.Where(s => s.STCODE == _stcode).FirstOrDefault();

                value = stname.FNAME + " " + stname.LNAME;
            }
            return value;
        }

        public string GetDP(string _stcode)
        {
            string value = string.Empty;
            using (Data_UserDataContext db = new Data_UserDataContext())
            {
                var stname = from a in db.MAS_USERs
                             join b in db.MAS_DEPs on a.D_ID equals b.DP_ID
                             where a.STCODE == _stcode
                             select new { DPNAME = b.DEPARTMENT };

                var rs = stname.FirstOrDefault();

                value = rs.DPNAME;
            }
            return value;
        }

    }


}