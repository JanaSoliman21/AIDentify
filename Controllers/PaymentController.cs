using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IdGenerator _idGenerator;


        public PaymentController(IPaymentRepository paymentRepository, ISubscriptionRepository subscriptionRepository, IdGenerator idGenerator)
        {
            _paymentRepository = paymentRepository;
            _subscriptionRepository = subscriptionRepository;
            _idGenerator = idGenerator;
        }

        // Get All Payments for this User
        [HttpGet("{userId}")]
        public ActionResult<List<Payment>> GetAll(string userId)
        {
            var payments = _paymentRepository.GetAll(userId);
            if (payments == null || !payments.Any())
            {
                return NotFound("No payments found for this user.");
            }
            return Ok(payments);
        }

        // Get a Specific Payment by ID for this User
        [HttpGet("{userId}/{id}")]
        public ActionResult<Payment> Get(string userId, string id)
        {
            var payment = _paymentRepository.Get(userId, id);
            if (payment == null)
            {
                return NotFound("Payment not found.");
            }
            return Ok(payment);
        }

        //// Add a New Payment for this User
        //[HttpPost("{userId}")]
        //public ActionResult Add(string userId, [FromBody] Payment payment)
        //{
        //    try
        //    {
        //        // Save the Payment
        //        payment.Id = _idGenerator.GenerateId<Payment>(ModelPrefix.Payment);
        //        _paymentRepository.Add(userId, payment);

        //        Subscription? subscription = _subscriptionRepository.GetSubscriptionByUserId(userId);
        //        bool isNewSubscription = false;

        //        if (subscription == null)
        //        {
        //            isNewSubscription = true;
        //            subscription = new Subscription
        //            {
        //                PlanId = "1", // Default Plan (Ensure it's valid)
        //                StartDate = payment.PaymentDate,
        //                IsPaid = true
        //            };

        //            // Fetch Plan Duration (Avoids Null Reference Exception)
        //            var plan = subscription.Plan;
        //            int duration = plan?.Duration ?? 0;
        //            subscription.EndDate = subscription.StartDate.AddMonths(duration);
        //        }
        //        else
        //        {
        //            subscription.StartDate = payment.PaymentDate;

        //            // Fetch Plan Duration
        //            var plan = subscription.Plan;
        //            int duration = plan?.Duration ?? 0;

        //            subscription.EndDate = subscription.StartDate.AddMonths(duration);
        //            subscription.IsPaid = true;
        //        }

        //        // Save the Subscription
        //        if (isNewSubscription)
        //            _subscriptionRepository.AddSubscription(subscription, userId);
        //        else
        //            _subscriptionRepository.UpdateSubscription(subscription);



        //        return Ok("Payment added successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}


        // Update an Existing Payment for this User
        [HttpPut("{userId}/{id}")]
        public ActionResult Update(string userId, string id, [FromBody] Payment payment)
        {
            try
            {
                // Find the existing payment
                var existingPayment = _paymentRepository.Get(userId, id);
                if (existingPayment == null)
                {
                    return NotFound("Payment not found.");
                }

                // Update payment details
                existingPayment.Amount = payment.Amount;
                existingPayment.PaymentDate = DateTime.Now;
                existingPayment.WayOfPayment = payment.WayOfPayment;

                _paymentRepository.Update(userId, existingPayment);

                // Retrieve and update subscription if necessary
                var subscription = _subscriptionRepository.GetSubscriptionByUserId(userId);
                if (subscription != null)
                {
                    subscription.StartDate = existingPayment.PaymentDate;

                    var plan = subscription.Plan;
                    int duration = plan?.Duration ?? 0;

                    subscription.EndDate = subscription.StartDate.AddMonths(duration);
                    subscription.IsPaid = true;

                    _subscriptionRepository.UpdateSubscription(subscription);
                }

                return Ok("Payment updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        // Delete a Payment for this User
        [HttpDelete("{userId}/{paymentId}")]
        public ActionResult Delete(string userId, string paymentId)
        {
            try
            {
                // Find the existing payment
                var payment = _paymentRepository.Get(userId, paymentId);
                if (payment == null)
                {
                    return NotFound("Payment not found.");
                }

                // Delete the payment
                _paymentRepository.Delete(userId, payment);

                // Retrieve subscription related to the user
                var subscription = _subscriptionRepository.GetSubscriptionByUserId(userId);
                if (subscription != null)
                {
                    // If the payment was the most recent one, adjust the subscription
                    var latestPayment = _paymentRepository.GetAll(userId)?.OrderByDescending(p => p.PaymentDate).FirstOrDefault();

                    if (latestPayment != null)
                    {
                        subscription.StartDate = latestPayment.PaymentDate;
                        var plan = subscription.Plan;
                        int duration = plan?.Duration ?? 0;
                        subscription.EndDate = subscription.StartDate.AddMonths(duration);
                    }
                    else
                    {
                        // No payments left → Subscription is unpaid
                        subscription.IsPaid = false;
                    }

                    _subscriptionRepository.UpdateSubscription(subscription);
                }

                return Ok("Payment deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // Admin fetches all pending payments
        [HttpGet("pending")]
        public IActionResult GetPendingPayments()
        {
            var pendingPayments = _paymentRepository.GetPendingPayments();
            return Ok(pendingPayments);
        }

        // Admin approves or rejects a payment
        [HttpPut("update-status/{paymentId}")]
        public IActionResult UpdatePaymentStatus(string paymentId, [FromBody] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("You have to enter a status.");
            }

            if (Enum.TryParse(status, true, out PaymentStatues parsedStatus))
            {
                _paymentRepository.UpdateStatus(paymentId, parsedStatus);
                return Ok($"Payment {paymentId} updated to {parsedStatus}");
            }

            return BadRequest("Please write 'Pending', 'Completed', or 'Failed'.");
        }

    }

    //// DTO for updating payment status
    //public class PaymentStatusUpdateRequest
    //{
    //    public PaymentStatus Status { get; set; }
    //    public string AdminId { get; set; } // Admin who reviews the payment
    //}
}
