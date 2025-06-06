using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace AIDentify.Repositry
{
    public class SystemUpdateRepository : ISystemUpdateRepository
    {
        private readonly ContextAIDentify _contextAIDentify;

        public SystemUpdateRepository(ContextAIDentify contextAIDentify)
        {
            _contextAIDentify = contextAIDentify;
        }

        #region Get All
        public List<SystemUpdate> GetAllSystemUpdates() 
        {
            return _contextAIDentify.SystemUpdate.Include(s => s.Admin).ToList();
        }
        #endregion

        #region Get by Id
        public SystemUpdate GetSystemUpdate(string id)
        {
            return _contextAIDentify.SystemUpdate.Include(s => s.Admin).FirstOrDefault();
        }
        #endregion

        #region Get All by Admin Id
        public List<SystemUpdate> GetAllSystemUpdateByAdminId(string adminId)
        {
            return _contextAIDentify.SystemUpdate.Where(s => s.AdminId == adminId).ToList();
        }
        #endregion

        #region Add
        public void AddSystemUpdate(string adminId, SystemUpdate systemUpdate)
        {
            var admin = _contextAIDentify.Admin.Where(a => a.Admin_ID == adminId).FirstOrDefault();
            if (admin != null)
            {
                admin.SystemUpdates.Add(systemUpdate);
                _contextAIDentify.Add(systemUpdate);
            }
        }
        #endregion

        #region Update
        public void UpdateSystemUpdate(SystemUpdate systemUpdate)
        {
            var oldSystemUpdate = _contextAIDentify.SystemUpdate.Where(s => s.Id == systemUpdate.Id).FirstOrDefault();
            if (oldSystemUpdate != null)
            {
                _contextAIDentify.Entry(oldSystemUpdate).CurrentValues.SetValues(systemUpdate);
                _contextAIDentify.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public bool DeleteSystemUpdate(SystemUpdate systemUpdate)
        {
            _contextAIDentify.SystemUpdate.Remove(systemUpdate);
            return true;
        }
        #endregion

        #region Delete All For this Admin
        public bool DeleteAllSystemUpdateForThisAdmin(string adminId)
        {
            var systemUpdates = _contextAIDentify.SystemUpdate.Where(s => s.AdminId == adminId).ToList();
            systemUpdates.RemoveRange(0, systemUpdates.Count);
            return true;
        }
        #endregion
    }
}
