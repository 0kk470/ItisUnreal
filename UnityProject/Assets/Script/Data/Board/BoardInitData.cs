using System;
using Saltyfish.Logic;

namespace Saltyfish.Data
{
    [Serializable]
    public struct BoardCreateData
    {
        public int x;

        public int y;

        public int MineNum;

        public long Seed;

        public Unit Player;

        public Unit Boss;

        public void Reset()
        {
            Player?.Reset();
            Boss?.Reset();
        }
    }
}