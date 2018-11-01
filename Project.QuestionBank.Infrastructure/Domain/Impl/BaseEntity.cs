using Project.QuestionBank.Infrastructure.Domain.Interface;
using SqlSugar;

namespace Project.QuestionBank.Infrastructure.Domain.Impl
{
    /// <summary>
    /// 实体基类
    /// Author:刘健
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
    }
}
