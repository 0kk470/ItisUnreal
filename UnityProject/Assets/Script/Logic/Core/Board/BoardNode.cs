namespace Saltyfish.Logic
{
    public class BoardNode
    {
        public bool IsMarked { get; set; }

        public bool IsExplored { get; set; }

        public bool IsMine {get;set;}

        public int MineNum{get; set;}

        public int X{get; private set;}

        public int Y{get; private set;}

        private BoardNode(){}

        public BoardNode(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}