using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanRepository PlanRepository;
        private readonly ISystemUpdateRepository SystemUpdateRepository;
        private readonly IdGenerator _idGenerator;
        private UpdateType updateTypePlan = UpdateType.Plan;

        public PlanController(IPlanRepository planRepository, IdGenerator idGenerator, ISystemUpdateRepository systemUpdateRepository)
        {
            PlanRepository = planRepository;
            _idGenerator = idGenerator;
            SystemUpdateRepository = systemUpdateRepository;
        }

        #region Get All

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(PlanRepository.GetAll());
        }

        #endregion

        #region Get By Id

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(PlanRepository.Get(id));
        }

        #endregion

        #region Get All Plans Available for Students
        [HttpGet("students")]
        public IActionResult GetAllForStudent()
        {
            var plans = PlanRepository.GetPlansForStudent();
            if (plans == null || !plans.Any())
            {
                return NotFound("No plans available for students.");
            }
            return Ok(plans);
        }
        #endregion

        #region Get All Plans Available for Doctors
        [HttpGet("doctors")]
        public IActionResult GetAllForDoctor()
        {
            var plans = PlanRepository.GetPlansForDoctor();
            if (plans == null || !plans.Any())
            {
                return NotFound("No plans available for doctors.");
            }
            return Ok(plans);
        }
        #endregion

        #region Add New Plan

        [HttpPost("{adminId}")] //here
        public IActionResult Add(string adminId, [FromBody] Plan plan) //here
        {
            //make sure the admin exists
            // to be done later

            plan.Id = _idGenerator.GenerateId<Plan>(ModelPrefix.Plan);

            if(plan.PlanName == string.Empty)
            {
                return BadRequest("Plan name cannot be empty.");
            }
            if (plan.Duration == -1)
            {
                return BadRequest("Duration cannot be empty.");
            }
            if (plan.MaxScans == -1)
            {
                return BadRequest("Max Scans cannot be empty.");
            }
            if (plan.MaxPatients == -1)
            {
                return BadRequest("Max Patients cannot be empty.");
            }
            if (plan.Price == -1)
            {
                return BadRequest("Price cannot be empty.");
            }

            SystemUpdate systemUpdate = new SystemUpdate {
                Id = _idGenerator.GenerateId<SystemUpdate>(ModelPrefix.SystemUpdate),
                UpdatedDescribtion = "Plan: " + plan.PlanName + " was added, with price of " + plan.Price.ToString(),
                UpdateType = updateTypePlan,
                AdminId = adminId
            };  //here

            SystemUpdateRepository.AddSystemUpdate(adminId, systemUpdate);  //here
            PlanRepository.Add(plan);

            return Ok("Posted Successfully");
        }

        #endregion

        #region Update Plan

        [HttpPut("{adminId}/{id}")]     //here
        public IActionResult Update(string adminId, string id, [FromBody] Plan plan)    //here
        {
            //checking admin ...

            plan.Id = id;

            var existingPlan = PlanRepository.Get(id);
            if (existingPlan == null)
            {
                return NotFound("Plan not found.");
            }

            List<Subscription> subscriptions = PlanRepository.GetSubscriptions(existingPlan);
            if (subscriptions.Count > 0)
            {
                Plan planTemp = new Plan
                {
                    Id = existingPlan.Id + "-Temp",
                    PlanName = existingPlan.PlanName,
                    Duration = existingPlan.Duration,
                    MaxScans = existingPlan.MaxScans,
                    MaxPatients = existingPlan.MaxPatients,
                    Price = existingPlan.Price
                };

                PlanRepository.Add(planTemp);

                foreach(var subscription in subscriptions)
                {
                    subscription.PlanId = planTemp.Id;
                }
            }


            // Update the old plan with the new values
            if (plan.PlanName == string.Empty)
            {
                plan.PlanName = existingPlan.PlanName;
            }
            if (plan.Duration == -1)
            {
                plan.Duration = existingPlan.Duration;
            }
            if (plan.MaxScans == -1)
            {
                plan.MaxScans = existingPlan.MaxScans;
            }
            if (plan.MaxPatients == -1)
            {
                plan.MaxPatients = existingPlan.MaxPatients;
            }
            if (plan.Price == -1)
            {
                plan.Price = existingPlan.Price;
            }

            SystemUpdate systemUpdate = new SystemUpdate
            {
                Id = _idGenerator.GenerateId<SystemUpdate>(ModelPrefix.SystemUpdate),
                UpdatedDescribtion = "Plan: " + plan.PlanName + " was updated",
                UpdateType = updateTypePlan,
                AdminId = adminId
            };  //here

            SystemUpdateRepository.AddSystemUpdate(adminId, systemUpdate);  //here

            // Update the plan in the repository
            existingPlan = plan;

            PlanRepository.Update(existingPlan);

            return Ok("Updated Successfully");
        }

        #endregion

        #region Delete Plan

        [HttpDelete("{adminId}/{id}")]      //here
        public IActionResult Delete(string adminId, string id)      //here
        {
            //checking admin ...

            Plan? plan = PlanRepository.Get(id);
            if(plan != null)
            {
                if (PlanRepository.Deleteable(id))
                {
                    SystemUpdate systemUpdate = new SystemUpdate
                    {
                        Id = _idGenerator.GenerateId<SystemUpdate>(ModelPrefix.SystemUpdate),
                        UpdatedDescribtion = "Plan: " + plan.PlanName + " was deleted",
                        UpdateType = updateTypePlan,
                        AdminId = adminId
                    };  //here

                    SystemUpdateRepository.AddSystemUpdate(adminId, systemUpdate);  //here


                    PlanRepository.Delete(plan);
                    return Ok("Deleted Successfully");
                }
                else
                {
                    return BadRequest("Plan has active subscriptions. Cannot delete.");
                }
            }
            else
            {
                return NotFound("Plan not found");
            }
        }

        #endregion

    }
}
