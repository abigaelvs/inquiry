using System;
using System.Collections.Generic;

using CNDS.Connection;
using CNDS.SqlPaging;
using CNDS.SqlStandard;
using CNDS.Utils;

using InqService.Constants;
using InqService.Entity;
using InqService.Model;

namespace InqService.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerResponse GetCustomers(SQLPage page)
        {
            DBConnection dbconn = null;
            List<MsCustomer> customers = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                page.RowsPerPage = Int16.Parse(
                    StartupRepository.ht[GeneralConstant.ParameterRowPerPage].ToString());
                customers = sql.ExecuteQueryPaging<MsCustomer>(MsCustomer.TableName,
                    null, null, null, page);

                dbconn.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (dbconn != null) dbconn.Rollback();
            }
            finally
            {
                if (dbconn != null) dbconn.Close();
            }

            CustomerResponse resp = new CustomerResponse
            {
                ResponseCode = ResponseCodeConstant.RcSuccess,
                ResponseDesc = ResponseCodeConstant.MsgSuccess,
                RequestTime = DateTime.Now.ToString(),
                ChannelId = "Channel Id",
                SourceReffId = DateTime.Now,
                PageNo = page.PageNo,
                CustomerList = customers
            };
            return resp;
        }
    }
}
