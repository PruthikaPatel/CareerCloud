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
   public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (CompanyJobSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                       @"INSERT INTO [dbo].[Company_Job_Skills]
                                ([Id]
                                ,[Job]
                                ,[Skill]
                                ,[Skill_Level]
                                ,[Importance])
                            VALUES
                                 (@Id
                                 ,@Job
                                 ,@Skill
                                 ,@Skill_Level
                                 ,@Importance)", conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", poco.Importance);

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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
                                      ,[Job]
                                      ,[Skill]
                                      ,[Skill_Level]
                                      ,[Importance]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Company_Job_Skills]";

                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyJobSkillPoco[] appPocos = new CompanyJobSkillPoco[9000];
                while (reader.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.Importance = reader.GetInt32(4);
                    //poco.TimeStamp = (byte[])reader[5];

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[Company_Job_Skills] where Id = @Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobSkillPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                       SET [Job] = @Job
                                          ,[Skill] = @Skill
                                          ,[Skill_Level] = @Skill_Level
                                          ,[Importance] = @Importance
                                 WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", poco.Importance);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                };
            };
        }
    }
}
