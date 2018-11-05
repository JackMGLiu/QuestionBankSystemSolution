using Project.QuestionBank.Core.Service.Interface;
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

        public DictController(IDictTypeService dictTypeService)
        {
            this._dictTypeService = dictTypeService;
        }

        // GET: System/Dict
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> TreeList()
        {
            var data = await _dictTypeService.Query(m => m.DeleteMark == 0, "SortCode asc");
            return Json(data,JsonRequestBehavior.AllowGet);
        }
    }
}