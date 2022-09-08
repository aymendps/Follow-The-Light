using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    //public List<AudioClip> audioClips;
    //List<string> clipNames;
    //Dictionary<string, AudioClip> dictionaryClips;
    AudioSource audioSrc;

    void Awake()
    {
        instance = this;
        //clipNames = audioClips.ConvertAll((AudioClip i) => { return i.ToString(); });
        //dictionaryClips = clipNames.Zip(audioClips, (key, value) => new { key, value }).ToDictionary(d => d.key, d => d.value);
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
        //AudioClip a = dictionaryClips[clipName];
        //audioSrc.PlayOneShot(a);        
    }

}
