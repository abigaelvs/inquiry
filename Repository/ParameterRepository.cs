using System;
using System.Collections.Generic;

using CNDS.Connection;
using CNDS.SqlStandard;
using CNDS.Utils;
using CNDS.SqlPaging;

using InqService.Entity;

namespace InqService.Repository
{
    public class ParameterRepository
    {
        public static List<ParameterLevel1> GetPageParameterLvl1(SQLPage page)
        {
            DBConnection dbconn = null;
            List<ParameterLevel1> result = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                result = sql.ExecuteQueryPaging<ParameterLevel1>(ParameterLevel1.TableName,
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
            return result;
        }

        public static ParameterLevel1 GetParameterLvl1(string keyParam)
        {
            ParameterLevel1 param = null;
            DBConnection dbconn = null;
            Dictionary<string, string> criterias = null;
            List<ParameterLevel1> paramList = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                criterias = new Dictionary<string, string>()
                {
                    { "key_param", CriteriasDB.CrtEqual(keyParam) }
                };

                paramList = sql.ExecuteQueryList<ParameterLevel1>(
                    ParameterLevel1.TableName,
                    null, criterias, null);

                if (paramList != null && paramList.Count > 0)
                {
                    param = paramList[0];
                }
                dbconn.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (criterias != null) criterias.Clear();
                if (paramList.Count > 0) paramList.Clear();
                if (dbconn != null) dbconn.Close();
            }
            return param;
        }

        public static List<ParameterLevel1> GetListParameterLvl1(string keyParam)
        {
            DBConnection dbconn = null;
            Dictionary<string, string> criterias = null;
            List<ParameterLevel1> paramList = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                criterias = new Dictionary<string, string>()
                {
                    { "key_param", CriteriasDB.CrtEqual(keyParam) }
                };

                paramList = sql.ExecuteQueryList<ParameterLevel1>(
                    ParameterLevel1.TableName,
                    null, criterias, null);

                dbconn.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (criterias != null) criterias.Clear();
                if (dbconn != null) dbconn.Close();
            }
            return paramList;
        }

        public static List<ParameterLevel2> GetListParameterLvl2(string keyParam)
        {
            DBConnection dbconn = null;
            Dictionary<string, string> criterias = null;
            List<ParameterLevel2> paramList = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                criterias = new Dictionary<string, string>()
                {
                    { "key_param", CriteriasDB.CrtEqual(keyParam) }
                };

                paramList = sql.ExecuteQueryList<ParameterLevel2>(
                    ParameterLevel2.TableName,
                    null, criterias, null);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (criterias != null) criterias.Clear();
                if (dbconn != null) dbconn.Close();
            }
            return paramList;
        }

        public static void UpdateParameterLvl1(object o, string keyParam)
        {
            DBConnection dbconn = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                Dictionary<string, string> criterias = new Dictionary<string, string>()
                {
                    { "key_param", CriteriasDB.CrtEqual(keyParam) }
                };

                sql.ExecuteUpdate(ParameterLevel1.TableName, o, criterias);

                dbconn.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (dbconn != null) dbconn.Close();
            }
        }

        public static List<ParameterLevel1> GetMultipleParameterLvl1(
            List<string> listKeyParam)
        {
            List<ParameterLevel1> paramList = null;
            DBConnection dbconn = null;
            Dictionary<string, string> criterias = null;

            try
            {
                dbconn = new DBConnection();
                SQLStandard sql = new SQLStandard(dbconn);
                dbconn.BeginTransaction();

                string whereKey = listKeyParam.Count == 0 ? 
                    "" : "('" + String.Join("', '", listKeyParam) + "')";
                criterias = new Dictionary<string, string>()
                {
                    { "key_param", $" in {whereKey}" }
                };
                paramList = sql.ExecuteQueryList<ParameterLevel1>(ParameterLevel1.TableName,
                    null, criterias, null);

                dbconn.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (criterias != null) criterias.Clear();
                if (dbconn != null) dbconn.Close();
            }
            return paramList;
        }
    }
}
