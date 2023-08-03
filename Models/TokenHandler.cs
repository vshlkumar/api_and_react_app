using Microsoft.Net.Http.Headers;

namespace EntityFrameworkUI.Models
{
    public class TokenHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {            
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            
            await SaveToken(request, response);
            return response;
        }

        private async Task SaveToken(HttpRequestMessage request, HttpResponseMessage response)
        {
            var token = await response.Content.ReadAsStringAsync();
            request.Headers.Add("token", token);                       
        }
    }
}
