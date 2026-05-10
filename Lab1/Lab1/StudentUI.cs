namespace La1
{
    public class StudentUI
    {
        private readonly StudentService studentService = new();
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ShowStudents();
                        break;
                    case "3":
                        EditStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        FindStudent();
                        break;
                    case "0":
                        return;
                     default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng thử lại.");
                        break;
                }
                Console.WriteLine("Nhấn Enter để tiếp tục....");
                Console.ReadLine();
            }
        }
        private void ShowStudents()
        {
            var students = studentService.GetStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("Không có sinh viên nào.");
                return;
            }
            Console.WriteLine("Danh sách sinh viên:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
        private void ShowMenu()
        {
            
            Console.WriteLine("===== QUẢN LÝ SINH VIÊN =====");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Hiển thị danh sách sinh viên");
            Console.WriteLine("3. Sửa thông tin sinh viên");
            Console.WriteLine("4. Xóa sinh viên");
            Console.WriteLine("5. Tìm kiếm sinh viên");
            Console.WriteLine("0. Thoát");

        }
        public void AddStudent()
        {
            Console.Write("Nhập họ tên sinh viên: ");
            string name = Console.ReadLine();
            Console.Write("Nhập email sinh viên: ");
            string email = Console.ReadLine();
            Console.Write("Nhập địa chỉ sinh viên: ");
            string address = Console.ReadLine();
            Console.Write("Nhập tuổi sinh viên: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Nhập điểm sinh viên: ");
            double grade = double.Parse(Console.ReadLine());
            studentService.AddStudent(name, email, address, age, grade);

        }
        public void EditStudent()
        {
            try
            {
                ShowStudents();
                Console.Write("Nhập ID sinh viên cần sửa: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Nhập họ tên sinh viên: ");
                string name = Console.ReadLine();
                Console.Write("Nhập email sinh viên: ");
                string email = Console.ReadLine();
                Console.Write("Nhập địa chỉ sinh viên: ");
                string address = Console.ReadLine();
                Console.Write("Nhập tuổi sinh viên: ");
                int age = int.Parse(Console.ReadLine());
                if(age < 0)
                {
                    Console.WriteLine("Tuổi không hợp lệ");
                    return;
                }
                Console.Write("Nhập điểm sinh viên: ");
                double grade = double.Parse(Console.ReadLine());
                if(grade < 0 || grade > 10)
                {
                    Console.WriteLine("Điểm không hợp lệ");
                    return;
                }
                var student = new Student { Name = name, Email = email, Address = address, Age = age, Grade = grade };
                studentService.EditStudent(id, student);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
        public void DeleteStudent()
        {
            try
            {
                ShowStudents();
                Console.Write("Nhập ID sinh viên cần xóa: ");
                int id = int.Parse(Console.ReadLine());
                studentService.DeleteStudent(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
       
       public void FindStudent()
        {
            try
            {
                ShowStudents();
                Console.Write("Nhập ID sinh viên cần tìm: ");
                int id = int.Parse(Console.ReadLine());
                var student = studentService.GetStudentById(id);
                if (student != null)
                {
                    Console.WriteLine(student);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy sinh viên.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
    }
}