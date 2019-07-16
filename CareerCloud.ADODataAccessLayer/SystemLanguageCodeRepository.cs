using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (SystemLanguageCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                       @"INSERT INTO [dbo].[System_Language_Codes]
                                            ([LanguageID],
                                            [Name]
                                           ,[Native_Name])
                                      VALUES
                                           (@LanguageID
                                           ,@Name
                                           ,@Native_Name)", conn
                        );
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [LanguageID]
                                      ,[Name]
                                      ,[Native_Name]
                                  FROM [dbo].[System_Language_Codes]";

                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                SystemLanguageCodePoco[] appPocos = new SystemLanguageCodePoco[1000];
                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = reader.GetString(0);
                    poco.Name = reader.GetString(1);
                    poco.NativeName = reader.GetString(2);

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }

        }


        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[System_Language_Codes] where LanguageID = @LanguageID";
                    cmd.Parameters.AddWithValue("LanguageID", item.LanguageID);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                       SET [Name] = @Name
                                          ,[Native_Name] = @Native_Name
                                 WHERE LanguageID =  @LanguageID";

                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

