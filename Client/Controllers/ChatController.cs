using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Shared.Dto;

namespace Client.Controllers
{
    class ChatController
    {
        private RestClient _client;

        public ChatController()
        {
            _client = new RestClient("https://localhost:5000");
        }

        public IEnumerable<ChatMessageDto> GetAllMessages()
        {
            string url = "api/User/messages";
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", $"Bearer {CurrentSession.LoginedUser.Token}");
            //request.(new LoginResponceDto(){Login = CurrentSession.LoginedUser.Login});
            var responce = _client.Execute(request);
            return JsonConvert.DeserializeObject<List<ChatMessageDto>>(responce.Content);
        }
    }
}
