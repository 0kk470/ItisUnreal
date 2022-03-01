using Saltyfish.Logic;
using Saltyfish.ObjectPool;
using Saltyfish.Resource;
using Saltyfish.Util;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Saltyfish.UI.Board
{
    public class BoardNodeView:UIDataContainer<BoardNode>,IPointerClickHandler,ICollectable
    {

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
                    if(m_Data.Mark == NodeMarkType.Flag)
                        m_Icon.sprite = AssetCache.Default.GetAsset<Sprite>("Sprites/Board/Flag");
                    else if(m_Data.Mark == NodeMarkType.Question)
                        m_Icon.sprite = AssetCache.Default.GetAsset<Sprite>("Sprites/Board/Question");
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(m_Data == null)
                return;
            if(m_Data.IsExplored)
                return;
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                m_Data?.Board?.Step(m_Data.X, m_Data.Y);
            }
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                m_Data.Mark += 1;
            }
        }

        public void OnUsage()
        {
            
        }

        public void OnRecycle()
        {
            m_Data = null;
        }
    }
}