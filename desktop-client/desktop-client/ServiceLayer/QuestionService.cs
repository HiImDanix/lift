using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Windows.Navigation;
using desktop_client.ModelLayer;
using desktop_client.Properties;
using desktop_client.repositoryLayer;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace desktop_client.ServiceLayer
{
    public class QuestionService
    {
        RestClient client;
        public QuestionService()
        {
            client = new RestClient(WebConfigurationManager.AppSettings["WebserviceURI"]);
        }

        public async Task<int> SaveQuestion(Question newQuestion)
        {
            try
            {
                var request = new RestRequest("/questions", Method.POST);
                request.AddHeader("Authorization", "Bearer " + AuthRepository.GetInstance().GetAdmin().Session);
                var param = new
                {
                    imagePath = newQuestion.ImagePath,
                    QuestionText = newQuestion.QuestionText,
                    Category = newQuestion.Category,
                };
                for (int i = 0; i < 4; i++)
                {
                    request.AddParameter($"Answers[{i}].answerText", newQuestion.Answers[i].AnswerText);
                    request.AddParameter($"Answers[{i}].isCorrect", newQuestion.Answers[i].IsCorrect);
                }
                    request.AddObject(param);
                var response = await client.ExecuteAsync(request);
                return (int)response.StatusCode;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<List<Question>> GetQuestions()
        {
            List<Question> questionList = new List<Question>();

            var request = new RestRequest("/questions", Method.GET);
            request.AddHeader("Authorization", "Bearer " + AuthRepository.GetInstance().GetAdmin().Session);

            var response = await client.ExecuteAsync(request);
            questionList = JsonConvert.DeserializeObject<List<Question>>(response.Content);

            return questionList;
        }
    }
}
