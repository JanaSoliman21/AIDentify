﻿using AIDentify.DTO;
using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
        private readonly HttpClient _httpClient;
        private readonly ContextAIDentify _context;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository, IdGenerator idGenerator, IPaymentRepository paymentRepository, HttpClient httpClient, ContextAIDentify contextAIDentify)
        {
            SubscriptionRepository = subscriptionRepository;
            PlanRepository = planRepository;
            _idGenerator = idGenerator;
            _paymentRepository = paymentRepository;
            _httpClient = httpClient;
            _context = contextAIDentify;
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
            var subscription = SubscriptionRepository.GetSubscription(id);
            if (subscription == null)
            {
                return NotFound("Subscription not found.");
            }

            return Ok(subscription);
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
        public async Task<IActionResult> Update(string id, [FromBody] SubscriptionPaymentDto subscriptionPayment)
        {
            var payment = subscriptionPayment.Payment;
            var subscription = subscriptionPayment.Subscription;
            bool hasChange = false;
            string message = string.Empty;

            if (subscription == null)
            {
                return BadRequest("You have to add your updated Subscription.");
            }

            var existingSubscription = SubscriptionRepository.GetSubscription(id);
            // Check if the subscription exists
            if (existingSubscription == null)
            {
                return NotFound("Subscription not found.");
            }

            Plan plan = PlanRepository.Get(subscription.PlanId);

            // Check if the plan exists
            if (plan == null || !PlanRepository.PlanExists(subscription.PlanId))
            {
                return BadRequest("Plan not found.");
            }

            TimeSpan daysDifference = DateTime.Now - existingSubscription.StartDate;

            // Update the Existing Plan
            if (existingSubscription.PlanId != plan.Id)
            {

                if (daysDifference <= TimeSpan.FromDays(10))
                {
                    long priceDifference = SubscriptionRepository.DealWithPlanPriceDifference(existingSubscription, plan.Id);
                    if (priceDifference != 0)
                    {
                        if (priceDifference > 0)
                        {
                            // Handle the price difference logic here
                            if (payment == null)
                            {
                                return BadRequest("Payment is required for the price difference." + " The difference is " + priceDifference);
                            }
                            if (payment.WayOfPayment == WayOfPayment.None)
                            {
                                return BadRequest("Payment method is required.");
                            }
                            if (payment.Amount != priceDifference)
                            {
                                return BadRequest("Payment amount does not match the price difference.");
                            }
                            payment.Id = _idGenerator.GenerateId<Payment>(ModelPrefix.Payment);
                            payment.PaymentDate = DateTime.Now;
                            payment.Status = PaymentStatues.Pending;
                        }
                        else
                        {
                            // Handle the case where the new plan is cheaper
                            hasChange = true;
                            message = "You have " + Math.Abs(priceDifference) + " change.";
                        }
                    }
                }
                else
                {
                    // Handle the price difference logic here
                    if (payment == null)
                    {
                        return BadRequest("Payment is required because you have exeeded 10 days in the old plan," +
                            "to change your plan You have to renew your subscription with the new plan.");
                    }
                }

                existingSubscription.PlanId = plan.Id;
                int duration = plan.Duration;
                existingSubscription.EndDate = existingSubscription.StartDate.AddMonths(duration);
                existingSubscription.WarningDate = existingSubscription.EndDate.AddDays(-7);
            }
            else
            {
                return BadRequest("You haven't changed your plan.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // save data
                SubscriptionRepository.UpdateSubscription(existingSubscription);
                await _context.SaveChangesAsync();
                if (payment != null && hasChange == true)
                {
                    // Save the payment
                    _paymentRepository.Add(existingSubscription.DoctorId ?? existingSubscription.StudentId, payment);
                    await _context.SaveChangesAsync();
                }

                if (daysDifference > TimeSpan.FromDays(10))
                {
                    var jsonContent = new StringContent(
                        JsonSerializer.Serialize(payment),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = AddPaymentToExistingSubscription(existingSubscription.StudentId ?? existingSubscription.DoctorId, payment, existingSubscription.PlanId);

                    if (response is BadRequestObjectResult badRequestResult)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest("Failed to process the payment due to " + badRequestResult.Value);
                    }
                    else if (response is OkObjectResult okResult)
                    {
                        message = okResult.Value.ToString();
                    }
                }

                await transaction.CommitAsync();
                return Ok("Updated Successfully. " + message);
            }
            catch (Exception ex)
            {
                // If anything throws an exception, rollback
                await transaction.RollbackAsync();
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
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

            // set the subscription status
            subscription.Status = SubscriptionStatus.Avalable;

            // payment check
            if (payment.Amount != plan.Price)
            {
                return BadRequest("Payment amount does not match the plan price.");
            }

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
        public IActionResult AddPaymentToExistingSubscription(string userId, [FromBody] Payment payment, string newPlanId = "")
        {
            // get the user's subscription
            Subscription subscription = SubscriptionRepository.GetSubscriptionByUserId(userId);
            if (subscription == null)
            {
                return BadRequest("Subscription not found.");
            }

            if (subscription.IsPaid)
            {
                return BadRequest("Subscription is already paid.");
            }

            //// create a new payment
            // set the payment id
            payment.Id = _idGenerator.GenerateId<Payment>(ModelPrefix.Payment);

            // set the payment date
            payment.PaymentDate = DateTime.Now;

            // Set initial status
            payment.Status = PaymentStatues.Pending;

            if (subscription.PlanId == null || !PlanRepository.PlanExists(subscription.PlanId))
            {
                return BadRequest("Plan not found.");
            }
            Plan plan = PlanRepository.Get(subscription.PlanId);

            if (payment.WayOfPayment == WayOfPayment.None)
            {
                return BadRequest("Payment method is required.");
            }

            if (newPlanId != "")
            {
                // get the new plan
                if (!PlanRepository.PlanExists(newPlanId))
                {
                    return BadRequest("New plan not found.");
                }
                var newPlan = PlanRepository.Get(newPlanId);
                // Payment Check
                if (payment.Amount != newPlan.Price)
                {
                    return BadRequest("Payment amount does not match the plan price.");
                }
            }
            else
            {
                // Payment Check
                if (payment.Amount != plan.Price)
                {
                    return BadRequest("Payment amount does not match the plan price.");
                }
                // set the subscription as paid
                subscription.IsPaid = true;
            }

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
            if (subscription.Status != SubscriptionStatus.Expired)
            {
                subscription.Status = SubscriptionStatus.Active;
                return Ok("Welcome to your Subscription");
            }
            return NotFound("Sorry! Your Subscription has ended, you need to renew your Subscription.");
        }

        #endregion

    }
}
