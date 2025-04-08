namespace ExcursionAPI.Contracts.Users
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
    }
}
