using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Project.QuestionBank.Core.AutoMapper;
using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Core.ViewModel.SysUser;

namespace Project.QuestionBank.Application.Controllers
{
    public class DemoController : Controller
    {
        private readonly ISysUserService _sysUserService;

        public DemoController(ISysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        // GET: Demo
        public async Task<ActionResult> Test1()
        {
            //var data = await _sysUserService.Query(m=>m.Id==2);
            var data = await _sysUserService.QueryById(2);
            var res = data.MapTo<SysUser,SysUserViewModel>();
            return Json(res, JsonRequestBehavior.AllowGet);          
        }

        public async Task<ActionResult> Test2()
        {
            //var data = await _sysUserService.Query(m=>m.Id==2);
            var data = await _sysUserService.Query("Id <= 12 and Id >= 5","Age desc");
            var res = data.ToModelList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Test3()
        {
            var res = _sysUserService.GetUserList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Test4(int page,int size)
        {
            var res = await _sysUserService.GetUserPageList(page, size);
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> AddUser([FromBody]SysUser user)
        {
            //var data = await _sysUserService.Add(user);
            return Json(new{msg="添加成功",data= user });
        }
    }
}