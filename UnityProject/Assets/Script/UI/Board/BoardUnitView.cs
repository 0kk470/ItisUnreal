using Saltyfish.Logic;
using UnityEngine;

namespace Saltyfish.UI.Board
{
    public class BoardUnitView:UIDataContainer<Unit>
    {
        [SerializeField]
        private HealthBar m_HpBar;

        void Awake()
        {

        }

        void OnDestroy()
        {

        }
    }
}