using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;
using InqService.Model;

namespace InqService.Repository
{
    public interface IInquiryRepository
    {
        Task<string> SendRedirect(string requestBody, string url, HttpRequest request);
        LoginResponse DoLogin(LoginRequest requestData);
        SelectUnitResponse SelectUnit(SelectUnitRequest requestData);
    }
}
