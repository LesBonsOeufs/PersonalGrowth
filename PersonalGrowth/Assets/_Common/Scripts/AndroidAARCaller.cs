using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.GabrielBernabeu.Common {
    public class AndroidAARCaller : Singleton<AndroidAARCaller>
    {
        private AndroidJavaClass unityClass;
        private AndroidJavaObject unityActivity;
        private AndroidJavaObject pluginInstance;

        private void Start()
        {
            Initialize("com.gabrielbernabeu.hcwforunity.Plugin");
        }

        private void Initialize(string pluginName)
        {
            unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            pluginInstance = new AndroidJavaObject(pluginName);

            if (pluginInstance == null )
            {
                Debug.Log("Plugin instance error");
            }
        }

        public void Call(string methodName, params object[] args)
        {
            Call<object>(methodName, args);
        }

        public void Call<T>(string methodName, params object[] args)
        {
            if (pluginInstance != null)
            {
                TextFeedbackMaker.Instance.CreateText("Plugin exists!", Color.black, 1f, Color.black, 1f, 1f, 1f, Color.black, 0f, 0.5f);
                pluginInstance.CallStatic(methodName, args);
            }
            else
                TextFeedbackMaker.Instance.CreateText("Plugin is null!", Color.black, 1f, Color.black, 1f, 1f, 1f, Color.black, 0f, 0.5f);
        }

        public void Test()
        {
            Call("getTodayStepsCount");
        }
    }
}
