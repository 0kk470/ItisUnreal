using UnityEngine;
using Saltyfish.Data;
using System.Linq;
using System.Collections.Generic;
using Saltyfish.Util;
using Saltyfish.Event;

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
                        if(!m_Nodes[i, j].IsMine && !m_Nodes[i, j].IsExplored)
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
            var r = new SystemRandom((int)createData.Seed);
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

        private bool IsMine(int x, int y)
        {
            var node = GetNode(x, y);
            if(node == null)
                return false;
            return node.IsMine;
        }

        private void CountMineNum(int x, int y)
        {
            var node = GetNode(x, y);
            if(node == null)
                return;
            for(int i = -1; i <= 1; ++i)
            {
                for(int j = -1; j <= 1; ++j)
                {
                    if(i == 0 && j == 0) continue;
                    if(IsMine(x + i, y + j))
                    {
                        node.MineNum += 1;
                    }
                }
            }
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

        public void Explore(int x, int y)
        {
            var node = GetNode(x, y);
            if (node == null)
                return;
            if (node.IsMine)
                return;
            if(node.IsExplored)
                return;
            node.IsExplored = true;
            CountMineNum(x,y);
            EventManager.DispatchEvent(GameEventType.OnNodeUpdate, node);
            if (node.MineNum > 0)
                return;
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    Explore(x + i, y + j);
                }
            }
        }
    }
}