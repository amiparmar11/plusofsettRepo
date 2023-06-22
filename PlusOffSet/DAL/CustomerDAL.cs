using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlusOffSet.Models;
using System.Data;

namespace PlusOffSet.DAL
{
    public class CustomerDAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }
      
        public List<customer> GetAllcustomer()
        {
            List<customer> customerList = new List<customer>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetCustomer_master";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtcustomer = new DataTable();

                connection.Open();
                sqlDA.Fill(dtcustomer);
                connection.Close();

                foreach (DataRow dr in dtcustomer.Rows)
                {
                    customerList.Add(new customer
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = dr["Name"].ToString(),
                        Address = dr["Address"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        
                    });
                }
            }
            return customerList;
        }
        
        public int Insertcustomer(customer customer)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SP_Insertupdate_Customermaster", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", customer.Id);
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }
        //get customer Id
        public List<customer> GetcustomerID(int ID)
        {
            List<customer> customerList = new List<customer>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetCustomer_master";
                command.Parameters.AddWithValue("@Id", ID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtcustomer = new DataTable();

                connection.Open();
                sqlDA.Fill(dtcustomer);
                connection.Close();

                foreach (DataRow dr in dtcustomer.Rows)
                {
                    customerList.Add(new customer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Address = dr["Address"].ToString(),
                        PhoneNumber = dr["PhoneNumber"].ToString()
                       
                    });
                }
            }
            return customerList;
        }
        //Update customer data
        public int UpdateCustomer(customer customer)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SP_Insertupdate_Customermaster", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", customer.Id);
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Address", customer.Address);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }
        //Delete Customer data
        public int DeleteCustomer(int id)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SP_DeleteCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }
    }
}