using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.DAL;

public partial class DataAccessLayer
{
    public static void AddStudent(
        DatabaseContext db,
        string firstName,
        string lastName,
        string email,
        int studentId,
        int gender,
        int isDorms,
        string hashedPassword)
    {
        var student = new Student();
        student.FirstName = firstName;
        student.LastName = lastName;
        student.Email = email;
        student.StudentId = studentId;
        student.Gender = gender;
        student.IsDorms = isDorms;
        student.HashedPassword = hashedPassword;

        db.Students.Add(student);
        db.SaveChanges();
    }

    public static Student? GetStudentById(DatabaseContext db, int studentId)
    {
        var student = db.Students
            .FirstOrDefault(student => student.PK_Student == studentId);

        return student;
    }

    public static Student? GetStudentByEmail(DatabaseContext db, string email)
    {
        var student = db.Students
            .FirstOrDefault(student => student.Email == email);

        return student;
    }

    public static List<Student> GetAllStudents(DatabaseContext db)
    {
        return db.Students.ToList();
    }
}