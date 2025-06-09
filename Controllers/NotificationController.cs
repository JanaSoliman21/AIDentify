using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IdGenerator _idGenerator;

        public NotificationController(INotificationRepository notificationRepository, IdGenerator idGenerator)
        {
            _notificationRepository = notificationRepository;
            _idGenerator = idGenerator;
        }


        [HttpGet]
        public ActionResult<List<Notification?>> GetAllNotifications()
        {
            return Ok(_notificationRepository.GetAllNotifications());
        }

        [HttpGet("{userId}")]
        public ActionResult<List<Notification?>> GetNotificationsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID cannot be null or empty.");
            }
            return Ok(_notificationRepository.GetNotificationsByUserId(userId));
        }       // here

        [HttpGet("notification/{id}")]
        public ActionResult<Notification?> GetNotification(string id)
        {
            var notification = _notificationRepository.GetNotification(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost("{userId}")]
        public ActionResult AddNotification([FromBody] string notificationContent, string userId)
        {
            Notification notification = new Notification
            {
                Id = _idGenerator.GenerateId<Notification>(ModelPrefix.Notification),
                NotificationContent = notificationContent,
                SentAt = DateTime.UtcNow
            };

            _notificationRepository.AddNotification(notification, userId);
            return Ok("Created Successfully");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateNotification([FromBody] string notificationNewContent, string id)
        {
            Notification notification = new Notification
            {
                Id = id,
                NotificationContent = notificationNewContent,
            };
            _notificationRepository.UpdateNotification(notification);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNotification(string id)
        {
            var notification = _notificationRepository.GetNotification(id);
            if (notification == null)
            {
                return NotFound();
            }
            _notificationRepository.DeleteNotification(notification);
            return Ok("Deleted Successfully");
        }

        [HttpDelete("user/{userId}")]
        public ActionResult DeleteAllNotificationsByUserId(string userId)
        {
            _notificationRepository.DeleteAllNotificationsByUserId(userId);
            return Ok("All Deleted Successfully");
        }
    }
}
