
using System;
using System.Collections;
using Saltyfish.Data;
using Saltyfish.Event;
using UnityEngine;

namespace Saltyfish.Logic
{
    [Serializable]
    public class Unit
    {
        [SerializeField]
        private UnitData m_UnitData;

        public ref readonly UnitData UnitData => ref m_UnitData;

        public bool IsDead { get; private set; } = false;

        public bool IsPlayer{get;set;}

        public void Reset()
        {
            IsDead = false;
            IsPlayer = false;
            m_UnitData.Reset();
        }


        public void TakeDamage(DamageInfo dmgInfo)
        {
            SetHp(m_UnitData.Health - dmgInfo.Damage);
            Event.EventManager.DispatchEvent(Event.GameEventType.OnUnitDamaged, dmgInfo);
        }

        public int GetHP()
        {
            return (int)m_UnitData.Health;
        }

        public void SetHp(float newHp)
        {
            float prevHp = m_UnitData.Health;
            float curHp = Mathf.Clamp(newHp, 0, m_UnitData.MaxHealth);
            m_UnitData.Health = curHp;
            EventManager.DispatchEvent(Event.GameEventType.OnUnitHpChanged, this, prevHp, newHp, m_UnitData.MaxHealth);

            if (curHp <= 0)
                Die();
        }

        public void RecoverHp(float ReHp)
        {
            SetHp(m_UnitData.Health + ReHp);
            EventManager.DispatchEvent(Event.GameEventType.OnUnitRecoverHp, this, ReHp);
        }

        public void SetMaxHealth(float newMaxHealth)
        {
            float prevHp = m_UnitData.Health;
            float prevPercent = GetHpPercent();
            float prevMaxHealth = m_UnitData.MaxHealth;
            newMaxHealth = Mathf.Clamp(newMaxHealth, 0, UnitData.MAX_HP);
            m_UnitData.MaxHealth = newMaxHealth;
            if (newMaxHealth > prevMaxHealth)
            {
                m_UnitData.Health = newMaxHealth * prevPercent;
            }
            EventManager.DispatchEvent(Event.GameEventType.OnUnitHpChanged, this, prevHp, m_UnitData.Health, m_UnitData.MaxHealth);
        }

        public float GetHpPercent()
        {
            return Mathf.Clamp01(m_UnitData.Health / m_UnitData.MaxHealth);
        }


        public virtual void Die()
        {
            if (IsDead)
                return;
            IsDead = true;
            EventManager.DispatchEvent(Event.GameEventType.OnUnitDead, this);
            if (IsPlayer)
            {
                GameManager.GameOver();
            }
            else
            {
                GameManager.GameWin();
            }
        }
    }
}