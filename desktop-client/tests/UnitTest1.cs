using desktop_client.ServiceLayer;

namespace tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            QuestionService questionService = new QuestionService();
            Console.WriteLine(questionService.GetQuestions());
        }
    }
}