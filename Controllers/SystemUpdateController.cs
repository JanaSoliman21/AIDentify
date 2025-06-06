using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class SystemUpdateController : ControllerBase
    {
        private readonly ISystemUpdateRepository _systemUpdateRepository;
        private readonly IdGenerator _idGenerator;

        public SystemUpdateController(ISystemUpdateRepository systemUpdateRepository, IdGenerator idGenerator)
        {
            _systemUpdateRepository = systemUpdateRepository;
            _idGenerator = idGenerator;
        }

        #region Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_systemUpdateRepository.GetAllSystemUpdates());
        }
        #endregion

        #region Get by Id
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_systemUpdateRepository.GetSystemUpdate(id));
        }
        #endregion

        #region Get All for this Admin
        [HttpGet("admin = {adminId}")]
        public IActionResult GetAllForAdmin(string adminId)
        {
            return Ok(_systemUpdateRepository.GetAllSystemUpdateByAdminId(adminId));
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public IActionResult UpdateSystemUpdate(string id, [FromBody] SystemUpdate systemUpdate)
        {
            var updatedSystemUpdate = _systemUpdateRepository.GetSystemUpdate(id);
            if (systemUpdate.UpdatedDescribtion != null && updatedSystemUpdate.UpdatedDescribtion != systemUpdate.UpdatedDescribtion)
            {
                updatedSystemUpdate.UpdatedDescribtion = systemUpdate.UpdatedDescribtion;
            }
            if (updatedSystemUpdate.UpdateType != UpdateType.None && updatedSystemUpdate.UpdateType != systemUpdate.UpdateType)
            {
                updatedSystemUpdate.UpdateType = systemUpdate.UpdateType;
            }

            _systemUpdateRepository.UpdateSystemUpdate(updatedSystemUpdate);
            return Ok("Update Successfully");
        }
        #endregion

        #region Delete
        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id)
        //{

        //}
        #endregion

        #region Delete All for this Admin

        #endregion
    }
}
