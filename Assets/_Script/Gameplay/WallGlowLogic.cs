using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGlowLogic : MonoBehaviour
{
    MeshRenderer meshRenderer;
    bool isGlowing = false;
    Material defaultMaterial;
    private void Awake()
    {

        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    private void Reset()
    {
        gameObject.tag = "Should Glow";
    }

    public void ChangeMaterial(Light light, Material desiredMaterial)
    {
        if (isGlowing) return;

        meshRenderer.material = desiredMaterial;
        meshRenderer.material.SetColor("_EmissionColor", light.color);
        CollisionSoundEffects.instance.PlayWallSoundEffect();
        if (LevelManager.instance != null) LevelManager.instance.glowingRenderers.Add(this);
        isGlowing = true;
    }

    public void ResetMaterial()
    {
        meshRenderer.material = defaultMaterial;
        isGlowing = false;
    }


}
