using System;

namespace Project.QuestionBank.Core.Domain
{
    /// <summary>
    /// 数据字典明细
    /// </summary>
    public class DictItem
    {
        public DictItem()
        {
            
        }

        /// <summary>
        /// 明细主键
        /// </summary>
        /// <returns></returns>
        public string DictItemId { get; set; }

        /// <summary>
        /// 分类主键
        /// </summary>
        /// <returns></returns>
        public string DictId { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public string ItemCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string ItemName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        /// <returns></returns>
        public string ItemValue { get; set; }

        /// <summary>
        /// 快速查询
        /// </summary>
        /// <returns></returns>
        public string QuickQuery { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        /// <returns></returns>
        public string SimpleSpelling { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        /// <returns></returns>
        public int? IsDefault { get; set; }

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