using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SweetHome.Utils
{
    public interface IEmailSender {
        Task<bool> SendEmail(string to, string bcc, string subject, string content);
    }
	public class EmailSender: IEmailSender
	{
        private string apiKey;
        public EmailSender(string apiKey) {
            this.apiKey = apiKey;
        }
        public async Task<bool> SendEmail(string to, string bcc, string subject, string content) {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.sendgrid.com/v3/mail/send");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

            var response = await client.PostAsJsonAsync("", new {
                personalizations = new object[]{
                    new {
                        to = new object[]{ new { email = to }},
                        bcc = new object[]{ new { email = bcc }}
                    }
                },
                @from = new {
                    email = "admin@give-future.ru",
                    name = "Благотворительный проект GiveFuture"
                },
                subject = subject,
                content = new object[]{ new { type = "text/plain", value = content }}
            });
            return response.IsSuccessStatusCode;
        }
    }
}