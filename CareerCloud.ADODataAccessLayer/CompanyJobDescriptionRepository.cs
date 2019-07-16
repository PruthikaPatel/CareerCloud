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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                       @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                            ([Id]
                                            ,[Job]
                                            ,[Job_Name]
                                            ,[Job_Descriptions])
                                    VALUES
                                            (@Id
                                            ,@Job
                                            ,@Job_Name
                                            ,@Job_Descriptions)", conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
                                      ,[Job]
                                      ,[Job_Name]
                                      ,[Job_Descriptions]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Jobs_Descriptions]";

                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyJobDescriptionPoco[] appPocos = new CompanyJobDescriptionPoco[5000];
                while (reader.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.JobName = reader.GetString(2);
                    poco.JobDescriptions = reader.GetString(3);
                    /*if (!reader.IsDBNull(4))
                     {
                         poco.TimeStamp = (byte[])reader[4];
                     }
                     */
                    //  poco.TimeStamp = (byte[])reader[4];

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }

        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[Company_Jobs_Descriptions] where Id = @Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                       SET [Job] = @Job
                                          ,[Job_Name] = @Job_Name
                                          ,[Job_Descriptions] = @Job_Descriptions
                                 WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                };
            };
        }
    }
}
