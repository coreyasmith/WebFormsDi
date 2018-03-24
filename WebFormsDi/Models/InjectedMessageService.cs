namespace WebFormsDi.Models
{
    public class InjectedMessageService : AbstractMessageService
    {
        protected override string GenerateMessage()
        {
            return "This message is injected!";
        }
    }
}
