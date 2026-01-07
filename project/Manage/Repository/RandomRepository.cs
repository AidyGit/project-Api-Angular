using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Manage.Dtos;
using project.Manage.Interfaces;
using project.Manage.Models;
using MailKit.Net.Smtp;
using MimeKit;

//Install-Package MailKit - התקנה של חבילת MailKit

namespace project.Manage.Repository
{
    public class RandomRepository : IRandomRepository
    {
        private readonly ApplicationDbContext _context;

        public RandomRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<RandomDto>> GetWinnerToDonation()
        {
            var winners = new List<RandomDto>();
            var random = new Random();

            var donations = await _context.DonationsModel
                .Include(d => d.PurchasesModel) // עכשיו זה יזהה את המאפיין!
                .ThenInclude(p => p.User)
                .ToListAsync();

            foreach (var donation in donations)
            {
                var purchases = donation.PurchasesModel.ToList();
                if (purchases.Count == 0)
                    continue;

                int index = random.Next(purchases.Count);
                var winnerPurchase = purchases[index];

                var user = winnerPurchase.User;
                if (user == null)
                    continue;

                winners.Add(new RandomDto()
                {
                    PurchaseId = winnerPurchase.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    DonationName = donation.Name
                });
try 
    {
        await SendEmailAsync(user.Email, "מזל טוב! זכית בהגרלה", 
            $"שלום {user.Name},<br/>זכית עבור התרומה: {donation.Name}.");
    }
    catch (Exception ex)
    {
        // כדאי להוסיף לוג למקרה ששליחת המייל נכשלה כדי שהתוכנית לא תקרוס
        Console.WriteLine($"שגיאה בשליחת מייל ל-{user.Email}: {ex.Message}");
    }            }
            return winners;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("מערכת ההגרלות", "your-email@example.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                // התחברות לשרת (לדוגמה Gmail)
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // הזדהות (מומלץ להשתמש בסיסמה ייעודית לאפליקציות)
                await client.AuthenticateAsync("y0548558425@gmail.com", "216259465");

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}

