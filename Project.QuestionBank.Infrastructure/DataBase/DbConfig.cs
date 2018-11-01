using System.Configuration;

namespace Project.QuestionBank.Infrastructure.DataBase
{
    /// <summary>
    /// 数据库配置
    /// Author:刘健
    /// </summary>
    public static class DbConfig
    {
        /// <summary>
        /// 数据库连接字符串(公有属性)
        /// </summary>
        public static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["QuestionBankDb"].ConnectionString;

        public static string ConnectionStringDev { get; } = ConfigurationManager.ConnectionStrings["QuestionBankDbDev"].ConnectionString;
    }
}