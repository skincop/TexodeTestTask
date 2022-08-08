using Model;

namespace Server.Serializator
{
    public interface ISerializator
    {
        List<Card> Deserialize();
        void SerializeAndWrite(List<Card> cards);
    }
}
