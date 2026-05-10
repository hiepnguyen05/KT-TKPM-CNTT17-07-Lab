using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Lab2
{
    public class StudentRepository
    {
        private readonly string _connectionString;
        private readonly IMongoCollection<Student> _collection;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase("studentsdb");
            _collection = database.GetCollection<Student>("students");
            ConnectToMongoDB();
        }

        // Hàm kết nối đến MongoDB 
        public void ConnectToMongoDB()
        {
            try
            {
                var client = new MongoClient(_connectionString);
                var database = client.GetDatabase("studentsdb");
                Console.WriteLine("✓ Kết nối đến MongoDB thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Lỗi kết nối đến MongoDB: {ex.Message}");
            }
        }

        // Phương thức thêm sinh viên vào MongoDB
        public void AddStudent(Student student)
        {
            try
            {
                _collection.InsertOne(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi thêm sinh viên: {ex.Message}");
                throw;
            }
        }

        // Phương thức cập nhật sinh viên
        public void UpdateStudent(int id, Student student)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
                var update = Builders<Student>.Update
                    .Set(s => s.Name, student.Name)
                    .Set(s => s.Email, student.Email)
                    .Set(s => s.Address, student.Address)
                    .Set(s => s.Age, student.Age)
                    .Set(s => s.Grade, student.Grade);

                _collection.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật sinh viên: {ex.Message}");
                throw;
            }
        }

        // Phương thức xoá sinh viên
        public void DeleteStudent(int id)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
                _collection.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xoá sinh viên: {ex.Message}");
                throw;
            }
        }

        // Phương thức lấy sinh viên theo ID
        public Student GetStudentById(int id)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
                return _collection.Find(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy sinh viên: {ex.Message}");
                return null;
            }
        }

        // Phương thức lấy tất cả sinh viên
        public List<Student> GetAllStudents()
        {
            try
            {
                return _collection.Find(Builders<Student>.Filter.Empty).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy danh sách sinh viên: {ex.Message}");
                return new List<Student>();
            }
        }

        // Phương thức tìm kiếm theo tên
        public List<Student> SearchByName(string name)
        {
            try
            {
                var filter = Builders<Student>.Filter.Regex(s => s.Name, new BsonRegularExpression(name, "i"));
                return _collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi tìm kiếm theo tên: {ex.Message}");
                return new List<Student>();
            }
        }

        // Phương thức tìm kiếm theo địa chỉ
        public List<Student> SearchByAddress(string address)
        {
            try
            {
                var filter = Builders<Student>.Filter.Regex(s => s.Address, new BsonRegularExpression(address, "i"));
                return _collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi tìm kiếm theo địa chỉ: {ex.Message}");
                return new List<Student>();
            }
        }

        // Phương thức tìm kiếm theo điểm
        public List<Student> SearchByGrade(double grade)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq(s => s.Grade, grade);
                return _collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi tìm kiếm theo điểm: {ex.Message}");
                return new List<Student>();
            }
        }
    }
}