using System;

namespace InqService.Repository
{
    public interface IEmailRepository
    {
        void SendEmailNotif(string errorCode, Exception ex);
    }
}
