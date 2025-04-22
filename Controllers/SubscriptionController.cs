using AIDentify.DTO;
using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository SubscriptionRepository;
        private readonly IPlanRepository PlanRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IdGenerator _idGenerator;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository, IdGenerator idGenerator, IPaymentRepository paymentRepository)
        {
            SubscriptionRepository = subscriptionRepository;
            PlanRepository = planRepository;
            _idGenerator = idGenerator;
            _paymentRepository = paymentRepository;
        }

        #region Normal CRUD Operations

        #region Get All

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(SubscriptionRepository.GetAllSubscriptions());
        }

        #endregion

        #region Get By Id

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(SubscriptionRepository.GetSubscription(id));
        }

        #endregion

        #region Get by User

        [HttpGet("user={userId}")]
        public IActionResult GetByUser(string userId)
        {
            var subscription = SubscriptionRepository.GetSubscriptionByUserId(userId);
            if (subscription == null)
            {
                return NotFound("Subscription not found.");
            }

            return Ok(subscription);
        }

        #endregion

        #region Add New Subscription (Commented)

        //[HttpPost("user={userId}")]
        //public IActionResult Add([FromBody] Subscription subscription, string userId)
        //{
        //    subscription.Id = _idGenerator.GenerateId<Subscription>(ModelPrefix.Subscription);
        //    if (subscription.PlanId == null || !PlanRepository.PlanExists(subscription.PlanId))
        //    {
        //        return BadRequest("Plan not found.");
        //    }
        //    SubscriptionRepository.AddSubscription(subscription, userId);
        //    return Ok("Posted Successfully");
        //}

        #endregion

        #region Update Subscription

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Subscription subscription)
        {
            //if (subscription.Id == null)
            //{
            //    subscription.Id = id;
            //}
            //else if (id != subscription.Id)
            //{
            //    return BadRequest("ID in the URL does not match ID in the body.");
            //}
            subscription.Id = id;
            var existingSubscription = SubscriptionRepository.GetSubscription(subscription.Id);
            if (existingSubscription == null)
            {
                return NotFound("Subscription not found.");
            }
            if (subscription.PlanId == null || !PlanRepository.PlanExists(subscription.PlanId))
            {
                return BadRequest("Plan not found.");
            }
            SubscriptionRepository.UpdateSubscription(subscription);
            return Ok("Updated Successfully");
        }

        #endregion

        #region Delete a Subscription

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existingSubscription = SubscriptionRepository.GetSubscription(id);
            if (existingSubscription == null)
            {
                return NotFound("Subscription not found.");
            }

            SubscriptionRepository.DeleteSubscription(existingSubscription);
            return Ok("Deleted Successfully");
        }

        #endregion

        #endregion

        #region Subscription-Payment Management

        #region Create a Payment for the first time for a Subscription that doesn't exist

        [HttpPost("new/user={userId}")]
        public IActionResult AddPaymentToANewSubscription(string userId, [FromBody] SubscriptionPaymentDto subscriptionPayment)
        {
            // check if the user's subscription already exists
            if (SubscriptionRepository.GetSubscriptionByUserId(userId) != null)
            {
                return BadRequest("Subscription already exists for this user.");
            }

            // get the payment and subscription
            Subscription subscription = subscriptionPayment.Subscription;
            Payment payment = subscriptionPayment.Payment;

            //// create a new payment
            // set the payment id
            payment.Id = _idGenerator.GenerateId<Payment>(ModelPrefix.Payment);

            // set the payment date
            payment.PaymentDate = DateTime.Now;

            // Set initial status
            payment.Status = PaymentStatues.Pending;

            //// create a new subscription
            // set the subscription id
            subscription.Id = _idGenerator.GenerateId<Subscription>(ModelPrefix.Subscription);

            // set the subscription start date to the payment date
            subscription.StartDate = payment.PaymentDate;

            // get the plan
            if (subscription.PlanId == null || !PlanRepository.PlanExists(subscription.PlanId))
            {
                return BadRequest("Plan not found.");
            }
            Plan plan = PlanRepository.Get(subscription.PlanId);

            // get the duration
            int duration = plan.Duration;

            // set the subscription end date to the start date + duration
            subscription.EndDate = subscription.StartDate.AddMonths(duration);

            // set the subscription warning date to the end date - 7 days
            subscription.WarningDate = subscription.EndDate.AddDays(-7);

            // set the subscription as paid
            subscription.IsPaid = true;

            // set the subscription status
            subscription.Status = SubscriptionStatus.Active;

            //// save all changes
            // save the subscription
            SubscriptionRepository.AddSubscription(subscription, userId);
            // save the payment
            _paymentRepository.Add(userId, payment);

            return Ok("Posted Successfully");
        }

        #endregion

        #region Create a Payment for a Subscription that already exists
        
        [HttpPost("existing/user={userId}")]
        public IActionResult AddPaymentToExistingSubscription(string userId, [FromBody] Payment payment)
        {
            // get the user's subscription
            Subscription subscription = SubscriptionRepository.GetSubscriptionByUserId(userId);
            if (subscription == null)
            {
                return BadRequest("Subscription not found.");
            }

            //// create a new payment
            // set the payment id
            payment.Id = _idGenerator.GenerateId<Payment>(ModelPrefix.Payment);

            // set the payment date
            payment.PaymentDate = DateTime.Now;

            // Set initial status
            payment.Status = PaymentStatues.Pending;

            // set the subscription as paid
            subscription.IsPaid = true;

            // set the subscription status
            subscription.Status = SubscriptionStatus.Active;

            //// update the subscription
            // update the subscription start date to the payment date
            subscription.StartDate = payment.PaymentDate;

            // get the plan
            if (subscription.PlanId == null || !PlanRepository.PlanExists(subscription.PlanId))
            {
                return BadRequest("Plan not found.");
            }
            Plan plan = PlanRepository.Get(subscription.PlanId);

            // get the duration
            int duration = plan.Duration;

            // set the subscription end date to the start date + duration
            DateTime newEndDate = subscription.StartDate.AddMonths(duration).AddDays((subscription.EndDate - payment.PaymentDate).TotalDays);
            subscription.EndDate = newEndDate;

            // set the subscription warning date to the end date - 7 days
            subscription.WarningDate = subscription.EndDate.AddDays(-7);


            //// save all changes
            // save the payment
            _paymentRepository.Add(userId, payment);
            // update the subscription
            SubscriptionRepository.UpdateSubscription(subscription);

            return Ok("Posted Successfully");
        }

        #endregion

        #endregion

        #region Access Subscription by UserId

        [HttpGet("access/{userId}")]
        public IActionResult AccessSubscription(string userId)
        {
            var subscription = SubscriptionRepository.GetSubscriptionByUserId(userId);
            if (subscription == null)
            {
                return NotFound("Sorry! You have no Subscription.");
            }
            if (subscription.Status == SubscriptionStatus.Active)
            {
                return Ok("Welcome to your Subscription");
            }
            return NotFound("Sorry! Your Subscription has ended, you need to renew your Subscription.");
        }

        #endregion

    }
}
