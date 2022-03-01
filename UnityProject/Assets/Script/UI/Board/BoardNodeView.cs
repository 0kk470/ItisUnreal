using Saltyfish.Logic;
using Saltyfish.Resource;
using Saltyfish.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Saltyfish.UI.Board
{
    public class BoardNodeView:UIDataContainer<BoardNode>
    {

        public BoardUI BoardUI{get;set;}

        [SerializeField]
        private Image m_Icon;

        [SerializeField]
        private Text m_MineNum;

        public override void SetData(BoardNode data)
        {
            base.SetData(data);
            Refresh();
        }

        public void Refresh()
        {
            m_MineNum.gameObject.BetterSetActive(m_Data.IsExplored && !m_Data.IsMine && m_Data.MineNum > 0);
            m_MineNum.text = m_Data.MineNum.ToString();
            m_Icon.SetTransformActive((m_Data.IsExplored && m_Data.IsMine) || (!m_Data.IsExplored && m_Data.IsMarked));
            if(m_Data.IsExplored)
            {
                if(m_Data.IsMine)
                {
                    m_Icon.sprite = AssetCache.Default.GetAsset<Sprite>("Sprites/Board/Mine");
                }
            }
            else
            {
                if(m_Data.IsMarked)
                {
                    m_Icon.sprite = AssetCache.Default.GetAsset<Sprite>("Sprites/Board/Flag");
                }
            }
        }
    }
}