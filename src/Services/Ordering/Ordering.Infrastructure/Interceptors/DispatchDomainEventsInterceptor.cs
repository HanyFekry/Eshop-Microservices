using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;
            var aggregates = context.ChangeTracker.Entries<IAggregate>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();
            var domainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();

            aggregates.ForEach(x => x.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
