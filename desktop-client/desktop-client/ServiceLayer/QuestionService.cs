using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Navigation;
using desktop_client.ModelLayer;
using desktop_client.Properties;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace desktop_client.ServiceLayer
{
    public class QuestionService
    {
        public QuestionService()
        {
        }

        public async Task<int> SaveQuestion(Question newQuestion)
        {
            try
            {
                var client = new RestClient(WebConfigurationManager.AppSettings["WebserviceURI"]);
                var request = new RestRequest("/questions/0", Method.PUT);
                var param = new
                {
                    imagePath = newQuestion.ImagePath,
                    QuestionText = newQuestion.QuestionText,
                    Category = newQuestion.Category,
                };
                for (int i = 0; i < 4; i++)
                {
                    request.AddParameter($"Answers[{i}].answerText", newQuestion.AnswerList[i].AnswerText);
                    request.AddParameter($"Answers[{i}].isCorrect", newQuestion.AnswerList[i].IsCorrect);
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
    }
}
