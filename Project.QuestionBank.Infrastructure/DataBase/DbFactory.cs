using System;
using System.Configuration;
using SqlSugar;

namespace Project.QuestionBank.Infrastructure.DataBase
{
    /// <summary>
    /// 数据库工厂
    /// Author:刘健
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 数据连接对象 
        /// </summary>
        public SqlSugarClient Db { get; private set; }

        /// <summary>
        /// 数据库类型 
        /// </summary>
        public static DbType DbType { get; set; }

        /// <summary>
        /// 数据库上下文实例（自动关闭连接）
        /// </summary>
        public static DbFactory Context
        {
            get { return new DbFactory(); }
        }

        private DbFactory()
        {
            var dataType = ConfigurationManager.AppSettings["DbType"];
            var connectionSting = DbConfig.ConnectionString;

            if (dataType == "SqlServer")
            {
                DbType = DbType.SqlServer;
            }
            if (dataType == "MySql")
            {
                DbType = DbType.MySql;
            }

            if (string.IsNullOrEmpty(connectionSting))
            {
                throw new ArgumentNullException("数据库连接字符串为空");
            }

            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionSting,
                DbType = DbType,
                IsAutoCloseConnection = true,
                IsShardSameThread = true,//设为true相同线程是同一个SqlConnection
                InitKeyType = InitKeyType.SystemTable,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache()
                },
                MoreSettings = new ConnMoreSettings()
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true
                }
            });
        }

        #region 实例方法

        /// <summary>
        /// 功能描述:获取数据库处理对象
        /// Author:刘健
        /// </summary>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDb<T>() where T : class, new()
        {
            return new SimpleClient<T>(Db);
        }

        /// <summary>
        /// 功能描述:获取数据库处理对象
        /// Author:刘健
        /// </summary>
        /// <param name="db">db</param>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetEntityDb<T>(SqlSugarClient db) where T : class, new()
        {
            return new SimpleClient<T>(db);
        }

        #endregion
    }
}