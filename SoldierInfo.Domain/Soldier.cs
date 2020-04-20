using System.Collections.Generic;

namespace SoldierInfo.Domain
{
    public class Soldier
    {
        public Soldier()
        {
            Quotes = new List<Quote>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<SoldierBattle> SoldierBattles { get; set; }
        public SecretIdentity SecretIdentity { get; set; }

    }
}