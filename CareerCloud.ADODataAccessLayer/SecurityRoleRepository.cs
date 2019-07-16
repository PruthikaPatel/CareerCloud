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
   public  class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                      @"INSERT INTO [dbo].[Security_Roles]
                                           ([Id]
                                           ,[Role]
                                           ,[Is_Inactive])
                                     VALUES
                                           (@Id
                                           ,@Role
                                           ,@Is_Inactive)", conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);

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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]                                      
                                      ,[Role]
                                      ,[Is_Inactive]
                                  FROM [dbo].[Security_Roles]";

                conn.Open();
                int x = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                SecurityRolePoco[] appPocos = new SecurityRolePoco[1000];
                while (reader.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Role = reader.GetString(1);
                    poco.IsInactive = reader.GetBoolean(2);                   

                    appPocos[x] = poco;
                    x++;
                }
                conn.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = $"DELETE [dbo].[Security_Roles] where Id = @Id";
                    cmd.Parameters.AddWithValue("Id", item.Id);
                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityRolePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                                       SET [Role] = @Role
                                          ,[Is_Inactive] = @Is_Inactive
                                 WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowEffectecd = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
