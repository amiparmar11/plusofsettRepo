using Microsoft.AspNetCore.Mvc;
using PlusOffSet.DAL;
using PlusOffSet.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace PlusOffSet.Controllers
{
    public class JobController : Controller
    {
        private readonly JobDAL _jobDAL;

        public JobController(JobDAL jobDAL)
        {
            _jobDAL = jobDAL;
        }

        [HttpGet]
        
        public ActionResult Index()
        {
            try
            {
                var jobList = _jobDAL.GetAlljob();

                if (jobList.Count == 0)
                {
                    TempData["InfoMessage"] = "Currently job not available in the Database.";

                }

                return View(jobList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
            JobDAL jobReportDAL = new JobDAL();
            List<job_master> jobReports = jobReportDAL.GetJobReportByCreatedDate(startDate, endDate);

            return View(jobReports);
        }
        [HttpGet]
        public ActionResult GenerateJobno()
        {
           
            job_master jobNo = _jobDAL.GenerateJobNo();

            return View(jobNo);
        }
        [HttpGet]
        public ActionResult job_create()
        {
            job_master jobNo = _jobDAL.GenerateJobNo();

            return View(jobNo);
        }

        [HttpPost]
        public ActionResult job_create(job_master job)
        {
            try
            {
                int Id = 0;
                job.created_date = DateTime.Today;
                if (ModelState.IsValid)
                {
                    Id = _jobDAL.Insertjob(job);

                    if (Id > 0)
                    {
                        TempData["SuccessMessage"] = "job details saved successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to insert the job.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult job_Edit(int Id)
        {
            try
            {
                var jobList = _jobDAL.GetjobID(Id).FirstOrDefault();

                if (jobList == null)
                {
                    TempData["ErrorMessage"] = "job details not available with the job Id : " + Id;
                    return RedirectToAction("Index");
                }

                return View(jobList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult job_view(int Id)
        {
            try
            {
                var jobList = _jobDAL.GetjobID(Id).FirstOrDefault();

                if (jobList == null)
                {
                    TempData["ErrorMessage"] = "job details not available with the job Id : " + Id;
                    return RedirectToAction("Index");
                }

                return View(jobList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult job_Edit(job_master job)
        {
            try
            {
                int Id = 0;

                if (ModelState.IsValid)
                {
                    Id = _jobDAL.Updatejob(job);

                    if (Id > 0)
                    {
                        TempData["SuccessMessage"] = "job details updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the job.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        
        public ActionResult job_Delete(int Id)
        {
            try
            {
                var jobList = _jobDAL.GetjobID(Id).FirstOrDefault();

                if (jobList == null)
                {
                    TempData["ErrorMessage"] = "job details not available with the job Id : " + Id;
                    return RedirectToAction("Index");
                }

                return View(jobList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("job_Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            try
            {
                int jobId = 0;

                if (ModelState.IsValid)
                {
                    jobId = _jobDAL.Deletejob(Id);

                    if (jobId > 0)
                    {
                        TempData["SuccessMessage"] = "job deleted successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to delete the job.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
