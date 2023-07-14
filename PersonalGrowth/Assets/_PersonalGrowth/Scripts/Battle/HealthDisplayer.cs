using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.Battle {
    public class HealthDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthTmp = default;

        private Vector3 initLocalPosition;

        private void Awake()
        {
            initLocalPosition = transform.localPosition;
        }

        private void Update()
        {
            transform.position = transform.parent.position + initLocalPosition;
            Vector3 lToCamera = Camera.main.transform.position - transform.position;
            lToCamera = Vector3.ProjectOnPlane(lToCamera, Vector3.right);
            transform.rotation = Quaternion.LookRotation(-lToCamera);
        }

        public void SetHealth(int value)
        {
            healthTmp.text = value.ToString();
        }
    }
}
