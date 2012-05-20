using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcDoodle {

    public class MvcDoodleRazorViewEngine : RazorViewEngine {

        public MvcDoodleRazorViewEngine() {

            base.AreaViewLocationFormats = new string[] { 
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            base.AreaMasterLocationFormats = new string[] { 
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            base.AreaPartialViewLocationFormats = new string[] { 
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            base.ViewLocationFormats = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            base.PartialViewLocationFormats = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            base.MasterLocationFormats = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            base.ViewLocationCache = new TwoLevelViewCache(base.ViewLocationCache);
        }
    }
}