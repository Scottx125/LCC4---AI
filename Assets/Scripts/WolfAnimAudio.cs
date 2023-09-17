using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimAudio : MonoBehaviour
{
    [SerializeField]
    AudioSource _wolfAudio;
    [SerializeField]
    AudioClip _wolfBite;

    private void Bite()
    {
        _wolfAudio.PlayOneShot(_wolfBite);
    }
}
