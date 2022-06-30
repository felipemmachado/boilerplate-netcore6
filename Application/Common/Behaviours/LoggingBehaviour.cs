using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IUserRequest _usuarioRequest;

        public LoggingBehaviour(ILogger<TRequest> logger, IUserRequest usuarioRequest)
        {
            _logger = logger;
            _usuarioRequest = usuarioRequest;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            var accessId = _usuarioRequest.AccessId ?? string.Empty;
            var email = _usuarioRequest.Email ?? string.Empty;

            _logger.LogInformation("SmartOdonto Request: {name} {@accessId} {@email} {@Request}",
                name, accessId, email, request);

            return Task.CompletedTask;
        }
    }
}
