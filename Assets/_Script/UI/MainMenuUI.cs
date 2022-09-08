using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    public ParticleSystem rain;
    public TextMeshProUGUI title;
    public List<TextMeshProUGUI> textToFade;
    public float fadeSpeed;
    public List<Button> buttons;
    public Button firstSelectedButton;
    public FlickeringLight flickeringLight;
    public List<Light> normalLights;
    public GameObject shortLamp;
    public Animator CMAnimator;
    public AudioSource staticNeonSound;
    public AudioSource rainSound;
    public BallFakeMainMenu playerModel;
    public float playerModelStartSpeed;
    public GameObject loadingSwirl;
    Rigidbody playerModelRB;
    Light playerModelLight;
    float playerMaximumSpeed;

    private void Start()
    {
        playerModelRB = playerModel.rb;
        playerModelLight = playerModel.gl;
        playerMaximumSpeed = playerModel.maximumSpeed;

        CMAnimator.SetInteger("State Index", 1);
        StartCoroutine(TransitionToMenuSequence());
    }

    private void Update()
    {
        AnimateMenuText();
    }

    public void AnimateMenuText()
    {
        title.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, playerModelLight.color);

    }

    public void FadeOutAllText()
    {
        foreach(TextMeshProUGUI t in textToFade)
        {
            StartCoroutine(Fading.FadeOutText(fadeSpeed, t));
        }
    }

    public void FadeInAllText()
    {
        foreach (TextMeshProUGUI t in textToFade)
        {
            StartCoroutine(Fading.FadeInText(fadeSpeed, t));
        }
    }

    public void LockButtons()
    {
        foreach(Button b in buttons)
        {
            b.interactable = false;
        }
    }

    public void UnlockButtons()
    {
        foreach(Button b in buttons)
        {
            b.interactable = true;
        }
    }

    IEnumerator TransitionToMenuSequence()
    {
        yield return new WaitForSeconds(1);
        FadeInAllText();
        yield return new WaitForSeconds(1);
        UnlockButtons();
        firstSelectedButton.Select();
    }

    IEnumerator TransitionToSceneSequence()
    {
        LockButtons();
        FadeOutAllText();

        yield return new WaitForSeconds(0.5f);

        flickeringLight.StopAllCoroutines();

        yield return null;

        StartCoroutine(flickeringLight.KillLight());
        DOTween.To(() => playerModelLight.range, x => playerModelLight.range = x, 0.5f, 4);
        //DOTween.To(() => emission , x => emission = x, 0f, 4);

        yield return new WaitForSeconds(1.75f);

        SoundEffectManager.instance.PlaySoundEffect(flickeringLight.lightKilledSound);
        foreach(Light l in normalLights)
        {
            l.enabled = false;
        }

        var emission = rain.emission;
        emission.rateOverTime = 0f;
        rainSound.Stop();
        staticNeonSound.Stop();
        CMShakeCamera.instance.ShakeCamera();

        DOTween.To(() => playerModelLight.intensity, x => playerModelLight.intensity = x, 2.5f, 1);

        yield return new WaitForSeconds(1.2f);

        shortLamp.SetActive(false);
        playerModel.maximumSpeed = playerModelStartSpeed;
        playerModelRB.useGravity = true;

        yield return new WaitForSeconds(1.5f);

        CMAnimator.SetInteger("State Index", 2);

        yield return new WaitForSeconds(0.5f);

        DOTween.To(() => playerModel.maximumSpeed, x => playerModel.maximumSpeed = x, playerMaximumSpeed, 0.5f);
        loadingSwirl.SetActive(true);

        yield return new WaitForSeconds(2f);

        MySceneManager.Instance.LoadNextScene();

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Applicaiton.Quit()");
    }

    public void PlayButton()
    {
        StartCoroutine(TransitionToSceneSequence());
    }
}
