///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 15/09/2022 09:14
///-----------------------------------------------------------------

using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Com.GabrielBernabeu.Common 
{
    public static class TMPCountUp
    {
        /// <summary>
        /// Use in a coroutine!
        /// </summary>
        /// <param name="tmp"></param>
        /// <param name="duration"></param>
        /// <param name="initValue"></param>
        /// <param name="endValue"></param>
        /// <param name="prefix">String to put before the counting</param>
        /// <param name="ease">The counting's easing</param>
        /// <returns></returns>
        public static IEnumerator CountToTarget(TextMeshProUGUI tmp, float duration, int initValue, int endValue, string prefix = "", 
                                          Ease ease = Ease.OutCubic)
        {
            float lValue;
            float lRatio;
            float lCounter = 0f;

            while (lCounter < duration)
            {
                lRatio = DOVirtual.EasedValue(0f, 1f, lCounter / duration, ease);
                lValue = Mathf.FloorToInt(initValue + (endValue - initValue) * lRatio);
                tmp.text = prefix + lValue;

                yield return null;
                lCounter += Time.unscaledDeltaTime;
            }

            lValue = endValue;
            tmp.text = prefix + lValue;
        }
    }
}