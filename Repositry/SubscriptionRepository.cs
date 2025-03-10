using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace AIDentify.Repositry
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ContextAIDentify _context;

        public SubscriptionRepository(ContextAIDentify context)
        {
            _context = context;
        }

        public List<Subscription> GetAllSubscriptions()
        {
            return _context.Subscription.Include(x => x.Plan).ToList();
        }

        public Subscription? GetSubscription(string id)
        {
            return _context.Subscription.Include(s => s.Plan).FirstOrDefault(s => s.Id == id);
        }

        public void AddSubscription(Subscription subscription, string userId)
        {
            subscription.Plan = _context.Plan.Find(subscription.PlanId);
            if (subscription.EndDate == new DateTime())
            {
                subscription.StartDate = DateTime.Now;
                subscription.EndDate = subscription.StartDate.AddMonths(subscription.Plan.Duration);
            }
            subscription.WarningDate = subscription.EndDate.AddDays(-7);

            if (_context.Doctor.Any(d => d.Doctor_ID == userId))
            {
                var doctor = _context.Doctor.FirstOrDefault(d => d.Doctor_ID == userId);
                if (doctor != null)
                {
                    subscription.DoctorId = userId;
                    doctor.SubscriptionId = subscription.Id;
                }
            }
            else if (_context.Student.Any(s => s.Student_ID == userId))
            {
                var student = _context.Student.FirstOrDefault(s => s.Student_ID == userId);
                if (student != null)
                {
                    subscription.StudentId = userId;
                    student.SubscriptionId = subscription.Id;
                }
            }
            else
            {
                throw new Exception("User not found.");
            }

            _context.Subscription.Add(subscription);
            _context.SaveChanges();
        }


        public void UpdateSubscription(Subscription subscription)
        {
            var existingSubscription = _context.Subscription.Find(subscription.Id);
            if (existingSubscription != null)
            {
                _context.Entry(existingSubscription).CurrentValues.SetValues(subscription);
                _context.SaveChanges();
            }
        }

        public void DeleteSubscription(Subscription subscription)
        {
            if (subscription != null)
            {
                // get the doctor or student
                if (subscription.DoctorId != null)
                {
                    var doctor = _context.Doctor.FirstOrDefault(d => d.Doctor_ID == subscription.DoctorId);
                    if (doctor != null)
                    {
                        doctor.SubscriptionId = null;
                    }
                }
                else if (subscription.StudentId != null)
                {
                    var student = _context.Student.FirstOrDefault(s => s.Student_ID == subscription.StudentId);
                    if (student != null)
                    {
                        student.SubscriptionId = null;
                    }
                }
                _context.Subscription.Remove(subscription);
                _context.SaveChanges();
            }
        }

        public Subscription? GetSubscriptionByUserId(string userId)
        {
            return _context.Subscription
                .Include(s => s.Plan)
                .Include(s => s.Doctor)  // Include Doctor if it exists
                .Include(s => s.Student) // Include Student if it exists
                .FirstOrDefault(s => s.DoctorId == userId || s.StudentId == userId);
        }


    }
}
