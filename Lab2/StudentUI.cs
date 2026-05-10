using System;
using System.Collections.Generic;

namespace Lab2
{
    public class StudentUI
    {
        private readonly IStudentService _studentService;

        public StudentUI(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllStudents();
                        break;
                    case "2":
                        AddNewStudent();
                        break;
                    case "3":
                        UpdateStudentInfo();
                        break;
                    case "4":
                        DeleteStudentInfo();
                        break;
                    case "5":
                        SearchStudent();
                        break;
                    case "6":
                        Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }

                Console.WriteLine("\nNhấn bất kỳ phím nào để tiếp tục...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║    QUẢN LÝ SINH VIÊN - MONGODB         ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Hiển thị danh sách sinh viên        ║");
            Console.WriteLine("║ 2. Thêm sinh viên mới                  ║");
            Console.WriteLine("║ 3. Sửa thông tin sinh viên             ║");
            Console.WriteLine("║ 4. Xoá sinh viên                       ║");
            Console.WriteLine("║ 5. Tìm kiếm sinh viên                  ║");
            Console.WriteLine("║ 6. Thoát                               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.Write("Nhập lựa chọn của bạn (1-6): ");
        }

        private void DisplayAllStudents()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine("                    DANH SÁCH CÁC SINH VIÊN");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");

            try
            {
                var students = _studentService.GetAllStudents();

                if (students.Count == 0)
                {
                    Console.WriteLine("Không có sinh viên nào trong danh sách.");
                    return;
                }

                Console.WriteLine(string.Format("{0,-5} {1,-15} {2,-20} {3,-15} {4,-5} {5,-8}",
                    "ID", "Tên", "Email", "Địa chỉ", "Tuổi", "Điểm"));
                Console.WriteLine("───────────────────────────────────────────────────────────────────");

                foreach (var student in students)
                {
                    Console.WriteLine(string.Format("{0,-5} {1,-15} {2,-20} {3,-15} {4,-5} {5,-8}",
                        student.Id, student.Name, student.Email, student.Address, student.Age, student.Grade));
                }

                Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách sinh viên: {ex.Message}");
            }
        }

        private void AddNewStudent()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine("                    THÊM SINH VIÊN MỚI");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");

            try
            {
                Console.Write("Nhập ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID không hợp lệ!");
                    return;
                }

                Console.Write("Nhập tên sinh viên: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Tên không được để trống!");
                    return;
                }

                Console.Write("Nhập email: ");
                string email = Console.ReadLine();

                Console.Write("Nhập địa chỉ: ");
                string address = Console.ReadLine();

                Console.Write("Nhập tuổi: ");
                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.WriteLine("Tuổi không hợp lệ!");
                    return;
                }

                Console.Write("Nhập điểm: ");
                if (!double.TryParse(Console.ReadLine(), out double grade))
                {
                    Console.WriteLine("Điểm không hợp lệ!");
                    return;
                }

                var student = new Student(id, name, email, address, age, grade);
                _studentService.AddStudent(student);
                Console.WriteLine("✓ Thêm sinh viên thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm sinh viên: {ex.Message}");
            }
        }

        private void UpdateStudentInfo()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine("                    SỬA THÔNG TIN SINH VIÊN");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");

            try
            {
                Console.Write("Nhập ID sinh viên cần sửa: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID không hợp lệ!");
                    return;
                }

                var existingStudent = _studentService.GetStudentById(id);
                if (existingStudent == null)
                {
                    Console.WriteLine($"Không tìm thấy sinh viên có ID {id}");
                    return;
                }

                Console.WriteLine($"\nThông tin hiện tại: {existingStudent}");
                Console.WriteLine("\nNhập thông tin mới (để trống để giữ nguyên):");

                Console.Write("Tên (để trống để giữ nguyên): ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                    name = existingStudent.Name;

                Console.Write("Email (để trống để giữ nguyên): ");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email))
                    email = existingStudent.Email;

                Console.Write("Địa chỉ (để trống để giữ nguyên): ");
                string address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                    address = existingStudent.Address;

                Console.Write("Tuổi (để trống để giữ nguyên): ");
                string ageInput = Console.ReadLine();
                int age = string.IsNullOrWhiteSpace(ageInput) ? existingStudent.Age : int.Parse(ageInput);

                Console.Write("Điểm (để trống để giữ nguyên): ");
                string gradeInput = Console.ReadLine();
                double grade = string.IsNullOrWhiteSpace(gradeInput) ? existingStudent.Grade : double.Parse(gradeInput);

                var updatedStudent = new Student(id, name, email, address, age, grade);
                _studentService.UpdateStudent(id, updatedStudent);
                Console.WriteLine("✓ Cập nhật thông tin sinh viên thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi sửa thông tin sinh viên: {ex.Message}");
            }
        }

        private void DeleteStudentInfo()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine("                    XOÁ SINH VIÊN");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");

            try
            {
                Console.Write("Nhập ID sinh viên cần xoá: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID không hợp lệ!");
                    return;
                }

                var student = _studentService.GetStudentById(id);
                if (student == null)
                {
                    Console.WriteLine($"Không tìm thấy sinh viên có ID {id}");
                    return;
                }

                Console.WriteLine($"Thông tin sinh viên: {student}");
                Console.Write("Bạn có chắc chắn muốn xoá sinh viên này? (y/n): ");
                
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    _studentService.DeleteStudent(id);
                    Console.WriteLine("✓ Xoá sinh viên thành công!");
                }
                else
                {
                    Console.WriteLine("Hủy xoá sinh viên.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xoá sinh viên: {ex.Message}");
            }
        }

        private void SearchStudent()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine("                    TÌM KIẾM SINH VIÊN");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine("1. Tìm kiếm theo ID");
            Console.WriteLine("2. Tìm kiếm theo Tên");
            Console.WriteLine("3. Tìm kiếm theo Địa chỉ");
            Console.WriteLine("4. Tìm kiếm theo Điểm");
            Console.Write("Nhập lựa chọn (1-4): ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        SearchById();
                        break;
                    case "2":
                        SearchByName();
                        break;
                    case "3":
                        SearchByAddress();
                        break;
                    case "4":
                        SearchByGrade();
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tìm kiếm: {ex.Message}");
            }
        }

        private void SearchById()
        {
            Console.Write("\nNhập ID cần tìm: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = _studentService.GetStudentById(id);
                if (student != null)
                {
                    Console.WriteLine($"\nKết quả tìm kiếm:\n{student}");
                }
                else
                {
                    Console.WriteLine($"\nKhông tìm thấy sinh viên có ID {id}");
                }
            }
            else
            {
                Console.WriteLine("ID không hợp lệ!");
            }
        }

        private void SearchByName()
        {
            Console.Write("\nNhập tên cần tìm: ");
            string name = Console.ReadLine();

            var results = _studentService.SearchByName(name);
            DisplaySearchResults(results, "tên");
        }

        private void SearchByAddress()
        {
            Console.Write("\nNhập địa chỉ cần tìm: ");
            string address = Console.ReadLine();

            var results = _studentService.SearchByAddress(address);
            DisplaySearchResults(results, "địa chỉ");
        }

        private void SearchByGrade()
        {
            Console.Write("\nNhập điểm cần tìm: ");
            if (double.TryParse(Console.ReadLine(), out double grade))
            {
                var results = _studentService.SearchByGrade(grade);
                DisplaySearchResults(results, "điểm");
            }
            else
            {
                Console.WriteLine("Điểm không hợp lệ!");
            }
        }

        private void DisplaySearchResults(List<Student> results, string searchType)
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine($"                 KẾT QUẢ TÌM KIẾM THEO {searchType.ToUpper()}");
            Console.WriteLine("═══════════════════════════════════════════════════════════════════");

            if (results.Count == 0)
            {
                Console.WriteLine($"Không tìm thấy sinh viên nào theo {searchType}.");
                return;
            }

            Console.WriteLine(string.Format("{0,-5} {1,-15} {2,-20} {3,-15} {4,-5} {5,-8}",
                "ID", "Tên", "Email", "Địa chỉ", "Tuổi", "Điểm"));
            Console.WriteLine("───────────────────────────────────────────────────────────────────");

            foreach (var student in results)
            {
                Console.WriteLine(string.Format("{0,-5} {1,-15} {2,-20} {3,-15} {4,-5} {5,-8}",
                    student.Id, student.Name, student.Email, student.Address, student.Age, student.Grade));
            }

            Console.WriteLine("═══════════════════════════════════════════════════════════════════");
            Console.WriteLine($"Tổng cộng: {results.Count} sinh viên");
        }
    }
}
