namespace Lab2
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }

        public Student(int id, string name, string email, string address, int age, double grade)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Age = age;
            Grade = grade;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}, Address: {Address}, Age: {Age}, Grade: {Grade}";
        }
    }
}
