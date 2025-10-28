namespace AgriConnectMarket.Application.Commands
{
    public sealed class RegisterCommand
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Optional avatar upload
        public Stream? AvatarStream { get; init; }
        public string? AvatarFileName { get; init; }
    }
}
