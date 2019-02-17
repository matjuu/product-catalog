using System.Threading.Tasks;
using Catalog.API.Configuration;
using Catalog.API.Domain.Contracts;

namespace Catalog.API.ApplicationServices
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request)
        {
            try
            {
                return await HandleCore(request);
            }
            catch (DomainException exception)
            {
                var exceptionHttpStatusCode = DomainExceptionMappings.Get(exception.GetType());
                throw new ApiException(exception.Message, exception.Code, exceptionHttpStatusCode, exception.Properties);
            }
        }

        protected abstract Task<TResponse> HandleCore(TRequest request);
    }

    public abstract class RequestHandlerBase<TRequest> : IRequestHandler<TRequest>
    {
        public async Task Handle(TRequest request)
        {
            try
            {
                await HandleCore(request);
            }
            catch (DomainException exception)
            {
                var exceptionHttpStatusCode = DomainExceptionMappings.Get(exception.GetType());
                throw new ApiException(exception.Message, exception.Code, exceptionHttpStatusCode, exception.Properties);
            }
        }

        protected abstract Task HandleCore(TRequest request);
    }
}