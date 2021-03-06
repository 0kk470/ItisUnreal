//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg
{

public sealed class UnitTableData :  Bright.Config.BeanBase 
{
    public UnitTableData(JSONNode _json) 
    {
        { if(!_json["Id"].IsNumber) { throw new SerializationException(); }  Id = _json["Id"]; }
        { if(!_json["Name"]["key"].IsString) { throw new SerializationException(); }  Name_l10n_key = _json["Name"]["key"]; if(!_json["Name"]["text"].IsString) { throw new SerializationException(); }  Name = _json["Name"]["text"]; }
        { if(!_json["Description"]["key"].IsString) { throw new SerializationException(); }  Description_l10n_key = _json["Description"]["key"]; if(!_json["Description"]["text"].IsString) { throw new SerializationException(); }  Description = _json["Description"]["text"]; }
        { if(!_json["BaseMaxHealth"].IsNumber) { throw new SerializationException(); }  BaseMaxHealth = _json["BaseMaxHealth"]; }
        { if(!_json["BaseAttack"].IsNumber) { throw new SerializationException(); }  BaseAttack = _json["BaseAttack"]; }
        { if(!_json["BaseAttackFreq"].IsNumber) { throw new SerializationException(); }  BaseAttackFreq = _json["BaseAttackFreq"]; }
        { if(!_json["IconPath"].IsString) { throw new SerializationException(); }  IconPath = _json["IconPath"]; }
    }

    public UnitTableData(int Id, string Name, string Description, float BaseMaxHealth, float BaseAttack, int BaseAttackFreq, string IconPath ) 
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.BaseMaxHealth = BaseMaxHealth;
        this.BaseAttack = BaseAttack;
        this.BaseAttackFreq = BaseAttackFreq;
        this.IconPath = IconPath;
    }

    public static UnitTableData DeserializeUnitTableData(JSONNode _json)
    {
        return new UnitTableData(_json);
    }

    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// ??????
    /// </summary>
    public string Name { get; private set; }
    public string Name_l10n_key { get; }
    /// <summary>
    /// ??????
    /// </summary>
    public string Description { get; private set; }
    public string Description_l10n_key { get; }
    /// <summary>
    /// ?????????????????????
    /// </summary>
    public float BaseMaxHealth { get; private set; }
    /// <summary>
    /// ???????????????
    /// </summary>
    public float BaseAttack { get; private set; }
    /// <summary>
    /// ????????????(x?????????????????????)
    /// </summary>
    public int BaseAttackFreq { get; private set; }
    /// <summary>
    /// ??????????????????
    /// </summary>
    public string IconPath { get; private set; }

    public const int __ID__ = 1137636180;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        Name = translator(Name_l10n_key, Name);
        Description = translator(Description_l10n_key, Description);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Description:" + Description + ","
        + "BaseMaxHealth:" + BaseMaxHealth + ","
        + "BaseAttack:" + BaseAttack + ","
        + "BaseAttackFreq:" + BaseAttackFreq + ","
        + "IconPath:" + IconPath + ","
        + "}";
    }
    }
}
