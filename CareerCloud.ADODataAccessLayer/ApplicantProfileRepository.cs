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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"INSERT into [dbo].[Applicant_Profiles]                      
                        ([Id],
                         [Login],
                         [Current_Salary],
                          [Current_Rate],
                          [Currency],
                           [Country_Code],
                           [State_Province_Code],
                           [Street_Address],
                           [City_Town],
                           [Zip_Postal_Code])
                      VALUES
                           (@Id,
                           @Login,
                           @Current_Salary,
                           @Current_Rate,
                           @Currency,
                           @Country_Code,
                           @State_Province_Code,
                           @Street_Address,
                           @City_Town,
                           @Zip_Postal_Code) ", conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                };
            };
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id],
                         [Login],
                         [Current_Salary],
                          [Current_Rate],
                          [Currency],
                           [Country_Code],
                           [State_Province_Code],
                           [Street_Address],
                           [City_Town],
                           [Zip_Postal_Code]
               
                    FROM [dbo].[Applicant_Profiles]";

                int x = 0;
                ApplicantProfilePoco[] appPocos = new ApplicantProfilePoco[5000];
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary = reader.GetDecimal(2);
                    poco.CurrentRate = reader.GetDecimal(3);
                    poco.Currency = reader.GetString(4);
                    poco.Country = reader.GetString(5);
                    poco.Province = reader.GetString(6);
                    poco.Street = reader.GetString(7);
                    poco.City = reader.GetString(8);
                    poco.PostalCode = reader.GetString(9);
                    /*if(!reader.IsDBNull(10))
                    {
                        poco.TimeStamp = (byte[])reader[10];
                    }*/

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }

        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[Applicant_Profiles] where Id = @Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                        SET [Id] = @Id
                                            ,[Login] = @Login
                                            ,[Current_Salary] = @Current_Salary
                                            ,[Current_Rate] = @Current_Rate
                                            ,[Currency] = @Currency
                                            ,[Country_Code] = @Country_Code
                                            ,[State_Province_Code] = @State_Province_Code
                                            ,[Street_Address] = @Street_Address
                                            ,[City_Town] = @City_Town
                                            ,[Zip_Postal_Code] = @Zip_Postal_Code
                                 WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
