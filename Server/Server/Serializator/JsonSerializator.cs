using Model;
using Newtonsoft.Json;

namespace Server.Serializator
{
    public class JsonSerializator : ISerializator
    {
        public readonly string filename;
        public List<Card> Deserialize()
        {
            var json = File.ReadAllText(filename);
            var cards = JsonConvert.DeserializeObject<List<Card>>(json);
            return cards ?? new List<Card>();
        }

        public void SerializeAndWrite(List<Card> cards)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(cards));
        }


        public JsonSerializator(string file = "cards.json")
        {
            filename = file;
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }
        }
    }
}
