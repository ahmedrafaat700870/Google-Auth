using Android.App;
using Android.Gms.Auth.Api.SignIn;

namespace logingoogle
{
    public partial class GoogleAuthService 
    {
        private Activity _activity;
        private GoogleSignInOptions _gso;
        private GoogleSignInClient _googleSignInClient;
        private TaskCompletionSource<UserDTO> _taskCompletionSource;
        private Task<UserDTO> GoogleAuthentication
        {
            get => _taskCompletionSource.Task;
        }
        public  GoogleAuthService()
        {
            _activity = Platform.CurrentActivity;

            _gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                   .RequestIdToken("XXXX")
                   .RequestEmail()
                   .RequestId()
                   .RequestProfile()
                   .Build();

            _googleSignInClient = GoogleSignIn.GetClient(_activity, _gso);
            MainActivity.ResultGoogleAuth += MainActivity_ResultGoogleAuth;
        }
        public partial Task<UserDTO> AuthenticateAsync()
        {
            _taskCompletionSource = new TaskCompletionSource<UserDTO>();
            _activity.StartActivityForResult(_googleSignInClient.SignInIntent, 9001);

            return GoogleAuthentication;
        }

        public partial async Task<UserDTO> GetCurrentUserAsync()
        {
            try
            {
                var user = await _googleSignInClient.SilentSignInAsync();
                return new UserDTO
                {
                    Email = user.Email,
                    FullName = $"{user.DisplayName}",
                    Id = user.Id,
                    UserName = user.GivenName
                };

            }
            catch (Exception)
            {
                throw new Exception("Error");
            }
        }

        public partial Task LogoutAsync() => _googleSignInClient.SignOutAsync();
        private void MainActivity_ResultGoogleAuth(object sender, (bool Success, GoogleSignInAccount Account) e)
        {
            if (e.Success)
                // Set result of Task
                _taskCompletionSource.SetResult(new UserDTO
                {
                    Email = e.Account.Email,
                    FullName = e.Account.DisplayName,
                    Id = e.Account.Id,
                    UserName = e.Account.GivenName
                });
            else
                // Set Exception
                _taskCompletionSource.SetException(new Exception("Error"));
        }
    }
}
