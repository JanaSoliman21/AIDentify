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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(PlanRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(PlanRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] Plan plan)
        {
            plan.Id = _idGenerator.GenerateId<Plan>(ModelPrefix.Plan);
            PlanRepository.Add(plan);
            return Ok("Posted Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Plan plan)
        {
            if (id != plan.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body.");
            }

            var existingPlan = PlanRepository.Get(plan.Id);
            if (existingPlan == null)
            {
                return NotFound("Plan not found.");
            }

            PlanRepository.Update(plan);

            return Ok("Updated Successfully");
        }


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
    }
}
