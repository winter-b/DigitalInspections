namespace DigitalInspectionsWebApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public enum Role { Worker, Manager}
        public Role UserRole { get; set; }
        public DateTimeOffset LastUpdated { get; set; } 

        public User(Guid id, string username, string password, Role userRole, DateTimeOffset lastUpdated)
        {
            Id = id;
            Username = username;
            Password = password;
            UserRole = userRole;
            LastUpdated = lastUpdated;
        }

        public User()
        {
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Username)}: {Username}, {nameof(Password)}: {Password}, {nameof(UserRole)}: {UserRole} {nameof(LastUpdated)}: {LastUpdated}";
        }

        protected bool Equals(User other)
        {
            return Id == other.Id && Username == other.Username && Password == other.Password && UserRole == other.UserRole && LastUpdated.Equals(other.LastUpdated);
        }


    }
}
