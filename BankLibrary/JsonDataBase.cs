using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BankLibrary
{
    public class JsonDataBase : IDataBase
    {
        private readonly string path;

        public JsonDataBase()
        {
            path = @"C:\Учеба\3kurs1sem\Uml\Проект\Bank\cards.json";
        }

        public Card GetCardById(int id)
        {
            var cards = JsonSerializer.Deserialize<List<Card>>(File.ReadAllText(path));
            return cards.Find(card => card.Id == id);
        }

        public Card GetCardByNumber(long number)
        {
            var cards = JsonSerializer.Deserialize<List<Card>>(File.ReadAllText(path));
            return cards.Find(card => card.Number == number);
        }

        public void ApplyChanges(Card card)
        {
            var cards = JsonSerializer.Deserialize<List<Card>>(File.ReadAllText(path));
            var index = cards.FindIndex(c => c.Id == card.Id);
            cards[index] = card;
            File.WriteAllText(path, JsonSerializer.Serialize(cards));
        }
    }
}