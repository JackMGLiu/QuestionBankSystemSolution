using System.Collections.Generic;

namespace Project.QuestionBank.Application.Models
{
    public class NavModel
    {
        public NavModel()
        {
            children = new List<NavModel>();
        }

        public string typename { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string href { get; set; }
        public bool spread { get; set; }
        public List<NavModel> children { get; set; }
    }
}