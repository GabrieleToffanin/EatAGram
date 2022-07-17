using Eatagram.SDK.Interfaces;
using Eatagram.SDK.Services;
using Eatagram.WPF.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Eatagram.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override Window CreateShell()
        {
            var startingView = Container.Resolve<HomeWindow>();

            return startingView;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAuthenticationProvider, AuthenticationProvider>();
        }

        protected override void OnInitialized()
        {
            var login = Container.Resolve<LoginWindow>();
            var result = login.ShowDialog();

            if (result.Value)
                base.OnInitialized();
        }
    }
}
