using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plannr.Controllers
{
    public class OptionController : Controller
    {
        //
        // GET: /Option/

        public ActionResult Index()
        {
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_Index");
            }
        }

        public ActionResult ChangeData()
        {
            return View();
        }


        public ActionResult ChangeUserName()
        {
            return View();
        }

        public ActionResult ChangePwd()
        {
            return View();
        }



    }
}
