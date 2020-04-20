namespace SoldierInfo.Domain
{
    public class SoldierBattle
    {
        public int SoldierId { get; set; }
        public Soldier Soldier { get; set; }
        public int BattleId { get; set; }
        public Battle Battle { get; set; }
    }
}
