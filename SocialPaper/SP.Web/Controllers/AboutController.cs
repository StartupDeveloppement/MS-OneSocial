using SP.Model;
using SP.Web.Context;
using SP.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SP.Web.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string sitename)
        {
            ViewBag.Active = "About";

            if (string.IsNullOrEmpty(sitename))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.CanEdit = false;
                UserProfile u = UsersContext.GetUserBySiteName(sitename);

                if (null != u)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        string email = User.Identity.Name;
                        if (email.Equals(u.Email, StringComparison.InvariantCultureIgnoreCase))
                            ViewBag.CanEdit = true;
                    }

                    ViewBag.UserName = u.UserName;
                    ViewBag.SiteName = u.SiteName;
                    ViewBag.ImageUrl = u.Image;
                    ViewBag.CvUrl = u.Cv;
                    ViewBag.Presentation = u.Description;
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Edit()
        {
            UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
            ViewBag.Title = "A propos de " + u.UserName;
            ViewBag.UserName = u.UserName;
            ViewBag.SiteName = u.SiteName;
            ViewBag.ImageUrl = u.Image;
            ViewBag.CvUrl = u.Cv;

            if (null != u)
            {
                string email = User.Identity.Name;
                if (email.Equals(u.Email, StringComparison.InvariantCultureIgnoreCase))
                    ViewBag.CanEdit = true;
            }
            else
                return RedirectToAction("Index", "Home");
            return View(new AboutEdit { Description = u.Description, Id = u.Id });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AboutEdit pPresentation)
        {
            UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
            ViewBag.Title = "A propos de " + u.UserName;
            ViewBag.UserName = u.UserName;
            ViewBag.SiteName = u.SiteName;
            ViewBag.ImageUrl = u.Image;
            ViewBag.CvUrl = u.Cv;

            if (null != u)
            {
                UsersContext.UpdateUserDescription(u.Id, pPresentation.Description);
                return RedirectToAction("Index", "About", new { sitename = u.SiteName });
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImageUpload(HttpPostedFileBase UploadPicture)
        {
            UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);

            if (null != UploadPicture && UploadPicture.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads/Users/Images"),
                                               Path.GetFileName(UploadPicture.FileName));

                UsersContext.UpdateUserImage(u.Id, Path.Combine("../Uploads/Users/Images", Path.GetFileName(UploadPicture.FileName)));

                UploadPicture.SaveAs(filePath);
            }
            return RedirectToAction("Index", "About", new { sitename = u.SiteName });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CvUpload(HttpPostedFileBase UploadCv)
        {
            UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);

            if (null != UploadCv && UploadCv.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads/Users/Cv"),
                                               Path.GetFileName(UploadCv.FileName));

                UsersContext.UpdateUserCv(u.Id, Path.Combine("../Uploads/Users/Cv", Path.GetFileName(UploadCv.FileName)));

                UploadCv.SaveAs(filePath);
            }
            return RedirectToAction("Index", "About", new { sitename = u.SiteName });
        }

        [AllowAnonymous]
        public ActionResult CV(string sitename)
        {
            if (string.IsNullOrEmpty(sitename))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.CanEdit = false;
                UserProfile u = UsersContext.GetUserBySiteName(sitename);

                if (null != u)
                {
                    ViewBag.Title = "CV de " + u.UserName;
                    ViewBag.UserName = u.UserName;
                    ViewBag.SiteName = u.SiteName;
                    ViewBag.CvUrl = u.Cv;
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
