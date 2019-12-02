using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view            
            Job job = jobData.Find(id);

            return View(job);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                Job job = new Job
                {
                    Name = newJobViewModel.Name,
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID)
                };
                jobData.Jobs.Add(job);                

                return Redirect($"/Job?id={job.ID}");
            }
            else
            {
                TempData["Error"] = "Name cannot be blank";
                return Redirect("/Job/New");
            }

            return View();
        }
    }
}
