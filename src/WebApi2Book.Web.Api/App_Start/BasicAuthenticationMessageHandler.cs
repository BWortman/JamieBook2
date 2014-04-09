// BasicAuthenticationMessageHandler.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using log4net;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;

namespace WebApi2Book.Web.Api
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        public const char AuthorizationHeaderSeparator = ':';
        private const int UsernameIndex = 0;
        private const int PasswordIndex = 1;
        private const int ExpectedCredentialCount = 2;

        public const string SchemeType = "basic";
        private readonly ILog _log;

        public BasicAuthenticationMessageHandler(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof (BasicAuthenticationMessageHandler));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                _log.Debug("Already authenticated; passing on to next handler...");
                return await base.SendAsync(request, cancellationToken);
            }

            if (!CanHandleAuthentication(request))
            {
                _log.Debug("Not a basic auth request; passing on to next handler...");
                return await base.SendAsync(request, cancellationToken);
            }

            bool isAuthenticated;
            try
            {
                isAuthenticated = Authenticate(request);
            }
            catch (Exception e)
            {
                _log.Error("Failure in auth processing", e);
                return CreateUnauthorizedResponse();
            }

            if (isAuthenticated)
            {
                var response = await base.SendAsync(request, cancellationToken);
                return response.StatusCode == HttpStatusCode.Unauthorized ? CreateUnauthorizedResponse() : response;
            }

            return CreateUnauthorizedResponse();
        }

        public bool CanHandleAuthentication(HttpRequestMessage request)
        {
            return (request.Headers != null
                    && request.Headers.Authorization != null
                    && request.Headers.Authorization.Scheme.ToLowerInvariant() == SchemeType);
        }

        public bool Authenticate(HttpRequestMessage request)
        {
            _log.Debug("Attempting to authenticate...");

            var authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                return false;
            }

            var credentials = GetCredentials(authHeader);
            if (credentials.Length != ExpectedCredentialCount)
            {
                return false;
            }

            // TODO: do it!
            return true; //SecurityService.Authenticate(GetClaims(credentials));
        }

        public Dictionary<string, string> GetClaims(string[] credentials)
        {
            var username = credentials[UsernameIndex].Trim();
            var password = credentials[PasswordIndex].Trim();
            var claims = new Dictionary<string, string>
            {
                {ClaimTypes.Name, username},
                {CustomClaimTypes.Password, password}
            };
            return claims;
        }

        public string[] GetCredentials(AuthenticationHeaderValue authHeader)
        {
            var encodedCredentials = authHeader.Parameter;
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.ASCII.GetString(credentialBytes);
            var credentialParts = credentials.Split(AuthorizationHeaderSeparator);
            return credentialParts;
        }

        public HttpResponseMessage CreateUnauthorizedResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(SchemeType));
            return response;
        }
    }
}