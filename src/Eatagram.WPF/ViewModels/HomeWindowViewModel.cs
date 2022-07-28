using Eatagram.SDK.Interfaces;
using Eatagram.SDK.Models.Authentication;
using Eatagram.SDK.Models.Contracts;
using Eatagram.SDK.Services;
using Eatagram.WPF.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.WPF.ViewModels
{
    public class HomeWindowViewModel : BindableBase
    {
        private AuthenticationToken _authToken;
        private readonly IEventAggregator _events;
        private IEatagramRecipesService _recipesService;

        public ObservableCollection<RecipeContract> Recipes => new();

        private string _accessToken;

        private string AccessToken
        {
            get => _accessToken;
            set => SetProperty(ref _accessToken, value);
        }


        /// <inheritdoc />
        public HomeWindowViewModel(IEventAggregator events)
        {
            _events = events;

            _events.GetEvent<SuccessAuthenticationEvent>().Subscribe(OnAuthReceived, false);
        }

        private void OnAuthReceived(AuthenticationToken token)
        {
            _authToken = token;
            AccessToken = _authToken.Token;
            _recipesService = new EatagramRecipesService(token);
        }

        private async Task LoadRecipes()
        {
            var response = await _recipesService.FetchRecipes();

            if (response.Content != null)
                foreach (var item in response.Content)
                    Recipes.Add(item);
        }
    }
}
