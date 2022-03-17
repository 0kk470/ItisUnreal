using UnityEngine;
using UnityEngine.UI;

namespace Saltyfish.UI
{
    public class HealthBar:MonoBehaviour
    {
        [SerializeField]
        private Slider m_FillSlider;

        [SerializeField]
        private Text m_HealthTxt;

        public void SetHealthValue(float hp, float maxHp)
        {
            m_FillSlider.value = Mathf.Clamp01(hp / maxHp);
            m_HealthTxt.text = string.Format("{0}/{1}", Mathf.FloorToInt(hp), Mathf.FloorToInt(maxHp));
        }
    }
}