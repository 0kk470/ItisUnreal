using UnityEngine;
using Saltyfish.Util;

namespace Saltyfish
{
    public class Main : MonoBehaviour
    {
        #region Mono Callback
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Init();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        void OnDestroy()
        {
            DeInit();
        }

        #endregion

        private void Init()
        {
            Data.TableConfig.Init();

            InitManagers();

            UI.UIManager.Instance.OpenUI(UI.UINameConfig.MainMenuUI);


            //System.Action<TMPro.TMP_FontAsset> OnLoaded = (fontAsset) => Debug.LogError(fontAsset);
            //Resource.AssetCache.Default.GetAssetAsync<TMPro.TMP_FontAsset>("Fonts/MicrosoftYaHeiGB SDF", OnLoaded);
        }

        void InitManagers()
        {
            var managers = GetComponentsInChildren<ManagerBase>();
            foreach(var mgr in managers)
            {
                mgr.Init();
            }
            GameManager.Init();
        }

        private void DeInit()
        {
            Data.TableConfig.DeInit();
        }
    }
}
