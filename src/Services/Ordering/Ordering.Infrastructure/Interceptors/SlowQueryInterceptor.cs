using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Diagnostics;

namespace Ordering.Infrastructure.Interceptors
{
    public class SlowQueryInterceptor(ILogger<SlowQueryInterceptor> logger) : DbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override async ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {
            _stopwatch.Stop();
            if (_stopwatch.ElapsedMilliseconds > 200) // Log queries taking longer than 200ms
            {
                logger.LogInformation($"Slow query detected: {command.CommandText} took {_stopwatch.ElapsedMilliseconds}ms");
                Console.WriteLine($"Slow query detected: {command.CommandText} took {_stopwatch.ElapsedMilliseconds}ms");
            }
            return await base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            _stopwatch.Restart();
            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
