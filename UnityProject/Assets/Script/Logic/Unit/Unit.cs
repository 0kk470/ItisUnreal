
using System;
using System.Collections;
using cfg;
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

        public UnitTableData TableData{get;private set;} 

        public ref readonly UnitData UnitData => ref m_UnitData;

        public bool IsDead { get; private set; } = false;

        public bool IsPlayer{get;set;}

#region UnitTable Api
        public Sprite IconSprite => Resource.AssetCache.GetCache<Sprite>().GetAsset<Sprite>(TableData?.IconPath);

        public string UnitName => TableData?.Name;

        public string UnitDescription => TableData?.Description;
#endregion
        public void Reset()
        {
            IsDead = false;
            IsPlayer = false;
            m_UnitData.Reset();
        }

        public static Unit NewUnit(int id)
        {
            var unitTable = TableConfig.ConfigData.UnitTable.GetOrDefault(id);
            if(unitTable == null)
            {
                Debug.LogError("Error, No UnitTable Config, id: " + id);
                return null;
            }
            var unit = new Unit();
            unit.TableData = unitTable;
            unit.m_UnitData.BaseMaxHealth = unitTable.BaseMaxHealth;
            unit.m_UnitData.BaseAttack = unitTable.BaseMaxHealth;
            unit.m_UnitData.BaseAttackFreq = unitTable.BaseAttackFreq;
            unit.SetAttack(unitTable.BaseAttack);
            unit.SetAttackFrequency(unitTable.BaseAttackFreq);
            unit.SetMaxHealth(unitTable.BaseMaxHealth);
            unit.SetHp(unit.UnitData.MaxHealth);
            return unit;
        }

        public void TakeDamage(DamageInfo dmgInfo)
        {
            SetHp(m_UnitData.Health - dmgInfo.Damage);
            Event.EventManager.DispatchEvent(Event.GameEventType.OnUnitDamaged, dmgInfo);
        }

        public void SetAttack(float attack)
        {
            m_UnitData.Attack = attack;
        }

        public void SetAttackFrequency(int freq)
        {
            freq = Mathf.Clamp(freq, 1, UnitData.MAX_FREQ);
            m_UnitData.AttackFreq = freq;
        }

        public void SetHp(float newHp)
        {
            float prevHp = m_UnitData.Health;
            float curHp = Mathf.Clamp(newHp, 0, m_UnitData.MaxHealth);
            m_UnitData.Health = curHp;
            EventManager.DispatchEvent(Event.GameEventType.OnUnitHpChanged, this, prevHp, curHp, m_UnitData.MaxHealth);

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

        public void OnStep(int step)
        {
            //TODO
        }

        public bool CanAttack(int step)
        {
            return step % m_UnitData.AttackFreq == 0;
        }
    }
}