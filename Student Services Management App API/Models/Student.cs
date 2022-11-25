using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Models;

public class Student
{
    [Key] public int PK_Student { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public int Gender { get; set; }
    public int IsDorms { get; set; }
    public string HashedPassword { get; set; } = string.Empty;

    public static void AddStudentDb(
        string firstName,
        string lastName,
        string email,
        int studentId,
        int gender,
        int isDorms,
        string hashedPassword)
    {
        using var db = new DatabaseContext();
        Console.WriteLine("Finished establishing Db Context");

        var student = new Student();
        student.FirstName = firstName;
        student.LastName = lastName;
        student.Email = email;
        student.StudentId = studentId;
        student.Gender = gender;
        student.IsDorms = isDorms;
        student.HashedPassword = hashedPassword;

        Console.WriteLine("Finished creating Student");

        db.Students.Add(student);

        Console.WriteLine("Finished adding to db");
        db.SaveChanges();


        Console.WriteLine("Finished saving");
    }

    public static Student? GetStudentByIdDb(int studentId)
    {
        using var db = new DatabaseContext();
        var student = db.Students
            .FirstOrDefault(student => student.PK_Student == studentId);

        return student;
    }
}