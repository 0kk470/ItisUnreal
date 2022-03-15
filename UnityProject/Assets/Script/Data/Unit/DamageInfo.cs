namespace Saltyfish.Logic
{
    public struct DamageInfo
    {
        public Unit AttackUnit { get; set; }

        public Unit InjuredUnit { get; set; }

        public BoardNode RelatedNode{get;set;}

        public float Damage;
    }
}