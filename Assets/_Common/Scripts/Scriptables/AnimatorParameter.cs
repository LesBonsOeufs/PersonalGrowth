///-----------------------------------------------------------------
/// Author : Gabriel Bernabeu
/// Date : 21/03/2022 14:22
///-----------------------------------------------------------------
using UnityEngine;

namespace Com.GabrielBernabeu.Common {
    [CreateAssetMenu(fileName = "AnimatorParameter", menuName = "Common/AnimatorParameter", order = 1)]
    public class AnimatorParameter : ScriptableObject
    {
        public int ID { get; private set; }

        private void OnEnable()
        {
            ID = Animator.StringToHash(name);
        }
		
		public static implicit operator int(AnimatorParameter from)
		{
			return from.ID;
		}
    }
}
