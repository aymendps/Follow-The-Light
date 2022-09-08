using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class SampleSceneUI : MonoBehaviour
{
    public GameObject playerModel;
    public Button startButton;
    public GameObject remainActive;
    TextMeshProUGUI startButtonText;
    Rigidbody playerRB;
    PlayerInput playerInput;
    Light playerLight;

    private void Awake()
    {
        startButtonText = startButton.GetComponentInChildren<TextMeshProUGUI>();
        playerRB = playerModel.GetComponent<Rigidbody>();
        playerInput = playerModel.GetComponent<PlayerInput>();
        playerLight = playerModel.GetComponent<Light>();
    }

    private void Update()
    {
        startButtonText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, playerLight.color);
    }

    public void StartGame()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        remainActive.SetActive(true);
        playerRB.useGravity = true;
        playerInput.SwitchCurrentActionMap("Player");
    }
}
