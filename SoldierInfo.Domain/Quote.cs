namespace SoldierInfo.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Soldier Soldier { get; set; } //ref property to Soldier
        public int SoldierId { get; set; } //FK
    }
}
