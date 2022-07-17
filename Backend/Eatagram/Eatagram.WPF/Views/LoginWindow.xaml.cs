﻿using Eatagram.SDK.Models.Authentication;
using Eatagram.WPF.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eatagram.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(IEventAggregator events)
        {
            InitializeComponent();
            events.GetEvent<AuthenticationSuccessfullEvent>().Subscribe(_ => OnAuthSuccess(), ThreadOption.UIThread);
        }

        private void OnAuthSuccess()
        {
            DialogResult = true;
        }

    }
}
