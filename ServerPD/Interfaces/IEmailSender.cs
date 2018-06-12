namespace ServerPD.Interfaces
{
    public interface IEmailSender
    {
        void Send(string body, string email);
    }
}
