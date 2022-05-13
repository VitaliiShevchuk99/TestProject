using System.Collections.Generic;
using Backend.Data.Models;
using RestSharp;
using Shared.Dto;
using Newtonsoft.Json;


namespace Client.Controllers
{
    public class UserController
    {
        private RestClient _client;

        public UserController()
        {
            _client = new RestClient("https://localhost:5000");
        }
        public IRestResponse RegisterUser(UserModel userModel)
        {
            string url = "api/User";
            var request = new RestRequest(url, Method.POST);
            request.AddJsonBody(userModel);
            return _client.Execute(request);
        }

        public void LoginUser(UserDto user)
        {
            var url = "api/User/login";
            var request = new RestRequest(url, Method.POST);
            request.AddJsonBody(user);
            try
            {
                CurrentSession.LoginUser(JsonConvert.DeserializeObject<LoginResponceDto>(_client.Execute(request).Content));
            }
            catch
            {
                CurrentSession.LoginError();
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            string url = "api/User";
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", $"Bearer {CurrentSession.LoginedUser.Token}" );
            var responce = _client.Execute(request);
            return JsonConvert.DeserializeObject<List<UserDto>>(responce.Content);
        }
    }
}
