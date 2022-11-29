using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using desktop_client.ModelLayer;
using Newtonsoft.Json;

namespace desktop_client.ServiceLayer
{
    public class QuestionService
    {
        static readonly string restUrl = "https://localhost:7246/Questions";
        readonly HttpClient _httpClient;

        public HttpStatusCode CurrentHttpStatusCode { get; set; }

        public QuestionService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<int> SaveQuestion(Question newQuestion)
        {
            int insertedQuestionId;
            string useRestUrl = restUrl;
            var uri = new Uri(string.Format(useRestUrl));

            try
            {
                var json = JsonConvert.SerializeObject(newQuestion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _httpClient.PostAsync(uri, content);
                CurrentHttpStatusCode = response.StatusCode;
                string resultIdString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Int32.TryParse(resultIdString, out insertedQuestionId);
                }
                else
                {
                    insertedQuestionId = -2;
                }
            }
            catch
            {
                insertedQuestionId = -3;
            }
            return insertedQuestionId;
        }
    }
}
