using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.QuestionBank.Core.ViewModel
{
    public class ResultModel
    {
        public ResultModel()
        {
            data = null;
            backurl = "";
        }

        public string status { get; set; }
        public string msg { get; set; }
        public string backurl { get; set; }
        public object data { get; set; }
    }
}
