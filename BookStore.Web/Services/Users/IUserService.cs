using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore.Web.Services.Users
{
    public interface IUserService
    {
         Task<User> LoginAsync(User user);
         Task<User> RegisterUserAsync(User user);
         Task<User> GetUserByAccessTokenAsync(string accessToken);

    }
}