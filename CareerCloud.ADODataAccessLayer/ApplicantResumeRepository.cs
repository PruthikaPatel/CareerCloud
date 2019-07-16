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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"insert into [dbo].[Applicant_Resumes]
                            ([Id],
                            [Applicant],
                            [Resume],
                            [Last_Updated])
                    VALUES 
                            (@Id,
                            @Applicant,
                            @Resume,
                            @Last_Updated) ", conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);
                  
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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],[Applicant],[Resume],[Last_Updated]
                    FROM [dbo].[Applicant_Resumes]";

                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                ApplicantResumePoco[] appPocos = new ApplicantResumePoco[1000];
                while (reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Resume = reader.GetString(2);
                   //poco.LastUpdated =  reader.IsDbNull(3) ? null : reader.GetDateTime(3);
                    if (!reader.IsDBNull(3))
                    {
                        poco.LastUpdated = reader.GetDateTime(3);
                    }

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[Applicant_Resumes] where Id = @Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantResumePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                        SET 
                                        [Applicant] = @Applicant,
                                        [Resume] = @Resume,
                                        [Last_Updated] = @Last_Updated                                       
                                        where [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);                   
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                };
            };
        }
    }
}
