using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Server.Serializator;
using Server.Service;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ICardService _cardService;
        private ISerializator _serializator;
        
        private List<Card> _cards;



        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(_cards); 
        }

        [HttpPost]
        public  IActionResult AddCard(Card card)
        {
            if (_cardService.TryAddCard(card, ref _cards))
            {
                _serializator.SerializeAndWrite(_cards);
                return Ok("Added");
            }
            else return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteCard(Card card)
        {
            if (_cardService.TryDeleteCard(card.Id, ref _cards))
            {
                _serializator.SerializeAndWrite(_cards);
                return Ok("Deleted");
            }
            else return BadRequest();
        }

        [HttpPut]
        public IActionResult ChangeCard(Card card)
        {
            if (_cardService.TryChangeCard(card, ref _cards))
            {
                _serializator.SerializeAndWrite(_cards);
                return Ok("Changed");
            }
            else return BadRequest();
        }

        public CardController(ICardService cardService, ISerializator serilizator)
        {
            _cardService = cardService;
            _serializator = serilizator;
            _cards = _serializator.Deserialize();
        }


    }
}
