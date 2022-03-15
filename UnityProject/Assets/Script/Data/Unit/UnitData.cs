using System;

namespace Saltyfish.Data
{
    [Serializable]
    public struct UnitData
    {
        public float Health;

        public float MaxHealth;

        public float Attack;

#region Base Data

        public float BaseMaxHealth;

        public float BaseAttack;

        public const float MAX_HP = 999999;

#endregion

        public void Reset()
        {
            Health = 50;
            MaxHealth = 50;
            Attack = 0;
            BaseAttack = 0;
            BaseMaxHealth = 0;
        }
    }
}
