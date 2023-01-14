namespace PersonService.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }

        public string UserName { get; set; }
        public string Notes { get; set; }

        public Person(string id, string firstName, string lastName, string email, string gender, string active, string userName, string notes)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
            Active = bool.Parse(active);
            UserName = userName;
            Notes = notes;
        }

        public override string ToString()
        {
            return $"\n{Id};{FirstName};{LastName};{Email};{Gender};{Active};{UserName};{Notes}";
        }
    }
}
