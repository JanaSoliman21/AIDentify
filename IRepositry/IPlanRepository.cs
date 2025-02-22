using AIDentify.Models;
using AIDentify.Models.Context;

namespace AIDentify.IRepositry
{
    public interface IPlanRepository
    {
        List<Plan> GetAll();
        Plan? Get(string id);
        void Add(Plan Plan);
        void Update(Plan plan);

        void Delete(Plan Plan);

    }
}
