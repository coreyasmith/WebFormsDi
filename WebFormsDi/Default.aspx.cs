using System;
using System.Web.UI;
using WebFormsDi.Models;

namespace WebFormsDi
{
    public partial class Default : Page
    {
        private readonly IMessageService _messageService;

        public Default() 
            : this(new DefaultMessageService())
        {
        }

        public Default(IMessageService messageService)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            InitLabel.Text = _messageService.GetMessage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLabel.Text = _messageService.GetMessage();
        }
    }
}
