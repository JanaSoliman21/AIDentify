using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class PlanController: ControllerBase
    {
        private readonly IPlanRepository PlanRepository;
        private readonly IdGenerator _idGenerator;

        public PlanController(IPlanRepository planRepository, IdGenerator idGenerator)
        {
            PlanRepository = planRepository;
            _idGenerator = idGenerator;
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

        #region Add New Plan

        [HttpPost]
        public IActionResult Add([FromBody] Plan plan)
        {
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

            PlanRepository.Add(plan);
            return Ok("Posted Successfully");
        }

        #endregion

        #region Update Plan

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Plan plan)
        {
            plan.Id = id;

            var existingPlan = PlanRepository.Get(id);
            if (existingPlan == null)
            {
                return NotFound("Plan not found.");
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

            // Update the plan in the repository
            existingPlan = plan;

            PlanRepository.Update(existingPlan);

            return Ok("Updated Successfully");
        }

        #endregion

        #region Delete Plan

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Plan? plan = PlanRepository.Get(id);
            if(plan != null)
            {
                if (PlanRepository.Deleteable(id))
                {
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
