using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Project.QuestionBank.Infrastructure.DataBase;
using Project.QuestionBank.Infrastructure.Repository.Interface;
using SqlSugar;

namespace Project.QuestionBank.Infrastructure.Repository.Impl
{
    /// <summary>
    /// 数据仓储基接口实现
    /// </summary>
    /// Author:刘健
    /// <typeparam name="TEntity">实体类类型</typeparam>
    public abstract class BaseRepository<TEntity> : IDependency, IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 当前SqlSugarClient
        /// </summary>
        internal SqlSugarClient Db { get; private set; }

        /// <summary>
        /// 当前SqlSugarClient对应数据实体
        /// </summary>
        internal SimpleClient<TEntity> EntityDb { get; private set; }

        protected BaseRepository()
        {
            Db = DbFactory.Context.Db;
            EntityDb = DbFactory.Context.GetEntityDb<TEntity>(Db);
        }

        #region DbContext

        public SqlSugarClient CurrentDb
        {
            get { return Db; }
        }

        #endregion

        #region 事务

        public SqlSugarClient CurrentDbForTran
        {
            get { return GetInstance(); }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTran()
        {
            CurrentDbForTran.Ado.BeginTran();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            CurrentDbForTran.Ado.CommitTran();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            CurrentDbForTran.Ado.RollbackTran();
        }

        /// <summary>
        /// 跨方法事务方案使用数据库客户端
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = DbConfig.ConnectionString,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = false,
                    IsShardSameThread = true /*Shard Same Thread*/
                });

            return db;
        }

        #endregion

        #region 新增

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>int</returns>
        public async Task<int> Add(TEntity entity)
        {
            try
            {
                var res = await Task.Run(() => Db.Insertable(entity).ExecuteReturnBigIdentity());
                //返回的i是long类型,这里你可以根据你的业务需要进行处理
                return (int)res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 新增实体含事务
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> AddToTran(TEntity entity)
        {
            try
            {
                var res = Db.Ado.UseTran<long>(() => Db.Insertable(entity).ExecuteReturnBigIdentity());
                //返回的i是long类型,这里你可以根据你的业务需要进行处理
                return (int)res.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion

        #region 更新

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>bool</returns>
        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                //这种方式会以主键为条件
                var res = await Task.Run(() => Db.Updateable(entity).ExecuteCommand());
                return res > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 根据条件更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="strWhere">条件语句</param>
        /// <returns>bool</returns>
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            try
            {
                var res = await Task.Run(() => Db.Updateable(entity).Where(strWhere).ExecuteCommand());
                return res > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="lstColumns">更新字段列</param>
        /// <param name="lstIgnoreColumns">忽略字段列</param>
        /// <param name="strWhere">条件语句</param>
        /// <returns>bool</returns>
        public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            try
            {
                IUpdateable<TEntity> up = await Task.Run(() => Db.Updateable(entity));
                if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
                {
                    up = await Task.Run(() => up.IgnoreColumns(it => lstIgnoreColumns.Contains(it)));
                }
                if (lstColumns != null && lstColumns.Count > 0)
                {
                    up = await Task.Run(() => up.UpdateColumns(it => lstColumns.Contains(it)));
                }
                if (!string.IsNullOrEmpty(strWhere))
                {
                    up = await Task.Run(() => up.Where(strWhere));
                }
                return await Task.Run(() => up.ExecuteCommand()) > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteById(object id)
        {
            try
            {
                var res = await Task.Run(() => Db.Deleteable<TEntity>(id).ExecuteCommand());
                return res > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 根据实体对象删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>bool</returns>
        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                var res = await Task.Run(() => Db.Deleteable(entity).ExecuteCommand());
                return res > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 根据主键集合删除实体
        /// </summary>
        /// <param name="ids">主键id数组集合</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteByIdArray(object[] ids)
        {
            try
            {
                var res = await Task.Run(() => Db.Deleteable<TEntity>().In(ids).ExecuteCommand());
                return res > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion

        #region 查询

        /// <summary>
        /// 根据主键获取实体类信息
        /// </summary>
        /// <param name="objId">主键id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>TEntity实体类</returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().InSingle(objId));
        }

        /// <summary>
        /// 根据主键获取实体类信息
        /// </summary>
        /// <param name="objId">主键id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>TEntity实体类</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
        }

        /// <summary>
        /// 根据主键集合获取实例集合
        /// </summary>
        /// <param name="Ids">主键id数组集合</param>
        /// <returns>实体类集合List<TEntity></returns>
        public async Task<List<TEntity>> QueryByIdArray(object[] Ids)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().In(Ids).ToList());
        }

        /// <summary>
        /// 根据条件查询实体
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <returns>TEntity实体类</returns>
        public async Task<TEntity> FindEntity(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => EntityDb.GetSingle(whereExpression));
        }

        /// <summary>
        ///获取实例集合
        /// </summary>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> Query()
        {
            return await Task.Run(() => EntityDb.GetList());
        }

        /// <summary>
        /// 根据条件查询实体
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <returns>实体类集合List<TEntity></returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => EntityDb.GetList(whereExpression));
        }

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToList());
        }

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList());
        }

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList());
        }

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList());
        }

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize));
        }

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<Tuple<List<TEntity>, int>> QueryPageAndCount(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return new Tuple<List<TEntity>, int>(await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize)), await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Count()));
        }

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> QueryPageBySql(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize));
        }

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="strWhere">sql条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<Tuple<List<TEntity>, int>> QueryPageAndCountBySql(string strWhere, int intPageIndex,
            int intPageSize, string strOrderByFileds)
        {
            return new Tuple<List<TEntity>, int>(await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize)), await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Count()));
        }

        /// <summary>
        /// 根据条件获取实体分页集合
        /// </summary>
        /// <param name="whereExpression">lambda条件</param>
        /// <param name="intPageIndex">当前页索引</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>实体集合List<TEntity></returns>
        public async Task<List<TEntity>> QueryPageDefault(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await Task.Run(() => Db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageList(intPageIndex, intPageSize));
        }

        #endregion
    }
}