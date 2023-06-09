///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 06/10/2022 16:34
///-----------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu(menuName = "OrbitalDance/MusicInfo")]
public class MusicInfo : ScriptableObject 
{
    [SerializeField] private AudioClip _clip = default;
    [SerializeField] private float _bpm = default;

    public AudioClip Clip => _clip;
    public float BPM => _bpm;
}