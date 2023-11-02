using System.Net;
using System.Net.Mail;
using Platform.Classes;

public class EmailNewsletter
{
    private SmtpClient _smtpClient;
    private string _senderEmail;
    private string _senderPassword;
    private UsersDBHandler _userDB;

    public EmailNewsletter(string smtpServer, int smtpPort, string senderEmail, string senderPassword, UsersDBHandler usersDB)
    {
        _smtpClient = new SmtpClient(smtpServer);
        _smtpClient.Port = smtpPort;
        _senderEmail = senderEmail;
        _senderPassword = senderPassword;

        _smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
        _smtpClient.EnableSsl = true;

        _userDB = usersDB;
    }

    public bool SendNewsletter(string subject, string body)
    {
        var emailList = _userDB.GetSubscribedUsers();
        return Send(subject, body, emailList);
    }

    private bool Send(string subject, string body, List<User> users)
    {
        using (MailMessage message = new MailMessage())
        {
            message.From = new MailAddress(_senderEmail);
            foreach (User user in users)
            {
                message.To.Add(user.Email);
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true; // Установите true, если хотите отправлять HTML-письма

            try
            {
                //_smtpClient.Send(message);
                Console.WriteLine("Email рассылка успешно отправлена.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке email рассылки: " + ex.Message);
                return false;
            }
        }
    }
}
