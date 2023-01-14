namespace PersonService.Models
{
    public class PersonContext
    {
        public List<Person> People { get; set; }
        private readonly string path = Path.Combine(Environment.CurrentDirectory, @"Database\","people.txt");
        private string header;

        public PersonContext()
        {
            People = LoadPeople();
            header = GetHeader();
        }

        private List<Person> LoadPeople()
        {
            People = File.ReadAllLines(path).Skip(1)
                .Select(line => line.Split(';'))
                .Select(attr => new Person(attr[0], attr[1], attr[2], attr[3], attr[4], attr[5], attr[6], attr[7]))
                .ToList();

            return People;
        }

        private string GetHeader()
        {
            return File.ReadLines(path).First();
        }

        private void SaveChanges()
        {
            File.WriteAllText(path, String.Empty);
            File.WriteAllText(path, header);
            foreach (var person in People)
            {
                File.AppendAllText(path, person.ToString());
            }
        }

        public Person GetPersonById(string id)
        {
            return People.Single(p => p.Id == id);
        }

        public void CreatePerson(Person person)
        {
            People.Add(person);
            SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            People.Remove(person);
            SaveChanges();
        }

        public void UpdatePerson(string id, Person person)
        {
            People.Remove(People.Single(p => p.Id == id));
            People.Add(person);
            SaveChanges();
        }
    }
}
