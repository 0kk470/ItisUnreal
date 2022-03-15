using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saltyfish.Event
{
    /// <summary>
    /// 这个枚举用于拓展已有的事件
    /// </summary>
    public enum GameEventType
    {

        OnGameOver,

        OnNodeUpdate,
        OnUnitHpChanged,
        OnUnitRecoverHp,
        OnUnitDamaged,
        OnUnitDead,
    }
}
