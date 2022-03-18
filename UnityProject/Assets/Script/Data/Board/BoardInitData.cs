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

        public int PlayerUnitId;

        public int BossUnitId;

        public void Reset()
        {

        }
    }
}