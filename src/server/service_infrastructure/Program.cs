using Microsoft.EntityFrameworkCore;
using service_infrastructure.entities;
using service_infrastructure.infrastructure.database;
using System.Linq;
namespace service_infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Out.WriteLine("ServiceInfrastructure Test");
        
        var context = new AppDbContext();
        
        var user1 = new UserAccount { id = Guid.NewGuid(), username = "jack", password_hash = Guid.NewGuid().ToString() };
        
        context.UserAccounts.Add(user1);
        context.SaveChanges();

        var addedUser = context.UserAccounts.FirstOrDefault(user => user.username == "jack");

        Console.Out.WriteLine("addedUser: {0}", addedUser.username.ToString());
    }
}