using AIDentify.Models.Context;
using AIDentify.Models.Enums;

namespace AIDentify.Service
{
    using AIDentify.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using System.Numerics;
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

                        // Get subscriptions that need to be updated
                        var Subscriptions = dbContext.Subscription.ToList();


                        if (Subscriptions.Any())
                        {
                            foreach (var subscription in Subscriptions)
                            {
                                DateTime endDate = subscription.EndDate;
                                DateTime warningDate = subscription.WarningDate;
                                var plan = dbContext.Plan.Where(p => p.Id == subscription.PlanId)
                                    .AsNoTracking()
                                    .FirstOrDefault(p => p.Id == subscription.PlanId);
                                if (plan != null)
                                {
                                    int duration = plan.Duration;
                                    if (duration < 12 && duration >= 0)
                                    {
                                        endDate = subscription.StartDate.AddMonths(duration);
                                        warningDate = endDate.AddDays(-7);
                                    }
                                    else if (duration > 12)
                                    {
                                        int yDuration = duration / 12;
                                        int restOfDuration = duration % 12;
                                        endDate = subscription.StartDate.AddYears(yDuration).AddMonths(restOfDuration);
                                        warningDate = endDate.AddDays(-7);
                                    }

                                    subscription.EndDate = endDate;
                                    subscription.WarningDate = warningDate;
                                }
                            }

                            await dbContext.SaveChangesAsync();
                        }
                    }

                    // Wait for a set interval before running again
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Runs every hour
                }
            }
            catch (TaskCanceledException)
            {
                throw new Exception("PlanUpdating Task was canceled.");
            }
        }
    }
}

