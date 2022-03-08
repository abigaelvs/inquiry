using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using System.Threading.Tasks;

using CNDS.Http;

using InqService.Repository;

namespace InqService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IInquiryRepository _inquiryRepository;
        public LoginController(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] JsonElement requestBody)
        {
            string wrapperUrl = "https://localhost:44393/login";
            string result = await _inquiryRepository.SendRedirect(requestBody.ToString(),
                wrapperUrl, Request);
            return Ok(result);
        }
    }
}
