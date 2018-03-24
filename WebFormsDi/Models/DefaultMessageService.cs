namespace WebFormsDi.Models
{
    public class DefaultMessageService : AbstractMessageService
    {
        protected override string GenerateMessage()
        {
            return "Hello world!";
        }
    }
}
