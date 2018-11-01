using Project.QuestionBank.Infrastructure.Domain.Impl;

namespace Project.QuestionBank.Core.Domain
{
    public class SysRole : BaseEntity
    {
        public SysRole()
        {
            
        }

        public string RoleName { get; set; }
    }
}