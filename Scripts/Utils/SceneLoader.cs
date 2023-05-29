using System.Collections;
using UnityEngine.SceneManagement;

namespace Utils
{
    /// <summary>
    /// A class that contains methods for loading scenes.
    /// </summary>
    public static class SceneLoader
    {
        /// <summary>
        /// Loads a scene by name.
        /// </summary>
        /// <param name="sceneName">Name of the scene</param>
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Loads a scene by build index.
        /// </summary>
        /// <param name="sceneBuildIndex">Build index of the scene</param>
        public static void LoadScene(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }

        /// <summary>
        /// Loads a scene asynchronously by name.
        /// </summary>
        /// <param name="sceneName">Name of the scene</param>
        public static IEnumerator LoadSceneAsync(string sceneName)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        
        /// <summary>
        /// Loads a scene asynchronously by build index.
        /// </summary>
        /// <param name="sceneBuildIndex">Build index of the scene</param>
        public static IEnumerator LoadSceneAsync(int sceneBuildIndex)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }

}