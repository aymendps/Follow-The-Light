using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    /// <summary>
    /// Contains methods to fade in/out text and images
    /// </summary>
    public static class Fading {
        /// <summary>
        /// Fades in the given text
        /// </summary>
        /// <param name="speed">The tween's duration</param>
        /// <param name="text">The text element to use</param>
        public static void FadeInText(float speed, TextMeshProUGUI text)
        {
            DOTween.To(() => text.color, x => text.color = x, 
                    new Color(text.color.r, text.color.g, text.color.b, 1.0f), speed)
                .SetOptions(true);
        }
        
        /// <summary>
        /// Fades out the given text
        /// </summary>
        /// <param name="speed">The tween's duration</param>
        /// <param name="text">The text element to use</param>
        public static void FadeOutText(float speed, TextMeshProUGUI text)
        {
            DOTween.To(() => text.color, x => text.color = x,
                    new Color(text.color.r, text.color.g, text.color.b, 0.0f), speed)
                .SetOptions(true);
        }
        
        /// <summary>
        /// Fades in the given image
        /// </summary>
        /// <param name="speed">The tween's duration</param>
        /// <param name="image">The image element to use</param>
        public static void FadeInImage(float speed, Image image)
        {
            image.DOFade(1.0f, speed);
        }
        
        /// <summary>
        /// Fades out the given image
        /// </summary>
        /// <param name="speed">The tween's duration</param>
        /// <param name="image">The image element to use</param>
        public static void FadeOutImage(float speed, Image image)
        {
            image.DOFade(0.0f, speed);
        }
    }
}