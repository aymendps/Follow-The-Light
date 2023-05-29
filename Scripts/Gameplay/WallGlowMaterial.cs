using Managers;
using UnityEngine;

namespace Gameplay
{
    public class WallGlowMaterial : MonoBehaviour
    {
        private bool _isGlowing;
        private MeshRenderer _meshRenderer;
        private static Material _glowingMaterial;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _glowingMaterial = LevelManager.Instance.WallGlowingMaterial;
        }

        /// <summary>
        /// Changes the material of the wall to the glowing material using the ball's color
        /// </summary>
        /// <param name="ballColor">Color of the ball when collision happened</param>
        public void StartGlowing(Color ballColor)
        {
            if (_isGlowing) return;

            _meshRenderer.material = _glowingMaterial;
            _meshRenderer.material.SetColor(EmissionColor, ballColor); 
            
            _isGlowing = true;
        }
    }
}
