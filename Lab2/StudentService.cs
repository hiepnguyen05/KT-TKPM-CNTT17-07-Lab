using System.Collections.Generic;

namespace Lab2
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public void AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            
            _repository.AddStudent(student);
        }

        public void UpdateStudent(int id, Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            
            _repository.UpdateStudent(id, student);
        }

        public void DeleteStudent(int id)
        {
            _repository.DeleteStudent(id);
        }

        public Student GetStudentById(int id)
        {
            return _repository.GetStudentById(id);
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAllStudents();
        }

        public List<Student> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Student>();
            
            return _repository.SearchByName(name);
        }

        public List<Student> SearchByAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return new List<Student>();
            
            return _repository.SearchByAddress(address);
        }

        public List<Student> SearchByGrade(double grade)
        {
            return _repository.SearchByGrade(grade);
        }
    }
}
