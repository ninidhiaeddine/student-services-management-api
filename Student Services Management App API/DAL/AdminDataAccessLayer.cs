using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.DAL;

public partial class DataAccessLayer
{
    public static void AddAdmin(
        DatabaseContext db,
        string firstName,
        string lastName,
        string email,
        string hashedPassword)
    {
        var admin = new Admin();
        admin.FirstName = firstName;
        admin.LastName = lastName;
        admin.Email = email;
        admin.HashedPassword = hashedPassword;

        db.Admins.Add(admin);
        db.SaveChanges();
    }

    public static Admin? GetAdminById(DatabaseContext db, int adminId)
    {
        var admin = db.Admins
            .FirstOrDefault(a => a.PK_Admin == adminId);

        return admin;
    }

    public static Admin? GetAdminByEmail(DatabaseContext db, string email)
    {
        var admin = db.Admins
            .FirstOrDefault(a => a.Email == email);

        return admin;
    }

    public static List<Admin> GetAllAdmins(DatabaseContext db)
    {
        return db.Admins.ToList();
    }
}