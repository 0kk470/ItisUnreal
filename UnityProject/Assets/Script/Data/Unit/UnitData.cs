using System;

namespace Saltyfish.Data
{
    [Serializable]
    public struct UnitData
    {
        public float Health;

        public float MaxHealth;

        public float Attack;

        public int AttackFreq;

#region Base Data

        public float BaseMaxHealth;

        public float BaseAttack;

        public int BaseAttackFreq;

        public const float MAX_HP = 999999;

        public const int MAX_FREQ = 999;
#endregion

        public void Reset()
        {
            Health = 50;
            MaxHealth = 50;
            Attack = 0;
            BaseAttack = 0;
            BaseMaxHealth = 0;
            AttackFreq = 1;
            BaseAttackFreq = 1;
        }
    }
}
