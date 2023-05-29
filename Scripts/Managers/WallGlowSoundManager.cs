using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// This class is responsible for playing the sound effects for the wall glowing effect.
    /// </summary>
    public class WallGlowSoundManager : MonoBehaviour
    {
        public static WallGlowSoundManager Instance { get; private set; }
        [SerializeField] private List<AudioClip> wallSoundEffects;

        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this; }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Plays a random sound effect from the list of wall sound effects.
        /// </summary>
        public void PlayWallSoundEffect()
        {
            SoundEffectManager.Instance.PlaySoundEffect(wallSoundEffects[Random.Range(0, wallSoundEffects.Count)]);
        }

    }
}
