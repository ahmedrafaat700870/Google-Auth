using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logingoogle
{
    public partial class GoogleAuthService 
    {
        public partial Task<UserDTO> AuthenticateAsync();
        public partial Task<UserDTO> GetCurrentUserAsync();
        public partial Task LogoutAsync();
    }
}
