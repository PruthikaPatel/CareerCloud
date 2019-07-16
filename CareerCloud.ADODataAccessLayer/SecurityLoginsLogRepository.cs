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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                      @"INSERT INTO [dbo].[Security_Logins_Log]
                                           ([Id]
                                           ,[Login]
                                           ,[Source_IP]
                                           ,[Logon_Date]
                                           ,[Is_Succesful])
                                     VALUES
                                           (@Id
                                           ,@Login
                                           ,@Source_IP
                                           ,@Logon_Date
                                           ,@Is_Succesful)", conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);

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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
                                      ,[Login]
                                      ,[Source_IP]
                                      ,[Logon_Date]
                                      ,[Is_Succesful]
                                  FROM [dbo].[Security_Logins_Log]";

                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                SecurityLoginsLogPoco[] appPocos = new SecurityLoginsLogPoco[4000];
                while (reader.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.SourceIP = reader.GetString(2);
                    poco.LogonDate = reader.GetDateTime(3);
                    poco.IsSuccesful = reader.GetBoolean(4);

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
          
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[Security_Logins_Log] where Id = @Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                                       SET [Login] = @Login
                                          ,[Source_IP] = @Source_IP
                                          ,[Logon_Date] = @Logon_Date
                                          ,[Is_Succesful] = @Is_Succesful
                                 WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                };
            };
        }
    }
}
