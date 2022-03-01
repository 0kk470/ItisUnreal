using System;
using System.Collections.Generic;
using Saltyfish.Data;
using Saltyfish.Logic;
using Saltyfish.ObjectPool;
using Saltyfish.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Saltyfish.UI.Board
{
    public class BoardUI:UIBase
    {
        [SerializeField]
        private BoardCreateData m_BoardCreateData;

        [SerializeField]
        private GridLayoutGroup m_Grid;

        [SerializeField]
        private string m_NodePrefabPath = "Prefab/UI/Board/BoardNodeView";

        private List<BoardNodeView> m_AllNodes = new List<BoardNodeView>();

        private GameBoard m_Board = new GameBoard();

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ClearAllNodes();
        }

        private void GenerateBoard()
        {
            ClearAllNodes();
            Action<int, BoardNodeView, BoardNode> OnCreate = (idx, nodeView, nodeData) =>
            {
                m_AllNodes.Add(nodeView);
            };

            m_Board.Init(m_BoardCreateData);
            var nodeList = m_Board.NodeList;

            MonoUtil.GenerateMonoElementsWithPool(m_NodePrefabPath, nodeList, m_Grid.transform, OnCreate);

            m_Grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            m_Grid.constraintCount = m_Board.Column;
            m_Grid.SetLayoutVertical();
        }

        public void ClearAllNodes()
        {
            foreach(var node in m_AllNodes)
            {
                GoPoolMgr.RecycleComponent(node, m_NodePrefabPath);
            }
            m_AllNodes.Clear();
        }
    }
}