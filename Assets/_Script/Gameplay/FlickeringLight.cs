using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public float lightIntensity;
    public float flickerTimes;
    public float flickerDuration;
    public float giveTakeToDuration;
    public float timeBetweenFlickers;
    public AudioClip flickerOnSound;
    public AudioClip flickerOffSound;
    public AudioClip lightKilledSound;
    public AudioSource localAudioSrc;

    Light myLight;
    float defaultIntensity;
    float minFlickerDuration;
    float maxFlickerDuration;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        defaultIntensity = myLight.intensity;
        minFlickerDuration = flickerDuration - giveTakeToDuration;
        maxFlickerDuration = flickerDuration + giveTakeToDuration;

        StartCoroutine(FlickerLight());
    }

    void PlaySound(AudioClip clip)
    {
        if(localAudioSrc == null)
        {
            SoundEffectManager.instance.PlaySoundEffect(clip);
        }
        else
        {
            localAudioSrc.PlayOneShot(clip);
        }
    }

    IEnumerator FlickerLight()
    {
        for(int i=0; i<flickerTimes; i++)
        {
            myLight.intensity = lightIntensity;
            PlaySound(flickerOffSound);

            yield return new WaitForSeconds(Random.Range(minFlickerDuration,maxFlickerDuration));

            myLight.intensity = defaultIntensity;
            PlaySound(flickerOnSound);

            yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));
        }

        yield return new WaitForSeconds(timeBetweenFlickers);

        StartCoroutine(FlickerLight());
    }

    public IEnumerator KillLight()
    {
        for (int i = 0; i < flickerTimes-1; i++)
        {
            myLight.intensity = lightIntensity;
            PlaySound(flickerOffSound);

            yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));

            myLight.intensity = defaultIntensity;
            PlaySound(flickerOnSound);

            yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));
        }

        myLight.enabled = false;
        PlaySound(lightKilledSound);
        if (CMShakeCamera.instance != null) CMShakeCamera.instance.ShakeCamera();

    }
}
