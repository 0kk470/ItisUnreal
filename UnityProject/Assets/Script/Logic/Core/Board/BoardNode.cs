namespace Saltyfish.Logic
{
    public enum NodeMarkType
    {
        None,
        Flag,
        Question,
    }

    public class BoardNode
    {

        public GameBoard Board{get; set;}

        private NodeMarkType m_Mark;

        public NodeMarkType Mark 
        {
            get => m_Mark;

            set
            {
                var targetMark = value;
                if(targetMark > NodeMarkType.Question)
                    targetMark = NodeMarkType.None;
                m_Mark = targetMark;
            }
        }

        public bool IsMarked => Mark != NodeMarkType.None;

        public bool IsExplored { get; set; }

        public bool IsMine {get;set;}

        public bool IsExploredBlank => IsExplored && !IsMine;

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