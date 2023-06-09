///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 06/06/2022 10:49
///-----------------------------------------------------------------

using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Com.GabrielBernabeu.Common {
    public class TextFeedbackMaker : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textFeedbackPrefab = default;

        public static TextFeedbackMaker Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// </summary>
        /// <param name="text">Feedback's text</param>
        /// <param name="spawnInitColor">Color at the init of the spawn</param>
        /// <param name="spawnEndScale">Scale at the end of the spawn (the init scale is always 0)</param>
        /// <param name="spawnEndColor">Color at the end of the spawn</param>
        /// <param name="spawnDuration">Duration of the spawn</param>
        /// <param name="restDuration">Time between the spawn and the despawn</param>
        /// <param name="despawnEndScaleRatio">Ratio of the spawnEndScale at the end of the despawn</param>
        /// <param name="despawnEndColor">Color at the end of the despawn</param>
        /// <param name="despawnYShift">Y shifting done during the despawn</param>
        /// <param name="despawnDuration">Duration of the despawn</param>
        /// <param name="removeAlphaOnDespawn">Removes the alpha of the despawnEndColor</param>
        /// <param name="initPosition">Global initial position. If null, the position is TextFeedbackMaker's</param>
        /// <param name="aspectRatio">Controls the width & height of the TMP (when scale == 1). If null, 
        ///the scale will be the prefab's</param>
        public void CreateText(string text, Color spawnInitColor, float spawnEndScale, Color spawnEndColor, float spawnDuration, 
                               float restDuration, float despawnEndScaleRatio, Color despawnEndColor, 
                               float despawnYShift, float despawnDuration, bool removeAlphaOnDespawn = false, 
                               Vector2? initPosition = null, Vector2? aspectRatio = null)
        {
            if (removeAlphaOnDespawn)
                despawnEndColor.a = 0f;

            Vector3 lTrueInitPos;

            if (initPosition == null)
                lTrueInitPos = transform.position;
            else
            {
                lTrueInitPos = initPosition.Value;
                lTrueInitPos.z = transform.position.z;
            }


            TextMeshPro lTextFeedback = Instantiate(_textFeedbackPrefab, transform);

            if (aspectRatio != null)
                lTextFeedback.rectTransform.sizeDelta = aspectRatio.Value;

            Transform lFeedbackTransform = lTextFeedback.transform;
            lFeedbackTransform.position = lTrueInitPos;

            lTextFeedback.text = text;
            lTextFeedback.color = spawnInitColor;
            lFeedbackTransform.localScale = Vector3.zero;

            DOTween.Sequence(lFeedbackTransform)
                .Append(lFeedbackTransform.DOScale(spawnEndScale, spawnDuration))
                .Join(lTextFeedback.DOColor(spawnEndColor, spawnDuration))
                .AppendInterval(restDuration)
                .Append(lFeedbackTransform.DOScale(spawnEndScale * despawnEndScaleRatio, despawnDuration))
                .Join(lTextFeedback.DOColor(despawnEndColor, despawnDuration))
                .Join(lFeedbackTransform.DOBlendableLocalMoveBy(new Vector3(0f, despawnYShift, 0f), despawnDuration))
                .OnComplete(() => { Destroy(lFeedbackTransform.gameObject); });
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;

                for (int i = transform.childCount - 1; i >= 0; i--)
                    DOTween.Kill(transform.GetChild(i));
            }
        }
    }
}
