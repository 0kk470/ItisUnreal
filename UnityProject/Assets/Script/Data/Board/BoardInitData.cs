using System;

namespace Saltyfish.Data
{
    [Serializable]
    public struct BoardCreateData
    {
        public int x;

        public int y;

        public int MineNum;

        public long Seed;
    }
}