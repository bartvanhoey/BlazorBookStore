using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Web.Handlers
{
    public class ValidateHeaderHandler : DelegatingHandler
    {
       protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken) {
           if (!request.Headers.Contains("Authorization"))
           {
               return new HttpResponseMessage(HttpStatusCode.BadRequest);
           }
           
           return await base.SendAsync(request, cancellationToken);
       }
    }
}