using System;

namespace Saltyfish.Data
{
    public enum UnitTempPropKey
    {
    }

    public enum EPropertyType //加成属性类型
    {
        None = 0,
        Prop_Abs_MaxHp,      //最大生命值
        Prop_Abs_ReHp,       //生命值恢复值
        Prop_Abs_Atk,        //攻击力
        Prop_Per_Atk,        //攻击力百分比加成
        Prop_Abs_Def,        //防御力
        Prop_Per_Def,        //防御力百分比加成
        Prop_Abs_DefPen,     //防御穿透
        Prop_Abs_Hit,        //命中率
        Prop_Abs_Dodge,      //闪避率
        Prop_Abs_Crit,       //暴击率
        Prop_Abs_CritDmg,    //暴击伤害加成
        Prop_Per_Mitigation,   //伤害减免率
        Prop_Max_Count,
    }


    public enum UnitStatKey //当前状态数据 键索引
    {
        None = 0,
        Stat_Abs_CurHp,      //当前生命值
        Stat_Per_CurHp,      //当前生命值百分比加成
        Stat_Abs_CurReHp,      //当前生命值恢复值
        Stat_Abs_MaxHp,       //当前最大生命值
        Stat_Abs_Atk,        //攻击力
        Stat_Abs_Def,        //防御力
        Stat_Abs_DefPen,     //防御穿透
        Stat_Abs_Lpr,        //投射物速度加成率
        Stat_Abs_Hit,        //命中率
        Stat_Abs_Dodge,      //闪避率
        Stat_Abs_Crit,       //暴击率
        Stat_Abs_CritDmg,    //暴击伤害加成
        Stat_Abs_Mitigation,   //伤害减免率
    }


    [Serializable]
    public class UnitProperty
    {
        public EPropertyType PropertyType;

        public float Value;

        public UnitProperty()
        {
            
        }

        public UnitProperty(UnitProperty prop)
        {
            PropertyType = prop.PropertyType;
            Value = prop.Value;
        }

        public UnitProperty(EPropertyType propertyType, float val)
        {
            PropertyType = propertyType;
            Value = val;
        }

        public UnitProperty Clone()
        {
            return new UnitProperty(PropertyType, Value);
        }

    }
}
