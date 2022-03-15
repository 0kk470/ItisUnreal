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

        public BoardNode NodeData => m_Data;    

        [SerializeField]
        private Image m_Bg;

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
            RefreshText();
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
            m_Bg.color = m_Data.IsExplored ? Color.gray : Color.white;
        }

        private void RefreshText()
        {
            m_MineNum.gameObject.BetterSetActive(m_Data.IsExplored && !m_Data.IsMine && m_Data.MineNum > 0);
            m_MineNum.text = m_Data.MineNum.ToString();
            m_MineNum.color = GetNumColor(m_Data.MineNum);
        }

        private Color GetNumColor(int num)
        {
            Color color = Color.black;
            switch(num)
            {
                case 1:
                    color = Color.blue;
                    break;
                case 2:
                    color = Color.yellow;
                    break;
                case 3:
                    color = Color.red;
                    break;
                case 4:
                    color = Color.green;
                    break;
                case 5:
                    color = Color.magenta;
                    break;
                case 6:
                    color = Color.cyan;
                    break;
                case 7:
                    color = Util.UnityExtension.HtmlString2Color("#FF9000"); //orange
                    break;
                case 8:
                    color = Util.UnityExtension.HtmlString2Color("#9800FF"); //purple
                    break;
            }
            return color;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if(GameManager.IsBusy)
                return;
            if(m_Data == null)
                return;
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                m_Data?.Board?.Step(m_Data.X, m_Data.Y);
            }
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                if(m_Data.IsExplored)
                    return;
                m_Data.Mark += 1;
                Refresh();
            }
        }

        public void OnUsage()
        {
            
        }

        public void OnRecycle()
        {
            if(m_Data != null)
            {
                m_Data.Board = null;
                m_Data = null;
            }
        }
    }
}