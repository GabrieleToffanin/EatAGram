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

        public ObservableCollection<RecipeContract> Recipes { get; set; } 
            = new ObservableCollection<RecipeContract>();

        private string accessToken;

        public string AccessToken
        {
            get => accessToken;
            set => SetProperty(ref accessToken, value);
        }


        public HomeWindowViewModel(IEventAggregator events)
        {
            _events = events;

            _events.GetEvent<AuthenticationSuccessfullEvent>().Subscribe(OnAuthReceived, false);
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

            if (response != null)
                foreach (var item in response.Content)
                    Recipes.Add(item);
        }
    }
}
