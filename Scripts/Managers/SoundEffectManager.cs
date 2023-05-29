using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages the playing of sound effects in the game
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class SoundEffectManager : MonoBehaviour
    {
        public static SoundEffectManager Instance { get; private set; }
    
        private AudioSource _audioSrc;

        private void Awake()
        {
            // Singleton pattern with don't destroy on load
            if (Instance == null)
            {
                _audioSrc = GetComponent<AudioSource>();
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Plays a sound effect once
        /// </summary>
        /// <param name="clip">The audio clip to play once</param>
        public void PlaySoundEffect(AudioClip clip)
        {
            _audioSrc.PlayOneShot(clip);
        }

    }
}
