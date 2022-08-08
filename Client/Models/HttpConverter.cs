using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class HttpConverter 
    {
        public StringContent ConvertJson(Card card)
        {
            var json = JsonConvert.SerializeObject(card);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            return httpContent;
        }
    }
}
