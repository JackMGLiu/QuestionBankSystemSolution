using System.Threading.Tasks;
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
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddUserModel model)
        {
            var jsonResult = new ResultModel();
            var user = model.MapTo<AddUserModel, SysUser>();
            var res = await _sysUserService.Add(user);
            if (res > 0)
            {
                jsonResult.status = "1";
                jsonResult.msg = "新增成功";
            }
            else
            {
                jsonResult.status = "0";
                jsonResult.msg = "新增失败";
            }
            return Json(jsonResult);
        }

        [HttpGet]
        [Route("/users")]
        public async Task<ActionResult> Users(string keyword, int page, int size)
        {
            //var data = await _sysUserService.QueryPageAndCountBySql("", page, size, "id desc");
            int userscount = 0;
            if (!string.IsNullOrEmpty(keyword))
            {
                var all = await _sysUserService.Query(m => m.UserName.Contains(keyword) || m.RealName.Contains(keyword), "id desc");
                userscount = all.Count;

            }
            else
            {
                var all = await _sysUserService.Query("", "id desc");
                userscount = all.Count;
            }
            //var users = data.Item1.MapTo<List<SysUser>, List<SysUserViewModel>>();
            var users = await _sysUserService.GetUserPageList(keyword, page, size);

            var res = new PageViewModel
            {
                code = "",
                msg = "",
                //count = data.Item2,
                count = userscount,
                data = users
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}