///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 25/06/2022 22:16
///-----------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Com.GabrielBernabeu.Common {
    public class ARManager : MonoBehaviour
    {
        [SerializeField] private int baseSceneIndex = 0;
        [SerializeField] private int arSceneIndex = 1;

        public static ARManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void CheckARSupport(UnityAction onNotSupported = null, UnityAction onSupported = null,
                                   UnityAction onInstallationFailed = null)
        {
            #if UNITY_EDITOR
                onSupported?.Invoke();
#elif UNITY_ANDROID
                StartCoroutine(CheckARSupportCoroutine(onNotSupported, onSupported, onInstallationFailed));
#endif
        }

        private IEnumerator CheckARSupportCoroutine(UnityAction onNotSupported = null, UnityAction onSupported = null, 
                                                    UnityAction onInstallationFailed = null)
        {
            Debug.Log("Checking for AR support...");
            yield return ARSession.CheckAvailability();

            if (ARSession.state == ARSessionState.NeedsInstall)
            {
                Debug.Log("Device supports AR but needs install.");
                Debug.Log("Attempting installation...");
                yield return ARSession.Install();
            }

            if (ARSession.state == ARSessionState.Ready)
            {
                Debug.Log("Device supports AR!");
                onSupported?.Invoke();
            }
            else
            {
                switch (ARSession.state)
                {
                    case ARSessionState.Unsupported:
                        Debug.Log("Your device does not support AR.");
                        onNotSupported?.Invoke();
                        break;
                    case ARSessionState.NeedsInstall:
                        Debug.Log("Installation failed.");
                        onInstallationFailed?.Invoke();
                        break;
                }
            }
        }

        public void OpenAR()
        {
            SceneManager.LoadScene(arSceneIndex);
        }

        public void CloseAR()
        {
            SceneManager.LoadScene(baseSceneIndex);
        }

        private void OnDestroy()
        {
            if (Instance != this)
                return;

            Instance = null;
        }
    }
}
