using UnityEngine;
using Saltyfish.Data;
using System.Linq;
using System.Collections.Generic;
using Saltyfish.Util;

namespace Saltyfish.Logic
{

    using SystemRandom = System.Random;

    public class GameBoard
    {
        private BoardNode[,] m_Nodes;

        public List<BoardNode> NodeList => m_Nodes.ToList();

        public bool IsInit => m_Nodes != null;

        public int Row => m_Nodes?.GetLength(0) ?? 0;

        public int Column => m_Nodes?.GetLength(1) ?? 0;

        public bool IsCleared 
        {
            get
            {
                if(!IsInit) return false;
                int x = Row, y = Column;
                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        if(!m_Nodes[i, j].IsExploredBlank)
                            return false;
                    }
                }
                return true;
            }
        }

        public void Init(BoardCreateData createData)
        {
            int x = createData.x, y = createData.y;
            if (x <= 0 || y <= 0)
            {
                Debug.LogErrorFormat("Invalid x or y for gameboard, x:{0}, y:{1}", x, y);
                return;
            }
            var r = new SystemRandom(createData.Seed);
            m_Nodes = new BoardNode[x, y];
            for(int i = 0; i < x; ++i)
            {
                for(int j = 0; j < y; ++j)
                {
                    m_Nodes[i, j] = new BoardNode(i, j);
                }
            }
            int mineNum = createData.MineNum;
            while(mineNum > 0)
            {
                int rx = r.Next(0, x);
                int ry = r.Next(0, y);
                if(!m_Nodes[rx, ry].IsMine)
                {
                    m_Nodes[rx, ry].IsMine = true;
                    mineNum--;
                }
            }
        }

        private bool IsValidPos(int x, int y)
        {
            if (!IsInit)
            {
                Debug.LogError("Board is not initialized");
                return false;
            }
            if (x < 0 || x >= Row)
            {
                return false;
            }
            if (y < 0 || y >= Column)
            {
                return false;
            }
            return true;
        }

        public BoardNode GetNode(int x, int y)
        {
            if (!IsValidPos(x, y))
                return null;
            return m_Nodes[x, y];
        }

        public void Step(int x, int y)
        {
            var node = GetNode(x, y);
            if(node == null)
                return;
            if(node.IsExplored)
                return;
            if(node.IsMine)
            {
                GameManager.GameOver();
            }
            Explore(x, y);
            if(IsCleared)
                GameManager.GameWin();
        }

        public bool Explore(int x, int y)
        {
            var node = GetNode(x, y);
            if (node == null)
                return false;
            if (node.IsMine)
                return true;
            if(node.IsExplored)
                return false;
            node.IsExplored = true;
            for(int i = -1; i <= 1; ++i)
            {
                for(int j = -1; j <= 1; ++j)
                {
                    if(x == 0 && y == 0) continue;
                    if(Explore(x + i, y + j))
                    {
                        node.MineNum += 1;
                    }
                }
            }
            return false;
        }
    }
}