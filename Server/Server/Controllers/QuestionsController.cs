using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    
    public class QuestionsController : Controller
    {
        [Route("/question")]
        [HttpPost]
        public IActionResult Create([FromForm] string imagepath, string questionText, string category, string answer)
        {
            return new ObjectResult("hahaworks");
        }
    }
}
