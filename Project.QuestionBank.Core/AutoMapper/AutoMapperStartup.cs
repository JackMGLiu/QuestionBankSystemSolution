namespace Project.QuestionBank.Core.AutoMapper
{
    /// <summary>
    /// AutoMapper初始化
    /// </summary>
    public class AutoMapperStartup
    {
        public void Execute()
        {
            AutoMapperConfiguration.Init();
        }
    }
}