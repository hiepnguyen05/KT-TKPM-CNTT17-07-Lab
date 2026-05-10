namespace La1
{
    public class StudentService
    {
        private readonly StudentRepository _repo = new();
        public List<Student> GetStudents() => _repo.GetAll();
        public Student AddStudent(string name, string email, string address, int age, double grade) => _repo.Add(name, email, address, age, grade);
        public void DeleteStudent(int id) => _repo.Delete(id);
        public void EditStudent(int id, Student student) => _repo.Edit(id, student);
        public Student GetStudentById(int id) => _repo.GetAll().FirstOrDefault(s => s.Id == id);
    }
}