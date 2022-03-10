using System.Collections.Generic;

using InqService.Model;

namespace InqService.Repository
{
    public interface ICustomerRepository
    {
        CustomerResponse GetCustomer(CustomerRequest customer);
        CustomerResponse GetCustomers(StandardMessage customer);
    }
}
