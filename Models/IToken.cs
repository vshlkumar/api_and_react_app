using Refit;

namespace EntityFrameworkUI.Models
{
    public interface IToken
    {
        [Get("/jwt")]
        Task<string> GetJwt(string username, string password);
    }
}
