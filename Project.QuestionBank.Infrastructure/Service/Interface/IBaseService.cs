using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.QuestionBank.Infrastructure.Service.Interface
{
    /// <summary>
    /// 数据业务基接口
    /// </summary>
    /// Author:刘健
    /// <typeparam name="TEntity">实体类类型</typeparam>
    public interface IBaseService<TEntity> where TEntity : class
    {
        #region 新增

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>int</returns>
        Task<int> Add(TEntity entity);

        /// <summary>
        /// 新增实体含事务
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> AddToTran(TEntity entity);

        #endregion

        #region 更新

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>bool</returns>
        Task<bool> Update(TEntity entity);

        /// <summary>
        /// 根据条件更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="strWhere">条件语句</param>
        /// <returns>bool</returns>
        Task<bool> Update(TEntity entity, string strWhere);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="lstColumns">更新字段列</param>
        /// <param name="lstIgnoreColumns">忽略字段列</param>
        /// <param name="strWhere">条件语句</param>
        /// <returns>bool</returns>
        Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null,
            string strWhere = "");


        #endregion

        #region 删除

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>bool</returns>
        Task<bool> DeleteById(object id);

        /// <summary>
        /// 根据实体对象删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>bool</returns>
        Task<bool> Delete(TEntity entity);

        /// <summary>
        /// 根据主键集合删除实体
        /// </summary>
        /// <param name="ids">主键id数组集合</param>
        /// <returns>bool</returns>
        Task<bool> DeleteByIdArray(object[] ids);

        #endregion

        #region 查询

        /// <summary>
        /// 根据主键获取实体类信息
        /// </summary>
        /// <param name="objId">主键id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>TEntity实体类</returns>
        Task<TEntity> QueryById(object objId);

        /// <summary>
        /// 根据主键获取实体类信息
        /// </summary>
        /// <param name="objId">主键id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>TEntity实体类</returns>
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);

        /// <summary>
        /// 根据主键集合获取实例集合
        /// </summary>
        /// <param name="Ids">主键id数组集合</param>
        /// <returns>实体类集合List<TEntity></returns>
        Task<List<TEntity>> QueryByIdArray(object[] Ids);

        /// <summary>
        /// 根据条件查询实体
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <returns>TEntity实体类</returns>
        Task<TEntity> FindEntity(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        ///获取实例集合
        /// </summary>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> Query();

        /// <summary>
        /// 根据条件查询实体
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <returns>实体类集合List<TEntity></returns>
        Task<List<TEntity>> Query(string strWhere);

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex,
            int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<Tuple<List<TEntity>, int>> QueryPageAndCount(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> QueryPageBySql(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<Tuple<List<TEntity>, int>> QueryPageAndCountBySql(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        Task<List<TEntity>> QueryPageDefault(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0,
            int intPageSize = 20, string strOrderByFileds = null);

        #endregion

    }
}