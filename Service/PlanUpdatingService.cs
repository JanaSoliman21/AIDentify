using AIDentify.Models.Context;
using AIDentify.Models.Enums;

namespace AIDentify.Service
{
    using AIDentify.ID_Generator;
    using AIDentify.IRepositry;
    using AIDentify.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class PlanUpdatingService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PlanUpdatingService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ContextAIDentify>();
                        var _idGenerator = scope.ServiceProvider.GetRequiredService<IdGenerator>();

                        //var subscriptions = dbContext.Subscription.Include(s => s.Plan).ToList();
                        var affectedSubscriptions = dbContext.Subscription.Where(s => s.PlanId.EndsWith("Temp")).Include(s => s.Plan).ToList();
                        var tempPlans = dbContext.Plan.Where(p => p.Id.EndsWith("Temp")).ToList();

                        #region Commented
                        #region Maybe Important
                        //var expiredSubscriptions = dbContext.Subscription
                        //.Where(s => s.EndDate < DateTime.UtcNow && s.Status == SubscriptionStatus.Active)
                        //.ToList();

                        //if (expiredSubscriptions.Any())
                        //{
                        //    foreach (var subscription in expiredSubscriptions)
                        //    {
                        //        subscription.Status = SubscriptionStatus.Expired;
                        //        //subscription.IsPaid = false; // Assuming you want to set IsPaid to false when expired
                        //    }
                        //}
                        #endregion

                        //List<Subscription> affectedSubscriptions = new List<Subscription>();
                        //foreach (var subscription in subscriptions)
                        //{
                        //    if (subscription.StartDate == default || subscription.Plan == null)
                        //        continue;

                        //    if (subscription.PlanId.EndsWith("Temp"))
                        //    {
                        //        Console.WriteLine("Found!");
                        //        affectedSubscriptions.Add(subscription);
                        //    }

                        #region Here
                        //int duration = subscription.Plan.Duration;
                        //DateTime newEndDate, warningDate;
                        //DateTime startDate = subscription.StartDate;

                        //if (duration < 12)
                        //{
                        //    newEndDate = startDate.AddMonths(duration);
                        //}
                        //else
                        //{
                        //    int years = duration / 12;
                        //    int months = duration % 12;
                        //    newEndDate = startDate.AddYears(years).AddMonths(months);
                        //}

                        //warningDate = newEndDate.AddDays(-7);
                        //subscription.EndDate = newEndDate;
                        //subscription.WarningDate = warningDate;
                        #endregion
                        //}
                        #endregion

                        foreach (var subscription in affectedSubscriptions)
                        {
                            if (subscription.WarningDate == DateTime.Now.Date)
                            {
                                //Console.WriteLine("The plan u subscriped for has been updated or no longer exists!" +
                                //    "U need to choose one of the currently offered plans at the end of your subscription!");

                                string warningMessage = "The plan you subscribed to has been updated or no longer exists! " +
                                    "You need to choose one of the currently offered plans at the end of your subscription!";

                                Notification notification = new Notification
                                {
                                    Id = _idGenerator.GenerateId<Notification>(ModelPrefix.Notification),
                                    NotificationContent = warningMessage,
                                    SentAt = DateTime.UtcNow,
                                    DoctorId = subscription.DoctorId,
                                    StudentId = subscription.StudentId
                                };
                                dbContext.Notification.Add(notification);
                            }

                            if (subscription.EndDate <= DateTime.Now)
                            {
                                //Console.WriteLine("Your subscription has ended!" +
                                //    "U need to choose one of the currently offered plans!");
                                string endWarningMessage = "Your subscription has ended! " +
                                    "You need to choose one of the currently offered plans!";
                                Notification notification = new Notification
                                {
                                    Id = _idGenerator.GenerateId<Notification>(ModelPrefix.Notification),
                                    NotificationContent = endWarningMessage,
                                    SentAt = DateTime.UtcNow,
                                    DoctorId = subscription.DoctorId,
                                    StudentId = subscription.StudentId
                                };
                                dbContext.Notification.Add(notification);

                                if (subscription.IsPaid == true)
                                {
                                    long moneyBack = subscription.Plan.Price;
                                    
                                    //Console.WriteLine("Here is your money back: " + moneyBack.ToString());
                                    string moneyMessage = "Here is your money back: " + moneyBack.ToString();
                                    notification = new Notification
                                    {
                                        Id = _idGenerator.GenerateId<Notification>(ModelPrefix.Notification),
                                        NotificationContent = moneyMessage,
                                        SentAt = DateTime.UtcNow,
                                        DoctorId = subscription.DoctorId,
                                        StudentId = subscription.StudentId
                                    };
                                    dbContext.Notification.Add(notification);

                                    subscription.IsPaid = false;
                                }

                                subscription.PlanId = null;
                            }
                        }

                        foreach (var tempPlan in tempPlans)
                        {
                            var connectedSubscriptions = dbContext.Subscription.Where(s => s.Plan ==  tempPlan).ToList();
                            if (connectedSubscriptions.Count == 0)
                            {
                                dbContext.Remove(tempPlan);
                            }
                        }



                        //await dbContext.SaveChangesAsync();
                        await dbContext.SaveChangesAsync();
                    }


                    // Wait for a set interval before running again
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Runs every hour
                }
            }
            catch (TaskCanceledException)
            {
                throw new Exception("PlanUpdating Task was canceled.");
            }
        }
    }
}

