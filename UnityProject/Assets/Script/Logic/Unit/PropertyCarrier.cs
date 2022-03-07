using System;
using System.Collections.Generic;
using System.Text;
using Saltyfish.Data;
using Saltyfish.Util;
using UnityEngine;

namespace Saltyfish.Logic
{
    public interface IPropertyCarrier
    {
        void SetProperty(EPropertyType propertyType, float Value, bool IsAdd = true);

        float GetProperty(EPropertyType propertyType);

        void ApplyProperty(ref float[] finalProperties);
    }

    [Serializable]
    public class PropertyCarrier : FlagObject, IPropertyCarrier
    {
        public static int Max_Property_Count
        {
            get
            {
                return EPropertyType.Prop_Max_Count.GetHashCode();
            }
        }

        public bool IsEmpty
        {
            get
            {
                return m_Properties.Count == 0;
            }
        }

        [SerializeField]
        protected List<UnitProperty> m_Properties = new List<UnitProperty>();

        public ref readonly List<UnitProperty> Properties =>  ref m_Properties;

        public static void InitPropertyCounter(ref float[] finalProperties)
        {
            if (finalProperties == null)
            {
                finalProperties = new float[Max_Property_Count];
            }
        }

        public virtual void ApplyProperty(ref float[] finalProperties)
        {
            InitPropertyCounter(ref finalProperties);
            foreach (var prop in m_Properties)
            {
                if(prop.PropertyType >= EPropertyType.Prop_Max_Count)
                {
                    Debug.LogError("PropType index out of range");
                    continue;
                }
                finalProperties[(int)prop.PropertyType] += prop.Value;
            }
        }

        public float GetProperty(EPropertyType propertyType)
        {
            float res = 0;
            var idx = m_Properties.FindIndex((propItem) => { return propItem.PropertyType == propertyType; });
            if(idx != -1)
            {
                res = m_Properties[idx].Value;
            }
            return res;
        }

        public void SetProperty(EPropertyType propertyType, float Value, bool IsAdd = true)
        {
            var prop = m_Properties.Find((propItem) => { return propItem.PropertyType == propertyType; });
            if(prop == null)
            {
                prop = new UnitProperty(propertyType, 0);
                m_Properties.Add(prop);
            }
            if(IsAdd)
            {
                prop.Value += Value;
            }
            else
            {
                prop.Value = Value;
            }
        }

        public void SetProperties(IList<(EPropertyType propType, float val)> properties, bool IsAdd = true)
        {
            if (properties == null)
                return;
            foreach(var propItem in properties)
            {
                SetProperty(propItem.propType, propItem.val);
            }
        }


        public virtual string FormatProperties()
        {
            if (IsEmpty)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var propItem in m_Properties)
            {
                bool isPositive = propItem.Value >= 0;
                bool isPercent = GameUtil.IsPecentProperty(propItem.PropertyType);
                var propName = GameUtil.GetPropertyName(propItem.PropertyType);
                var propFmt = string.Format("{0} {1:F1}{2}",
                                isPositive ? "+" : "-",
                                propItem.Value,
                                isPercent ? "%" : "").PadRight(15);
                sb.Append(propFmt);
                sb.Append(propName);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public override void Clear()
        {
            base.Clear();
            m_Properties.Clear();
        }
    }
}
