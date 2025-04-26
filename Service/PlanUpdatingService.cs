using AIDentify.Models.Context;
using AIDentify.Models.Enums;

namespace AIDentify.Service
{
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

                        var subscriptions = dbContext.Subscription.Include(s => s.Plan).ToList();

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

                        foreach (var subscription in subscriptions)
                        {
                            if (subscription.StartDate == default || subscription.Plan == null)
                                continue;

                            int duration = subscription.Plan.Duration;
                            DateTime newEndDate, warningDate;
                            DateTime startDate = subscription.StartDate;

                            if (duration < 12)
                            {
                                newEndDate = startDate.AddMonths(duration);
                            }
                            else
                            {
                                int years = duration / 12;
                                int months = duration % 12;
                                newEndDate = startDate.AddYears(years).AddMonths(months);
                            }

                            warningDate = newEndDate.AddDays(-7);
                            subscription.EndDate = newEndDate;
                            subscription.WarningDate = warningDate;
                        }

                        //await dbContext.SaveChangesAsync();
                        await dbContext.SaveChangesAsync();
                    }


                    // Wait for a set interval before running again
                    await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken); // Runs every hour
                }
            }
            catch (TaskCanceledException)
            {
                throw new Exception("PlanUpdating Task was canceled.");
            }
        }
    }
}

