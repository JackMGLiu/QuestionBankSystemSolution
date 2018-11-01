using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Project.QuestionBank.Core.AutoMapper;
using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Core.ViewModel;
using Project.QuestionBank.Core.ViewModel.SysUser;

namespace Project.QuestionBank.Application.Controllers
{
    public class UserController : Controller
    {
        private readonly ISysUserService _sysUserService;

        public UserController(ISysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/users")]
        public async Task<ActionResult> Users(int page, int size)
        {
            var data = await _sysUserService.QueryPageAndCountBySql("", page, size, "id desc");
            //var all = await _sysUserService.Query("", "id desc");
            var users = data.Item1.MapTo<List<SysUser>, List<SysUserViewModel>>();
            var res = new PageViewModel
            {
                code = "",
                msg = "",
                count = data.Item2,
                data = users
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}