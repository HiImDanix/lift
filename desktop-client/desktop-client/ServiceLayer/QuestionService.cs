using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using desktop_client.ModelLayer;
using desktop_client.Properties;
using Newtonsoft.Json;
using RestSharp;

namespace desktop_client.ServiceLayer
{
    public class QuestionService
    {
        RestClient client = new RestClient("https://localhost:7246");

        public QuestionService()
        {
            client = new RestClient();
        }

        public async Task<int> SaveQuestion(Question newQuestion)
        {
            var request = new RestRequest("/questions", Method.POST);
            var param = new
            {
                imagePath = "imagePath",
                QuestionText = "question",
                Category = "category",
                AnswerList = " "
            };
            request.AddObject(param);
            var response = await client.ExecuteAsync(request);
            return (int)response.StatusCode;
        }
    }
}
