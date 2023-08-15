using MoreMountains.Feedbacks;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth {
    [RequireComponent(typeof(MMF_Player))]
    public class GeneralTextFeedback : Singleton<GeneralTextFeedback>
    {
        private MMF_Player feedbackPlayer;
        private MMF_FloatingText floatingTextFeedback;

        protected override void Awake()
        {
            base.Awake();
            feedbackPlayer = GetComponent<MMF_Player>();
            floatingTextFeedback = feedbackPlayer.GetFeedbackOfType<MMF_FloatingText>();
        }

        public void MakeText(string text)
        {
            floatingTextFeedback.Value = text;
            feedbackPlayer.PlayFeedbacks();
        }
    }
}