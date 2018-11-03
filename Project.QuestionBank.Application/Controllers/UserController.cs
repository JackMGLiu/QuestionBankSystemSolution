using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Project.QuestionBank.Core.AutoMapper;
using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Core.ViewModel;
using Project.QuestionBank.Core.ViewModel.SysUser;
using Project.QuestionBank.Utils.Helper;
using Project.QuestionBank.Utils.Security;

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
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 信息编辑及删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Form(int? key, UserFormModel model)
        {
            var jsonResult = new ResultModel();
            if (!key.HasValue)
            {
                model.Password = ConfigHelper.GetConfig("DefaultPassword").Md5Hash();
                model.CreateTime = DateTime.Now;
                var user = model.MapTo<UserFormModel, SysUser>();
                var res = await _sysUserService.Add(user);
                if (res > 0)
                {
                    jsonResult.status = "1";
                    jsonResult.msg = "新增信息成功";
                }
                else
                {
                    jsonResult.status = "0";
                    jsonResult.msg = "新增信息失败";
                }
            }
            else
            {
                var currentuser = await _sysUserService.QueryById(key);
                currentuser.UpdateTime = DateTime.Now;
                currentuser.UserName = model.UserName;
                currentuser.RealName = model.RealName;
                currentuser.Age = model.Age;
                currentuser.Email = model.Email;
                currentuser.Address = model.Address;
                currentuser.Remark = model.Remark;
                var res = await _sysUserService.Update(currentuser);
                if (res)
                {
                    jsonResult.status = "1";
                    jsonResult.msg = "编辑信息成功";
                }
                else
                {
                    jsonResult.status = "0";
                    jsonResult.msg = "编辑信息失败";
                }
            }
            return Json(jsonResult);
        }

        /// <summary>
        /// 查询分页信息
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="page">页码</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        [HttpGet]
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

        /// <summary>
        /// 查询信息实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetUserModel(int key)
        {
            var data = await _sysUserService.QueryById(key);
            var res = data.ToModel();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete()
        {
            var key = Request["key"];
            var jsonResult = new ResultModel();
            var res = await _sysUserService.DeleteById(key);
            if (res)
            {
                jsonResult.status = "1";
                jsonResult.msg = "删除信息成功";
            }
            else
            {
                jsonResult.status = "0";
                jsonResult.msg = "删除信息失败";
            }
            return Json(jsonResult);
        }

        /// <summary>
        /// 批量删除信息
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteList()
        {
            var key = Request["key"];
            var jsonResult = new ResultModel();
            var res = await _sysUserService.DeleteByIdArray(null);
            if (res)
            {
                jsonResult.status = "1";
                jsonResult.msg = "删除信息成功";
            }
            else
            {
                jsonResult.status = "0";
                jsonResult.msg = "删除信息失败";
            }
            return Json(jsonResult);
        }
    }
}