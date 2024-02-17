namespace WebApplication3.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>() {
            new Student {
            Id = 1,
            StudentName = "Yukta",
            Email = "student1@gmail.com",
            Address = "Hyderabad"
            },
            new Student{
            Id = 2,
            StudentName = "Neha",
            Email = "student2@gmail.com",
            Address = "Banglore"
            }
            };
    }
}
