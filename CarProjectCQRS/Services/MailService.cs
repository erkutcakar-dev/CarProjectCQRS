using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace CarProjectCQRS.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Car Project", _configuration["EmailSettings:FromEmail"]));
                emailMessage.To.Add(MailboxAddress.Parse(toEmail));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("html") { Text = body };

                using var client = new SmtpClient();
                await client.ConnectAsync(
                    _configuration["EmailSettings:SmtpServer"]!, 
                    int.Parse(_configuration["EmailSettings:Port"]!), 
                    true // SSL
                );
                await client.AuthenticateAsync(
                    _configuration["EmailSettings:Username"], 
                    _configuration["EmailSettings:Password"]
                );
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new Exception($"Mail gönderilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task SendContactResponseMailAsync(string toEmail, string name)
        {
            string mailBody = $@"
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #2c3e50;'>Mesajınız Alındı!</h2>
                        <p>Merhaba <strong>{name}</strong>,</p>
                        <p>Mesajınızı aldık ve en kısa sürede size dönüş yapacağız.</p>
                        <p>İlginiz için teşekkür ederiz.</p>
                        <hr style='margin: 20px 0; border: none; border-top: 1px solid #eee;'>
                        <p style='font-size: 12px; color: #666;'>
                            Bu otomatik bir mesajdır. Lütfen bu e-postaya yanıt vermeyin.
                        </p>
                    </div>
                </body>
                </html>
            ";

            await SendMailAsync(toEmail, "Mesajınız Alındı - Car Project", mailBody);
        }
    }
}
