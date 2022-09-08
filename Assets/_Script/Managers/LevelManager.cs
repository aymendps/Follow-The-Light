using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [HideInInspector] public List<WallGlowLogic> glowingRenderers;
    public BallController playerModel;
    public float loseLevelAtY;
    public bool hardMode;
    public Image hardModeIcon;
    public GameObject checkpoint;
    public GameObject deathParticleEffect;
    public GameObject respawnParticleEffect;
    public AudioClip deathQuote;
    public AudioClip deathSound;
    public Image deathPanel;
    public float fadeSpeed;

    TextMeshProUGUI deathPanelText;
    GameObject player;
    MeshRenderer playerMR;
    Rigidbody playerRB;
    Light playerL;
    TrailRenderer playerTR;
    bool levelLost;

    private void Awake()
    {
        instance = this;
        levelLost = false;
        hardMode = false;
    }
    private void Start()
    {
        player = playerModel.gameObject;
        playerMR = player.gameObject.GetComponent<MeshRenderer>();
        playerRB = player.GetComponent<Rigidbody>();
        playerL = player.GetComponent<Light>();
        playerTR = player.GetComponent<TrailRenderer>();
        deathPanelText = deathPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(!levelLost && playerModel.gameObject.transform.position.y < loseLevelAtY)
        {
            StartCoroutine(LevelLost());
        }
        if(hardModeIcon.enabled == true)
        {
            hardModeIcon.color = playerL.color;
        }
    }

    IEnumerator LevelLost()
    {
        levelLost = true;
        playerModel.playerControl = false;
        playerMR.enabled = false;
        playerRB.useGravity = false;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        playerRB.Sleep();
        playerL.enabled = false;
        playerTR.enabled = false;
        Instantiate(deathParticleEffect, player.transform.position, Quaternion.identity);
        SoundEffectManager.instance.PlaySoundEffect(deathSound);
        CMShakeCamera.instance.ShakeCamera();
        StartCoroutine(Fading.FadeInImage(fadeSpeed, deathPanel));
        yield return new WaitForSeconds(0.75f);
        deathPanelText.color = new Color(deathPanelText.color.r, deathPanelText.color.g, deathPanelText.color.b, 1.0f);
        SoundEffectManager.instance.PlaySoundEffect(deathQuote);
        yield return new WaitForSeconds(1.75f);
        StartCoroutine(ResetLevel()); 
    }
    IEnumerator ResetLevel()
    {
        ResetGlowingRenderers();
        playerModel.cameraTarget.transform.rotation = Quaternion.Euler(45, 0, 0);
        player.transform.position = checkpoint.transform.position;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        playerRB.Sleep();
        playerRB.useGravity = true;
        playerMR.enabled = true;
        playerL.enabled = true;
        playerTR.enabled = true;
        deathPanelText.color = new Color(deathPanelText.color.r, deathPanelText.color.g, deathPanelText.color.b, 0.0f);
        StartCoroutine(Fading.FadeOutImage(fadeSpeed, deathPanel));
        yield return new WaitForSeconds(0.4f);
        Instantiate(respawnParticleEffect, player.transform.position, Quaternion.Euler(90,0,0));
        playerModel.playerControl = true;
        levelLost = false;
    }

    void ResetGlowingRenderers()
    {
        if(hardMode)
        {
            foreach(WallGlowLogic m in glowingRenderers)
            {
                m.ResetMaterial();
            }
        }
    }
}
