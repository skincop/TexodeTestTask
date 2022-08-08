using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Client.Models
{
    internal class CardService
    {
        private HttpConverter converter;
        private HttpSender sender;
        public List<Card> GetCards()
        {
            try
            {
                var request = sender.RequestAsync();
                var json = request.Content.ReadAsStringAsync().Result;
                var cards=JsonConvert.DeserializeObject <List<Card>>(json);
                return cards;
            }
            catch
            {
                return new List<Card>();
            }
        }
        public bool TryAddCard(Card card)
        {
            try
            {
                var httpContent = converter.ConvertJson(card);
                var request = sender.PostRequest(httpContent);
                if (!request.IsSuccessStatusCode) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool TryDeleteCard(Card card)
        {
            try
            {
                var httpContent = converter.ConvertJson(card);
                var request = sender.RequestAsync(HttpMethod.Delete, httpContent);
                if (!request.IsSuccessStatusCode) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryChangeCard(Card card)
        {
            try
            {
                var httpContent = converter.ConvertJson(card);
                var request = sender.RequestAsync(HttpMethod.Put, httpContent);
                if (!request.IsSuccessStatusCode) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public CardService()
        {
            converter = new HttpConverter();
            sender = new HttpSender();
        }
    }
}
