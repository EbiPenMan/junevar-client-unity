namespace ProGraphGroup.Games.Junevar.Models
{
    public class Profile
    {
        public string Id;
        public string Username;
        public string Name;
        public string Email;
        public string Energy;

        public Profile()
        {
        }

        public Profile(string id, string username, string name, string email, string energy)
        {
            Id = id;
            Username = username;
            Name = name;
            Email = email;
            Energy = energy;
        }
    }
}