using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Samples.BasicSamples.ViewModels.ComplexSamples.Auth
{
    [Authorize(ActiveAuthenticationSchemes = "Scheme1")]
    public class SecuredPageViewModel : DotvvmViewModelBase
    {
        private static string Message = "server: Hello!";

        public SecuredPageViewModel()
        {
            LastMessage = Message;
        }

        public string MessageEditor { get; set; }

        public string LastMessage { get; set; }

        [Authorize(Roles = "admin", ActiveAuthenticationSchemes = "Scheme1")]
        public void ReplaceMessage()
        {
            Message = LastMessage = string.Format("{0}: {1}",
                Context.HttpContext.User.Identity.Name,
                MessageEditor);
        }
    }
}