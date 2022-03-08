using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace InqService.Repository
{
    public interface IInquiryRepository
    {
        Task<string> SendRedirect(string requestBody, string url, 
            HttpRequest request);
    }
}
