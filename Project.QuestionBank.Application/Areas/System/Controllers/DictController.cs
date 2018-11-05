using Project.QuestionBank.Core.Service.Interface;
using Project.QuestionBank.Core.ViewModel;
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


        #endregion
    }
}