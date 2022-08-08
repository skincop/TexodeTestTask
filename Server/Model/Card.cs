using Newtonsoft.Json;
namespace Model
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }


        [JsonConstructor]
        public Card(string name, byte[] image)
        {
            Id = Guid.NewGuid();
            Name = name;
            Image = image;
        }

        public Card()
        {

        }

        public Card ShallowCopy()
        {
            return (Card)this.MemberwiseClone();
        }


    }
}