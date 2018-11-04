using System;

namespace Project.QuestionBank.Core.Domain
{
    /// <summary>
    /// 数据字典类型
    /// </summary>
    public class DictType
    {
        public DictType()
        {
            
        }

        /// <summary>
        /// 分类主键
        /// </summary>
        /// <returns></returns>
        public string DictId { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>]
        public string ParentId { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        /// <returns></returns>
        public string DictCode { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        /// <returns></returns>
        public string DictName { get; set; }

        /// <summary>
        /// 树型结构
        /// </summary>
        /// <returns></returns>
        public int? IsTree { get; set; }

        /// <summary>
        /// 导航标记
        /// </summary>
        /// <returns></returns>
        public int? IsNav { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
    }
}