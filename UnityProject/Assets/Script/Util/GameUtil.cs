using System;
using Saltyfish.Data;

namespace Saltyfish.Util
{
    public static class GameUtil
    {
        public static bool IsPecentProperty(EPropertyType propertyType)
        {
            return propertyType == EPropertyType.Prop_Per_Atk ||
                propertyType == EPropertyType.Prop_Per_Def ||
                // propertyType == EPropertyType.Prop_Per_MaxHp ||
                // propertyType == EPropertyType.Prop_Per_MaxMp ||
                // propertyType == EPropertyType.Prop_Per_Speed ||
                propertyType == EPropertyType.Prop_Abs_Crit ||
                propertyType == EPropertyType.Prop_Abs_CritDmg ||
                propertyType == EPropertyType.Prop_Abs_Hit ||
                propertyType == EPropertyType.Prop_Abs_Dodge ||
                // propertyType == EPropertyType.Prop_Abs_Lpr ||
                // propertyType == EPropertyType.Prop_Abs_Asr ||
                // propertyType == EPropertyType.Prop_Abs_Hsr ||
                propertyType == EPropertyType.Prop_Per_Mitigation;
            // || propertyType == EPropertyType.Prop_Per_MaxEnergy;
        }

        public static string GetPropertyName(EPropertyType propertyType)
        {
            if (propertyType == EPropertyType.None || propertyType == EPropertyType.Prop_Max_Count)
                return string.Empty;
            string result = string.Empty;
            switch (propertyType)
            {
                case EPropertyType.Prop_Abs_MaxHp:
                    // result = KBLanguage.GetText(KBLanguageID.ID_MaxHealth);
                    break;
                case EPropertyType.Prop_Abs_ReHp:
                    // result = KBLanguage.GetText(KBLanguageID.ID_RecoverHp);
                    break;
                case EPropertyType.Prop_Abs_Atk:
                case EPropertyType.Prop_Per_Atk:
                    // result = KBLanguage.GetText(KBLanguageID.ID_Attack);
                    break;
                case EPropertyType.Prop_Abs_Def:
                case EPropertyType.Prop_Per_Def:
                    // result = KBLanguage.GetText(KBLanguageID.ID_Defense);
                    break;
                case EPropertyType.Prop_Abs_DefPen:
                    // result = KBLanguage.GetText(KBLanguageID.ID_DefensePresentation);
                    break;
                case EPropertyType.Prop_Abs_Hit:
                    // result = KBLanguage.GetText(KBLanguageID.ID_Hit);
                    break;
                case EPropertyType.Prop_Abs_Dodge:
                    // result = KBLanguage.GetText(KBLanguageID.ID_Dodge);
                    break;
                case EPropertyType.Prop_Abs_Crit:
                    // result = KBLanguage.GetText(KBLanguageID.ID_Crit);
                    break;
                case EPropertyType.Prop_Abs_CritDmg:
                    // result = KBLanguage.GetText(KBLanguageID.ID_CritDamage);
                    break;
                case EPropertyType.Prop_Per_Mitigation:
                    // result = KBLanguage.GetText(KBLanguageID.ID_Mitigation);
                    break;
            }
            return result;
        }
    }
}