﻿using System;
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
        public CustomerResponse GetCustomers(CustomerRequest customer)
        {
            DBConnection dbconn = null;
            List<MsCustomer> customers = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                //page.RowsPerPage = Int16.Parse(
                //    StartupRepository.ht[GeneralConstant.ParameterRowPerPage].ToString());
                SQLPage page = new SQLPage
                {
                    PageNo = customer.PageNo,
                    RowsPerPage = Int16.Parse(
                        StartupRepository.ht[GeneralConstant.ParameterRowPerPage]
                        .ToString())
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
                RequestTime = customer.RequestTime,
                ChannelId = customer.ChannelId,
                SourceReffId = customer.SourceReffId,
                PageNo = customer.PageNo,
                CustomerList = customers
            };
            return resp;
        }
    }
}