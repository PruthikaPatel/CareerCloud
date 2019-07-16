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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                       @"INSERT INTO [dbo].[Company_Profiles]
                                           ([Id]
                                           ,[Registration_Date]
                                           ,[Company_Website]
                                           ,[Contact_Phone]
                                           ,[Contact_Name]
                                           ,[Company_Logo])
                                     VALUES
                                           (@Id
                                           ,@Registration_Date
                                           ,@Company_Website
                                           ,@Contact_Phone
                                           ,@Contact_Name
                                           ,@Company_Logo)", conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
                                      ,[Registration_Date]
                                      ,[Company_Website]
                                      ,[Contact_Phone]
                                      ,[Contact_Name]
                                      ,[Company_Logo]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Profiles]";

                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyProfilePoco[] appPocos = new CompanyProfilePoco[1000];
                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    if (!reader.IsDBNull(2))
                    {
                        poco.CompanyWebsite = reader.GetString(2);
                    }
                    poco.ContactPhone = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                    {
                        poco.ContactName = reader.GetString(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        poco.CompanyLogo = (byte[])reader[5]; 
                    }                   
                   // poco.TimeStamp = (byte[])reader[6];

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
          
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[Company_Profiles] where Id = @Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                       SET [Registration_Date] = @Registration_Date
                                          ,[Company_Website] = @Company_Website
                                          ,[Contact_Phone] = @Contact_Phone
                                          ,[Contact_Name] = @Contact_Name
                                          ,[Company_Logo] = @Company_Logo
                                 WHERE Id = @Id";
                    
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                };
            };
        }
    }
}
