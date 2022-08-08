using Model;

namespace Server.Service
{
    public interface ICardService
    {
        public List<Card> GetAllCards();
        public bool TryAddCard(Card card,ref List<Card> cardList);
        public bool TryDeleteCard(Guid id, ref List<Card> cardList);
        public bool TryChangeCard(Card card,ref List<Card> cardList);
    }
}
