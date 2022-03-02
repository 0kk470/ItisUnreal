using System;
using System.Collections.Generic;
using Saltyfish.Data;
using Saltyfish.Event;
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
        private string m_NodePrefabPath = "Prefabs/UI/Board/BoardNodeView";

        [SerializeField]
        private Button m_RestartBtn;

        [SerializeField]
        private Button m_BackMenuBtn;

        private List<BoardNodeView> m_AllNodes = new List<BoardNodeView>();

        private GameBoard m_Board = new GameBoard();

        protected override void Awake()
        {
            base.Awake();
            m_RestartBtn.onClick.AddListener(OnRestartBtnClick);
            m_BackMenuBtn.onClick.AddListener(OnBackMenuBtnClick);
            EventManager.AddListener<BoardNode>(GameEventType.OnNodeUpdate, OnNodeUpdate);
            EventManager.AddListener<bool>(GameEventType.OnGameOver, OnGameOver);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            m_RestartBtn.onClick.RemoveListener(OnRestartBtnClick);
            m_BackMenuBtn.onClick.RemoveListener(OnBackMenuBtnClick);
            EventManager.RemoveListener<BoardNode>(GameEventType.OnNodeUpdate, OnNodeUpdate);
            EventManager.RemoveListener<bool>(GameEventType.OnGameOver, OnGameOver);
            ClearAllNodes();
        }

        public override void Refresh()
        {
            base.Refresh();
            GenerateBoard();
        }

        private void OnGameOver(bool isWin)
        {
            ShowAllMines();
        }

        private void ShowAllMines()
        {
            var mines = m_AllNodes.FindAll(nodeView => nodeView.NodeData?.IsMine ?? false);
            for(int i = 0;i < mines.Count; ++i)
            {
                mines[i].NodeData.IsExplored = true;
                mines[i].Refresh();
            }
        }


        private void OnNodeUpdate(BoardNode node)
        {
            var toFind = m_AllNodes.Find(nodeView => nodeView.NodeData == node);
            toFind?.Refresh();
        }

        private void OnRestartBtnClick()
        {
            UIManager.Instance.CloseUI(UINameConfig.GameOverUI);
            GenerateBoard();
        }

        private void OnBackMenuBtnClick()
        {
            GameManager.BackToMainMenu();
        }

        private void GenerateBoard()
        {
            ClearAllNodes();
            Action<int, BoardNodeView, BoardNode> OnCreate = (idx, nodeView, nodeData) =>
            {
                nodeData.Board = m_Board;
                m_AllNodes.Add(nodeView);
            };
            m_BoardCreateData.Seed = DateTime.Now.Ticks;
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