using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace TaskManager.Helpers;

public class DatabasePreparation
{
    public static void PreparePopulation(IApplicationBuilder applicationBuilder, ILogger logger)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            SeedData(context, logger);
        }
    }

    private static void SeedData(ApplicationDbContext context, ILogger logger)
    {
        logger.LogInformation("Applying migration ...");

        try
        {
            context.Database.Migrate();
        }
        catch (InvalidOperationException e)
        {
            logger.LogWarning(e, "There is instance of database for testing");
        }

        if (context.Users.Any() == false && context.Tasks.Any() == false)
        {
            logger.LogInformation("Adding data");

            string[] usernames;
            string[] emails;
            string[] passwords;

            DateTime[] expirationTime;
            string[] titles;
            string[] descriptions;

            string[] roles;

            PrepareUsersData(out usernames, out emails, out passwords);
            PrepareTasksData(out expirationTime, out titles, out descriptions);
            PrepareRolesData(out roles);

            context.Users.AddRange(
                Enumerable.Range(0, 5).Select(index => new UserEntity()
                {
                    Username = usernames[Random.Shared.Next(0, usernames.Length)],
                    Email = emails[Random.Shared.Next(0, emails.Length)],
                    Password = passwords[Random.Shared.Next(0, passwords.Length)]
                }));
            context.SaveChanges();

            List<UserEntity> users = context.Users.ToList();
            string firstUserIndex = users.First().Id;
            string lastUserIndex = users.Last().Id;

            context.Roles.AddRange(
                Enumerable.Range(0, 5).Select(index => new RoleEntity()
                {
                    Role = roles[Random.Shared.Next(0, roles.Length)], 
                    Users = new List<UserEntity> { users[Random.Shared.Next(0, users.Count)] }
                }));
            context.Tasks.AddRange(
                Enumerable.Range(0, 5).Select(index => new TaskEntity()
                {
                    Title = titles[Random.Shared.Next(0, titles.Length)],
                    ExpirationTime = expirationTime[Random.Shared.Next(0, expirationTime.Length)],
                    Description = descriptions[Random.Shared.Next(0, descriptions.Length)],
                    UserId = users[Random.Shared.Next(0, users.Count)].Id    
                }));

            context.SaveChanges();
        }
        else
        {
            logger.LogInformation("Already have data - not seeding");
        }
    }

    private static void PrepareUsersData(out string[] usernames, out string[] emails,  out string[] passwords)
    {
        usernames = new[]
        {
            "Vitaliy", "John", "Boris", "Neo", "Donald",
            "Tom", "Daniel", "John", "Jeison", "Oliver"
        };
        emails = new[]
        {
            "Miller", "Smith", "Martin", "Jackson", "Wilson",
            "Taylor", "Thompson", "Jones", "Parker", "Davis"
        };
        passwords = new[]
        {
            Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()
        };
    }
    private static void PrepareRolesData(out string[] roles)
    {
        roles = new[]
        {
            "user", "admin", "moderator", "developer", "owner"
        };
    }
    private static void PrepareTasksData(out DateTime[] expirationDate, out string[] titles, 
        out string[] descriptions)
    {
        expirationDate = new[]
        {
            DateTime.Parse("03/30/2037 12:13:52"), DateTime.Parse("02/18/2025 09:32:41"), DateTime.Parse("11/21/2026 11:09:41"),
            DateTime.Parse("06/10/2155 07:30:31"), DateTime.Parse("03/04/2161 10:47:58"), DateTime.Parse("09/10/2039 07:09:36"),
            DateTime.Parse("10/07/2029 17:32:25"), DateTime.Parse("10/12/2037 09:21:31"), DateTime.Parse("08/04/2098 18:34:16")
        };

        titles = new[]
        {
            "Catch-22", "Of Mice and Men", "The Odyssey", "Lord of the Flies", "As I Lay Dying",
            "One Hundred Years Of Solitude", "Tender Is the Night", "No Country for Old Men", "To Kill a Mockingbird", "Gone with the Wind"
        };

        descriptions = new[]
        {
            "uebuw bxpae gtvkm xpsrs siimj snjbx uuxty jcfwv", "lrtotapfb adavnxyda zzrwiadef ciqiucjza fuvcbngid lqgdhpdzw peqmkmjvo blofflcla", "pxi ydn xcu tnt",
            "lrghztb kbxbqoa nhurbng ", "hvyhjhbbbgxxi kmdobfijenzux kcrvpduzhtjmo ", "zjcfxry ppcrfsw lkvhcvt sdydiqv dkowwxf eayrfsg ", "tmc qzb tco nug xuh phd kqe arp ",
            "xqscexnxftesgxsos tqjnvneddwewohwxp ", "hrwz omht rnvx czmn xzrd kzbi fkjc mvar ", "ha pp hh ye px pe eh sb ", "acepctm fxrggpc xglzcsu exwutfm "
        };
    }
}