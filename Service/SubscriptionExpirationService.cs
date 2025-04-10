using AIDentify.Models.Context;
using AIDentify.Models.Enums;

namespace AIDentify.Service
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SubscriptionExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SubscriptionExpirationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ContextAIDentify>();

                    // Get subscriptions that need to be updated
                    var expiredSubscriptions = dbContext.Subscription
                        .Where(s => s.EndDate < DateTime.UtcNow && s.Status == SubscriptionStatus.Active)
                        .ToList();

                    if (expiredSubscriptions.Any())
                    {
                        foreach (var subscription in expiredSubscriptions)
                        {
                            subscription.Status = SubscriptionStatus.Expired;
                            //subscription.IsPaid = false; // Assuming you want to set IsPaid to false when expired
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }

                // Wait for a set interval before running again
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // Runs every hour
            }
        }
    }
}
