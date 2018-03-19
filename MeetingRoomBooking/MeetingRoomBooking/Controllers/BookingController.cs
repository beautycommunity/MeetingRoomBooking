using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MeetingRoomBooking.DATA;
using MeetingRoomBooking.Models;
//using static MeetingRoomBooking.Models.Documents;

namespace MeetingRoomBooking.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {

            dbMeetingRoomBookingDataContext db = new dbMeetingRoomBookingDataContext();



            var model = new Documents();

            foreach(var rs in db.MAS_TIMEs)
            {
                Documents.CheckBox chk = new Documents.CheckBox();
                chk.Checked = false;
                chk.topic = rs.S_TIME + "-"+ rs.E_TIME;
                chk.id = rs.TMCODE;

                model.getchk.Add(chk);
            }

            foreach (var rs in db.MAS_DRINKs)
            {
                Documents.CheckBox chk = new Documents.CheckBox();
                chk.Checked = false;
                chk.topic = rs.NAME;
                chk.id = rs.TDCODE;

                model.drinkchk.Add(chk);
            }

            foreach (var rs in db.MAS_TOOLs)
            {
                Documents.CheckBox chk = new Documents.CheckBox();
                chk.Checked = false;
                chk.topic = rs.NAME;
                chk.id = rs.TLCODE;

                model.toolkchk.Add(chk);
            }


            return View(model);
        }



    }
}