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
        const int ExploreRange = 1;

        private BoardNode[,] m_Nodes;

        private Unit m_Player;

        private Unit m_Boss;

        private SystemRandom m_RandomGenerator;

        public List<BoardNode> NodeList => m_Nodes.ToList();

        public bool IsInit => m_Nodes != null;

        public int CurStep {get; private set;}

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
            InitNodes(createData);
            InitSeed(createData);
            InitMines(createData);
            InitUnits(createData);
        }

        private void InitNodes(BoardCreateData createData)
        {
            CurStep = 0;
            int x = createData.x, y = createData.y;
            if (x <= 0 || y <= 0)
            {
                Debug.LogErrorFormat("Invalid x or y for gameboard, x:{0}, y:{1}", x, y);
                return;
            }
            m_Nodes = new BoardNode[x, y];
            for(int i = 0; i < x; ++i)
            {
                for(int j = 0; j < y; ++j)
                {
                    m_Nodes[i, j] = new BoardNode(i, j);
                }
            }
        }

        private void InitSeed(BoardCreateData createData)
        {
            m_RandomGenerator = new SystemRandom((int)createData.Seed);
        }

        private void InitMines(BoardCreateData createData)
        {
            int x = createData.x, y = createData.y;
            int mineNum = createData.MineNum;
            while(mineNum > 0)
            {
                int rx = m_RandomGenerator.Next(0, x);
                int ry = m_RandomGenerator.Next(0, y);
                if(!m_Nodes[rx, ry].IsMine)
                {
                    m_Nodes[rx, ry].IsMine = true;
                    mineNum--;
                }
            }
        }

        private void InitUnits(BoardCreateData createData)
        {
            m_Player = createData.Player;
            m_Boss = createData.Boss;
            if(m_Player != null)
            {
                m_Player.IsPlayer = true;
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
            for(int i = -ExploreRange; i <= ExploreRange; ++i)
            {
                for(int j = -ExploreRange; j <= ExploreRange; ++j)
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
               OnPlayerDamage(node);
               MarkAsExplored(node);
            }
            else
            {
                Explore(x, y);
            }
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
            CountMineNum(x,y);
            OnBossDamage(node);
            MarkAsExplored(node);
            if (node.MineNum > 0)
                return;
            for (int i = -ExploreRange; i <= ExploreRange; ++i)
            {
                for (int j = -ExploreRange; j <= ExploreRange; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    Explore(x + i, y + j);
                }
            }
        }

        public void MarkAsExplored(BoardNode node)
        {
            node.IsExplored = true;
            EventManager.DispatchEvent(GameEventType.OnNodeUpdate, node);
        }

        private void OnPlayerDamage(BoardNode node)
        {
            DamageInfo dmgInfo = new DamageInfo();
            dmgInfo.AttackUnit = m_Boss;
            dmgInfo.InjuredUnit = m_Player;
            dmgInfo.RelatedNode = node;
            dmgInfo.Damage = 1;
            m_Player?.TakeDamage(dmgInfo);
        }

        private void OnBossDamage(BoardNode node)
        {
            DamageInfo dmgInfo = new DamageInfo();
            dmgInfo.AttackUnit = m_Player;
            dmgInfo.InjuredUnit = m_Boss;
            dmgInfo.RelatedNode = node;
            dmgInfo.Damage = node.MineNum;
            m_Boss?.TakeDamage(dmgInfo);
        }
    }
}