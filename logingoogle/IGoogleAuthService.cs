using System.Threading.Tasks;

namespace logingoogle
{
    public interface IGoogleAuthService
    {
        Task<UserDTO> AuthenticateAsync();
        Task LogoutAsync();
        Task<UserDTO> GetCurrentUserAsync();
    }
}
