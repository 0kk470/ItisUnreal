using System;
using cfg;
using Saltyfish.Event;
using Saltyfish.Logic;
using Saltyfish.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace Saltyfish.UI.Board
{
    public class BoardUnitView : UIDataContainer<Unit>
    {
        [SerializeField]
        private Image m_Icon;

        [SerializeField]
        private HealthBar m_HpBar;

        void Awake()
        {
            EventManager.AddListener<Unit, float, float, float>(GameEventType.OnUnitHpChanged, OnUnitHpChanged);
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<Unit, float, float, float>(GameEventType.OnUnitHpChanged, OnUnitHpChanged);
        }

        public override void SetData(Unit data)
        {
            base.SetData(data);
            Refresh();
        }

        private void Refresh()
        {
            if(m_Data == null)
                return;
            m_HpBar.SetHealthValue(m_Data.UnitData.Health, m_Data.UnitData.MaxHealth);
            m_Icon.sprite = m_Data.IconSprite;
        }

        private void OnUnitHpChanged(Unit unit, float prevHp, float curHp, float maxHp)
        {
            if (unit == null || unit != m_Data)
                return;
            m_HpBar.SetHealthValue(curHp, maxHp);
        }
    }
}