using Project.QuestionBank.Core.Domain;
using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Core.ViewModel;
using Project.QuestionBank.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.QuestionBank.Application.Areas.System.Controllers
{
    public class DictController : Controller
    {
        private readonly IDictTypeService _dictTypeService;
        private readonly IDictItemService _dictItemService;

        public DictController(IDictTypeService dictTypeService, IDictItemService dictItemService)
        {
            this._dictTypeService = dictTypeService;
            this._dictItemService = dictItemService;
        }

        // GET: System/Dict
        public ActionResult Index()
        {
            return View();
        }

        #region 类型

        /// <summary>
        /// 获取类型目录树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> TreeList()
        {
            var data = await _dictTypeService.Query(m => m.DeleteMark == 0, "SortCode asc");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 明细


        /// <summary>
        /// 获取类型目录树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ItemByType(string typeId, int page, int size)
        {
            var data = await _dictItemService.QueryPageAndCount(m => m.DictId == typeId && m.DeleteMark == 0, page, size, "SortCode asc");

            var res = new PageViewModel
            {
                code = "",
                msg = "",
                count = data.Item2,
                data = data.Item1
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ItemForm()
        {
            return View();
        }

        /// <summary>
        /// 信息新增及编辑
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ItemForm(int? key, DictItem model)
        {
            var jsonResult = new ResultModel();
            if (!key.HasValue)
            {
                model.DictItemId = Guid.NewGuid().ToString("N");
                model.QuickQuery = Str.ConvertPinYin(model.ItemName).ToUpper();
                model.SimpleSpelling = Str.PinYin(model.ItemName);
                model.DeleteMark = 0;
                model.CreateTime = DateTime.Now;
                var res = await _dictItemService.Add(model);
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
                //var currentuser = await _sysUserService.QueryById(key);
                //currentuser.UpdateTime = DateTime.Now;
                //currentuser.UserName = model.UserName;
                //currentuser.RealName = model.RealName;
                //currentuser.Age = model.Age;
                //currentuser.Email = model.Email;
                //currentuser.Address = model.Address;
                //currentuser.Remark = model.Remark;
                //var res = await _sysUserService.Update(currentuser);
                //if (res)
                //{
                //    jsonResult.status = "1";
                //    jsonResult.msg = "编辑信息成功";
                //}
                //else
                //{
                //    jsonResult.status = "0";
                //    jsonResult.msg = "编辑信息失败";
                //}
            }
            return Json(jsonResult);
        }


        #endregion
    }
}