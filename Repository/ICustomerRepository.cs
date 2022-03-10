using System.Collections.Generic;

using InqService.Model;

namespace InqService.Repository
{
    public interface ICustomerRepository
    {
        CustomerResponse GetCustomers(CustomerRequest customer);
    }
}
