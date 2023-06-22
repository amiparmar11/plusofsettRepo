using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlusOffSet.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlusOffSet.DAL
{
    public class JobDAL
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
        public List<job_master> GetAlljob()
        {
            List<job_master> jobList = new List<job_master>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetJob_master";

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtjob = new DataTable();
                connection.Open();
                sqlDA.Fill(dtjob);
                connection.Close();

                foreach (DataRow dr in dtjob.Rows)
                {
                    jobList.Add(new job_master
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        job_title = dr["job_title"].ToString(),
                        job_item = dr["job_item"].ToString(),
                        job_size = dr["job_size"].ToString(),
                        job_machine = dr["job_machine"].ToString(),
                        job_qty = Convert.ToInt32(dr["job_qty"]),
                        job_details = dr["job_details"].ToString(),
                        job_paper_type = dr["job_paper_type"].ToString(),
                        job_paper_size = dr["job_paper_size"].ToString(),
                        job_GSM = dr["job_GSM"].ToString(),
                        job_paper_quality = dr["job_paper_quality"].ToString(),
                        job_paper_no_of_sheets = dr["job_paper_no_of_sheets"].ToString(),
                        job_paper_by = dr["job_paper_by"].ToString(),
                        job_printing_no_of_sheets = dr["job_printing_no_of_sheets"].ToString(),
                        job_printing_option = dr["job_printing_option"].ToString(),
                        job_printing_no_of_impression = dr["job_printing_no_of_impression"].ToString(),
                        job_printing_no_of_form = Convert.ToInt32(dr["job_printing_no_of_form"]),
                        job_printing_GSM = dr["job_printing_GSM"].ToString(),
                        job_printing_size1 = dr["job_printing_size1"].ToString(),
                        job_printing_size2 = dr["job_printing_size2"].ToString(),
                        job_finishing_type = dr["job_finishing_type"].ToString(),
                        job_finishing_quality = dr["job_finishing_quality"].ToString(),
                        job_finishing_area = dr["job_finishing_area"].ToString(),
                        job_Note = dr["job_Note"].ToString(),
                        job_no = dr["job_no"].ToString(),
                        created_date = Convert.ToDateTime(dr["created_date"])
                    });
                }
            }
            return jobList;
        }
        public List<job_master> GetJobReportByCreatedDate(DateTime startDate, DateTime endDate)
        {
            List<job_master> jobReports = new List<job_master>();

            using (SqlConnection connection = new SqlConnection("YourConnectionString"))
            {
                SqlCommand command = new SqlCommand("SP_GetJobReport_ByCreatedDate", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    job_master jobReport = new job_master();
                    jobReport.Id = Convert.ToInt32(reader["Id"]);
                    jobReport.job_title = reader["Job_title"].ToString();
                    // Set other properties

                    jobReports.Add(jobReport);
                }

                reader.Close();
            }

            return jobReports;
        }
        public job_master GenerateJobNo()
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SP_GenerateJobno", connection);
                command.CommandType = CommandType.StoredProcedure;

                string jobNo = command.ExecuteScalar().ToString();

                return new job_master { job_no = jobNo };
            }
        }
    

        public int Insertjob(job_master job_Master)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SP_InsertUpdate_jobMaster", connection);
                command.CommandType = CommandType.StoredProcedure;
              
                command.Parameters.AddWithValue("@id", job_Master.Id);
                command.Parameters.AddWithValue("@job_title", job_Master.job_title);
                command.Parameters.AddWithValue("@job_item", job_Master.job_item);
                command.Parameters.AddWithValue("@job_size", job_Master.job_size);
                command.Parameters.AddWithValue("@job_machine", job_Master.job_machine);
                command.Parameters.AddWithValue("@job_qty", job_Master.job_qty);
                command.Parameters.AddWithValue("@job_details", job_Master.job_details);
                command.Parameters.AddWithValue("@job_paper_type", job_Master.job_paper_type);
                command.Parameters.AddWithValue("@job_paper_size", job_Master.job_paper_size);
                command.Parameters.AddWithValue("@job_GSM", job_Master.job_GSM);
                command.Parameters.AddWithValue("@job_paper_quality", job_Master.job_paper_quality);
                command.Parameters.AddWithValue("@job_paper_no_of_sheets", job_Master.job_paper_no_of_sheets);
                command.Parameters.AddWithValue("@job_paper_by", job_Master.job_paper_by);
                command.Parameters.AddWithValue("@job_printing_no_of_sheets", job_Master.job_printing_no_of_sheets);
                command.Parameters.AddWithValue("@job_printing_option", job_Master.job_printing_option);
                command.Parameters.AddWithValue("@job_printing_no_of_impression", job_Master.job_printing_no_of_impression);
                command.Parameters.AddWithValue("@job_printing_no_of_form", job_Master.job_printing_no_of_form);
                command.Parameters.AddWithValue("@job_printing_GSM", job_Master.job_printing_GSM);
                command.Parameters.AddWithValue("@job_printing_size1", job_Master.job_printing_size1);
                command.Parameters.AddWithValue("@job_printing_size2", job_Master.job_printing_size2);
                command.Parameters.AddWithValue("@job_finishing_type", job_Master.job_finishing_type);
                command.Parameters.AddWithValue("@job_finishing_quality", job_Master.job_finishing_quality);
                command.Parameters.AddWithValue("@job_finishing_area", job_Master.job_finishing_area);
                command.Parameters.AddWithValue("@job_Note", job_Master.job_Note);
                command.Parameters.AddWithValue("@job_No", job_Master.job_no);
                command.Parameters.AddWithValue("@created_date", job_Master.created_date);
                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }
        //get Job Id
        public List<job_master> GetjobID(int ID)
        {
            List<job_master> jobList = new List<job_master>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetJob_master";
                command.Parameters.AddWithValue("@Id", ID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtjob = new DataTable();

                connection.Open();
                sqlDA.Fill(dtjob);
                connection.Close();

                foreach (DataRow dr in dtjob.Rows)
                {
                    jobList.Add(new job_master
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        job_no= dr["job_no"].ToString(),
                        job_title = dr["job_title"].ToString(),
                        job_item = dr["job_item"].ToString(),
                        job_size = dr["job_size"].ToString(),
                        job_machine = dr["job_machine"].ToString(),
                        job_qty = Convert.ToInt32(dr["job_qty"]),
                        job_details = dr["job_details"].ToString(),
                        job_paper_type = dr["job_paper_type"].ToString(),
                        job_paper_size = dr["job_paper_size"].ToString(),
                        job_GSM = dr["job_GSM"].ToString(),
                        job_paper_quality = dr["job_paper_quality"].ToString(),
                        job_paper_no_of_sheets = dr["job_paper_no_of_sheets"].ToString(),
                        job_paper_by = dr["job_paper_by"].ToString(),
                        job_printing_no_of_sheets = dr["job_printing_no_of_sheets"].ToString(),
                        job_printing_option = dr["job_printing_option"].ToString(),
                        job_printing_no_of_impression = dr["job_printing_no_of_impression"].ToString(),
                        job_printing_no_of_form = Convert.ToInt32(dr["job_printing_no_of_form"]),
                        job_printing_GSM = dr["job_printing_GSM"].ToString(),
                        job_printing_size1 = dr["job_printing_size1"].ToString(),
                        job_printing_size2 = dr["job_printing_size2"].ToString(),
                        job_finishing_type = dr["job_finishing_type"].ToString(),
                        job_finishing_quality = dr["job_finishing_quality"].ToString(),
                        job_finishing_area = dr["job_finishing_area"].ToString(),
                        job_Note = dr["job_Note"].ToString(),
                        created_date =Convert.ToDateTime(dr["created_date"])

                    });
                }
            }
            return jobList;
        }
        //Update job data
        public int Updatejob(job_master job_Master)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SP_InsertUpdate_jobMaster", connection);
                command.CommandType = CommandType.StoredProcedure;
               
                command.Parameters.AddWithValue("@id", job_Master.Id);
                command.Parameters.AddWithValue("@job_title", job_Master.job_title);
                command.Parameters.AddWithValue("@job_item", job_Master.job_item);
                command.Parameters.AddWithValue("@job_size", job_Master.job_size);
                command.Parameters.AddWithValue("@job_machine", job_Master.job_machine);
                command.Parameters.AddWithValue("@job_qty", job_Master.job_qty);
                command.Parameters.AddWithValue("@job_details", job_Master.job_details);
                command.Parameters.AddWithValue("@job_paper_type", job_Master.job_paper_type);
                command.Parameters.AddWithValue("@job_paper_size", job_Master.job_paper_size);
                command.Parameters.AddWithValue("@job_GSM", job_Master.job_GSM);
                command.Parameters.AddWithValue("@job_paper_quality", job_Master.job_paper_quality);
                command.Parameters.AddWithValue("@job_paper_no_of_sheets", job_Master.job_paper_no_of_sheets);
                command.Parameters.AddWithValue("@job_paper_by", job_Master.job_paper_by);
                command.Parameters.AddWithValue("@job_printing_no_of_sheets", job_Master.job_printing_no_of_sheets);
                command.Parameters.AddWithValue("@job_printing_option", job_Master.job_printing_option);
                command.Parameters.AddWithValue("@job_printing_no_of_impression", job_Master.job_printing_no_of_impression);
                command.Parameters.AddWithValue("@job_printing_no_of_form", job_Master.job_printing_no_of_form);
                command.Parameters.AddWithValue("@job_printing_GSM", job_Master.job_printing_GSM);
                command.Parameters.AddWithValue("@job_printing_size1", job_Master.job_printing_size1);
                command.Parameters.AddWithValue("@job_printing_size2", job_Master.job_printing_size2);
                command.Parameters.AddWithValue("@job_finishing_type", job_Master.job_finishing_type);
                command.Parameters.AddWithValue("@job_finishing_quality", job_Master.job_finishing_quality);
                command.Parameters.AddWithValue("@job_finishing_area", job_Master.job_finishing_area);
                command.Parameters.AddWithValue("@job_Note", job_Master.job_Note);
                command.Parameters.AddWithValue("@job_No", job_Master.job_no);
                command.Parameters.AddWithValue("@created_date", job_Master.created_date);
                connection.Open();
                Id = command.ExecuteNonQuery();
                connection.Close();
            }
            return Id;
        }
        //Delete Customer data
        public int Deletejob(int id)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SP_Delete_JobMaster", connection);
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
