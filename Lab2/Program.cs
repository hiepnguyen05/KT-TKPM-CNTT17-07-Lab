using Lab2;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        
        try
        {
            // Chuỗi kết nối đến MongoDB
            const string connectionString = "mongodb://localhost:27017";
            
            // Khởi tạo Repository
            var repository = new StudentRepository(connectionString);
            
            // Khởi tạo Service
            IStudentService studentService = new StudentService(repository);
            
            // Khởi tạo UI
            var ui = new StudentUI(studentService);
            
            // Chạy ứng dụng
            ui.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khởi động ứng dụng: {ex.Message}");
        }
    }
}