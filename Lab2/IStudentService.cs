using System.Collections.Generic;

namespace Lab2
{
    public interface IStudentService
    {
        void AddStudent(Student student);
        void UpdateStudent(int id, Student student);
        void DeleteStudent(int id);
        Student GetStudentById(int id);
        List<Student> GetAllStudents();
        List<Student> SearchByName(string name);
        List<Student> SearchByAddress(string address);
        List<Student> SearchByGrade(double grade);
    }
}
