using AIDentify.Models.Context;
using AIDentify.Models.Enums;

namespace AIDentify.Service
{
    using AIDentify.IRepositry;
    using AIDentify.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SubscriptionFilteringService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SubscriptionFilteringService(IServiceScopeFactory scopeFactory)
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
                        IEnumerable<Subscription> UnNeededSubscriptions = dbContext.Subscription
                            .Where(s => s.DoctorId == null && s.StudentId == null)
                            .ToList();

                        if (UnNeededSubscriptions.Any())
                        {
                            dbContext.Subscription.RemoveRange(UnNeededSubscriptions);
                            await dbContext.SaveChangesAsync();
                        }
                    }

                    // Wait for a set interval before running again
                    await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
                }
            }
            catch(TaskCanceledException) 
            {
                throw new Exception("SubscriptionFiltering Task was canceled.");
            }
        }
    }
}
