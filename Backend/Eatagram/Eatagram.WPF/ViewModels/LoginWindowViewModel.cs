using Eatagram.SDK.Interfaces;
using Eatagram.SDK.Models.Authentication;
using Eatagram.SDK.Services;
using Eatagram.WPF.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.WPF.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _events;
        private readonly IAuthenticationProvider _authenticationService;
        public DelegateCommand MicrosoftAuthenticationCommand { get; init; }

        public LoginWindowViewModel(IEventAggregator events, IAuthenticationProvider authenticationService)
        {
            _authenticationService = authenticationService;
            _events = events;

            MicrosoftAuthenticationCommand = new DelegateCommand(async () => await AuthenticateUserThroughMicrosoft());
        }

        private async Task AuthenticateUserThroughMicrosoft()
        {
            var result = await _authenticationService.AuthenticateUser();

            if (result != null)
            {
                _events.GetEvent<AuthenticationSuccessfullEvent>().Publish(result);
            }

        }


    }
}
