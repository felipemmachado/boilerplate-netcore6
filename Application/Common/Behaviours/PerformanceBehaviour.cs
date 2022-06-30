using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest 
        : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IUserRequest _usuarioRequest;


        public PerformanceBehaviour(
            ILogger<TRequest> logger,
            IUserRequest usuarioRequest)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _usuarioRequest = usuarioRequest;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            // 5 segundos
            if (elapsedMilliseconds > 5000)
            {
                var requestName = typeof(TRequest).Name;
                var idAcesso = _usuarioRequest.AccessId ?? string.Empty;
                var email = _usuarioRequest.Email ?? string.Empty;


                _logger.LogWarning("SmartOdonto Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@IdAcesso} {@Email} {@Request}",
                    requestName, elapsedMilliseconds, idAcesso, email, request);
            }

            return response;
        }
    }
}
