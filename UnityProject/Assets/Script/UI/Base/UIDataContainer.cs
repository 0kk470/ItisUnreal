using Saltyfish.Util;
using UnityEngine;

namespace Saltyfish.UI
{
    public class UIDataContainer<T>:MonoBehaviour,IDataContainer<T>
    {
        protected T m_Data;

        public virtual void SetData(T data)
        {
            m_Data = data;
        } 
    }
}