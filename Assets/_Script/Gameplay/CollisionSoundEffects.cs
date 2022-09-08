using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundEffects : MonoBehaviour
{
    public static CollisionSoundEffects instance;
    public List<AudioClip> wallSoundEffects;
    static AudioSource audioSrc;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        instance = this;
    }

    public void PlayWallSoundEffect()
    {
        if(wallSoundEffects.Count!=0)
        audioSrc.PlayOneShot(wallSoundEffects[Random.Range(0, wallSoundEffects.Count)]);
    }

}
