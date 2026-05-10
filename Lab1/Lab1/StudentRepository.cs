using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace La1
{
    public class StudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1 ;
        private readonly string filePath = "student.txt";
        public StudentRepository()
        {
            LoadFromFile();
        }
        public List<Student> GetAll() => _students;
        public void SaveToFile()
        {
            File.WriteAllLines(filePath, _students.Select(s => s.ToFileString()));
        }

        public void LoadFromFile()
        {
            if(!File.Exists(filePath)) return;
            foreach( var line in File.ReadAllLines(filePath))
            {
                var item = Student.FromFileString(line);
                _students.Add(item);
                if(item.Id >= _nextId)
                {
                    _nextId = item.Id + 1 ;
                }
            }
        }
        public Student Add(string name, string email, string address, int age, double grade)
        {
            var item = new Student{ Id = _nextId++, Name = name, Email = email, Address = address, Age = age, Grade = grade };
            _students.Add(item);
            SaveToFile();
            return item;
        }
        public void Delete(int id)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if(item != null)
            {
                _students.Remove(item);
                SaveToFile();
                Console.WriteLine("Xóa học sinh thành công");
            }
            else
            {
                Console.WriteLine("Xóa thất bại");
            }
        }
        
        public void Edit(int id, Student student)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if(item != null)
            {
                item.Name = student.Name;
                item.Email = student.Email;
                item.Address = student.Address;
                item.Age = student.Age;
                item.Grade = student.Grade;
                SaveToFile();
                Console.WriteLine("Sửa học sinh thành công");
            }
            else
            {
                Console.WriteLine("Sửa thất bại");
            }
        }

        public void Find(int id)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if(item != null)
            {
                Console.WriteLine(item);
            }
            else
            {
                Console.WriteLine("Không tìm thấy học sinh");
            }
        }
    }
}