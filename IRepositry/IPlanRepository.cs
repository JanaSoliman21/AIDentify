using AIDentify.Models;
using AIDentify.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace AIDentify.IRepositry
{
    public class IPlanRepository
    {
        private readonly ContextAIDentify _context;

        public IPlanRepository(ContextAIDentify context)
        {
            _context = context;
        }

        public List<Plan> GetAll()
        {
            return _context.Plan.ToList();
        }

        public Plan? Get(string id)
        {
            return _context.Plan.Find(id);
        }
        
        public void Add(Plan Plan)
        {
            _context.Plan.Add(Plan);
            _context.SaveChanges();
        }

        public void Update(Plan plan)
        {
            var existingPlan = _context.Plan.Find(plan.PlanId);

            if (existingPlan != null)
            {
                _context.Entry(existingPlan).CurrentValues.SetValues(plan);
                _context.SaveChanges();
            }
        }


        public void Delete(Plan Plan)
        {
            if (Plan != null)
            {
                _context.Plan.Remove(Plan);
                _context.SaveChanges();
            }
        }

    }
}
