using System;
using System.Data;
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
            int rowsPerPage = 1;
            int allCust = 0;

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
                rowsPerPage = Int16.Parse(param.Value1Param);
                SQLPage page = new SQLPage
                {
                    PageNo = customer.PageNo,
                    RowsPerPage = rowsPerPage
                };

                Dictionary<string, string> criterias = new Dictionary<string, string>
                {
                    { "oid", CriteriasDB.CrtEqual(customer.Oid) },
                    { "AND cust_name", CriteriasDB.CrtEqual(customer.CustName) }
                };
                customers = sql.ExecuteQueryPaging<MsCustomer>(MsCustomer.TableName,
                    null, criterias, null, page);

                DataTable dtAllCust = sql.ExecuteQuery(MsCustomer.TableName,
                    null, criterias, null);
                allCust = dtAllCust.Rows.Count;

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

            int totalPage = allCust / rowsPerPage;
            Page paging = new Page
            {
                PageNo = customer.PageNo,
                TotalPage = totalPage > 1 ? totalPage : 1,
                TotalRow = allCust,
                RowPerPage = rowsPerPage
            };
            CustomerResponse resp = new CustomerResponse
            {
                ResponseCode = ResponseCodeConstant.RcSuccess,
                ResponseDesc = ResponseCodeConstant.MsgSuccess,
                RequestTime = customer.RequestTime,
                ChannelId = customer.ChannelId,
                SourceReffId = customer.SourceReffId,
                CustomerList = customers,
                Paging = paging
            };
            return resp;
        }

        public CustomerResponse GetCustomers(StandardMessage customer)
        {
            DBConnection dbconn = null;
            List<MsCustomer> customers = null;
            int rowsPerPage = 1;
            int allCust = 0;

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
                rowsPerPage = Int16.Parse(param.Value1Param);
                SQLPage page = new SQLPage
                {
                    PageNo = customer.PageNo,
                    RowsPerPage = rowsPerPage
                };
                customers = sql.ExecuteQueryPaging<MsCustomer>(MsCustomer.TableName,
                    null, null, null, page);

                DataTable dtAllCust = sql.ExecuteQuery(MsCustomer.TableName,
                    null, null, null);
                allCust = dtAllCust.Rows.Count;

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

            decimal total = (decimal)allCust / rowsPerPage;
            int totalPage = (int)Math.Ceiling(total);
            Page paging = new Page
            {
                PageNo = customer.PageNo,
                TotalPage = totalPage,
                TotalRow = allCust,
                RowPerPage = rowsPerPage
            };
            CustomerResponse resp = new CustomerResponse
            {
                ResponseCode = ResponseCodeConstant.RcSuccess,
                ResponseDesc = ResponseCodeConstant.MsgSuccess,
                RequestTime = customer.RequestTime,
                ChannelId = customer.ChannelId,
                SourceReffId = customer.SourceReffId,
                CustomerList = customers,
                Paging = paging
            };
            return resp;
        }
    }
}
