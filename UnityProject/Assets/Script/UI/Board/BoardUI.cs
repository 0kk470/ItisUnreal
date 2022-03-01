using Saltyfish.Data;
using Saltyfish.Logic;
using Saltyfish.Resource;
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

        private GameBoard m_Board = new GameBoard();

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void GenerateBoard()
        {
            m_Board.Init(m_BoardCreateData);
        }


    }
}