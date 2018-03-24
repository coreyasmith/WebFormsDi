using System;

namespace WebFormsDi.Models
{
    public abstract class AbstractMessageService : IMessageService
    {
        private readonly Guid _id;

        public AbstractMessageService()
        {
            _id = Guid.NewGuid();
        }

        public string GetMessage()
        {
            return $"[{_id}] {GenerateMessage()}";
        }

        protected abstract string GenerateMessage();
    }
}
