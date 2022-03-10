using System.Collections.Generic;

using CNDS.SqlPaging;

using InqService.Entity;
using InqService.Model;

namespace InqService.Repository
{
    public interface ICustomerRepository
    {
        CustomerResponse GetCustomers(SQLPage page);
    }
}
