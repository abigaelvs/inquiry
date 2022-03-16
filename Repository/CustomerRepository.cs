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
        public CustomerResponse GetCustomer(CustomerRequest customer)
        {
            DBConnection dbconn = null;
            List<MsCustomer> customers = null;
            SQLPage page = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                Dictionary<string, string> paramCrit = new Dictionary<string, string>
                {
                    { "key_param", CriteriasDB.CrtEqual(GeneralConstant.ParameterRowPerPage) }
                };
                ParameterLevel1 param = sql.ExecuteQueryFirst<ParameterLevel1>(
                    ParameterLevel1.TableName, null, paramCrit, null);
                page = new SQLPage
                {
                    PageNo = customer.PageNo,
                    RowsPerPage = Int16.Parse(param.Value1Param)
                };

                Dictionary<string, string> criterias = new Dictionary<string, string>
                {
                    { "oid", CriteriasDB.CrtEqual(customer.Oid) },
                    { "AND cust_name", CriteriasDB.CrtEqual(customer.CustName) }
                };
                customers = sql.ExecuteQueryPaging<MsCustomer>(MsCustomer.TableName,
                    null, criterias, null, page);

                dbconn.CommitTransaction();
            }
            catch (Exception)
            {
                if (dbconn != null) dbconn.Rollback();
            }
            finally
            {
                if (dbconn != null) dbconn.Close();
            }

            CustomerResponse resp = new CustomerResponse(
                ResponseCodeConstant.RcSuccess, ResponseCodeConstant.MsgSuccess, 
                customers, page);
            PropertyCopier<CustomerRequest, CustomerResponse>.CopyProperties(customer, resp);

            return resp;
        }

        public CustomerResponse GetCustomers(StandardMessage customer)
        {
            DBConnection dbconn = null;
            List<MsCustomer> customers = null;
            SQLPage page = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                Dictionary<string, string> paramCrit = new Dictionary<string, string>
                {
                    { "key_param", CriteriasDB.CrtEqual(GeneralConstant.ParameterRowPerPage) }
                };
                ParameterLevel1 param = sql.ExecuteQueryFirst<ParameterLevel1>(
                    ParameterLevel1.TableName, null, paramCrit, null);
                page = new SQLPage
                {
                    PageNo = customer.PageNo,
                    RowsPerPage = Int16.Parse(param.Value1Param)
                };
                customers = sql.ExecuteQueryPaging<MsCustomer>(MsCustomer.TableName,
                    null, null, null, page);

                dbconn.CommitTransaction();
            }
            catch (Exception)
            {
                if (dbconn != null) dbconn.Rollback();
            }
            finally
            {
                if (dbconn != null) dbconn.Close();
            }

            CustomerResponse resp = new CustomerResponse(
                ResponseCodeConstant.RcSuccess, ResponseCodeConstant.MsgSuccess, 
                customers, page);
            PropertyCopier<StandardMessage, CustomerResponse>.CopyProperties(customer, resp);
            
            return resp;
        }
    }
}
