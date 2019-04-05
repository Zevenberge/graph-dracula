using System;
using System.Threading.Tasks;
using Dracula.Repository;
using HotChocolate.Execution;

namespace Dracula.Api
{
    public class TransactionMiddleware
    {
        private readonly QueryDelegate _next;
        public TransactionMiddleware(QueryDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public async Task InvokeAsync(IQueryContext context, DraculaDbContext database)
        {
            if (IsPollingRequest(context))
            {
                await _next(context);
                return;
            }
            Console.WriteLine("Starting transaction");
            await _next(context);
            if (IsMutation(context) && IsNotFaulted(context))
            {
                await database.SaveChangesAsync();
            }
            Console.WriteLine("Finishing transaction");
        }

        private bool IsPollingRequest(IQueryContext context)
        {
            return "IntrospectionQuery".Equals(context.Request.OperationName);
        }

        private bool IsMutation(IQueryContext context)
        {
            return context.Request.Query.Contains("mutation");
        }

        private bool IsNotFaulted(IQueryContext context)
        {
            return context.Result.Errors.Count == 0;
        }
    }
}
