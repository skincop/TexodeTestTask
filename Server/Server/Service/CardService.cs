using Model;
using Server.Serializator;

namespace Server.Service
{
    public class CardService : ICardService
    {
        private ISerializator _serilizator;

        public List<Card> GetAllCards()
        {
            return _serilizator.Deserialize();
        }


        public bool TryAddCard(Card card, ref List<Card> cardList)
        {
            try
            {
                cardList.Add(card);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool TryDeleteCard(Guid id, ref List<Card> cardList)
        {
            try
            {
                var deletedIndex = cardList.FindIndex(c => c.Id == id);
                if (deletedIndex == -1) 
                    return false;
                cardList.RemoveAt(deletedIndex);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool TryChangeCard(Card card, ref List<Card> cardList)
        {
            try
            {
                var replacedIndex = cardList.FindIndex(c => c.Id == card.Id);
                if (replacedIndex == -1)
                    return false;
                else
                    cardList[replacedIndex] = card;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CardService(ISerializator serilizator)
        {
            _serilizator = serilizator;
        }
    }
}
