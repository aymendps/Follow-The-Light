using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Main_Menu
{
    public class MainMenuUI : MonoBehaviour
    {
        [Tooltip("The title of the game in the Canvas")]
        [SerializeField] private TextMeshProUGUI title;
        
        [Tooltip("The text elements to fade in and out in the Canvas")]
        [SerializeField] private List<TextMeshProUGUI> textToFade;
        
        [Tooltip("The buttons in the Canvas")]
        [SerializeField] private List<Button> buttons;
        
        [Tooltip("The speed at which the text fades in and out")]
        [SerializeField] private float textFadeSpeed;
        public IEnumerator ShowMenuUI()
        {            
            yield return new WaitForSeconds(1);
            FadeInAllText();
            yield return new WaitForSeconds(1);
            UnlockButtons();
        }
        
        public void HideMenuUI()
        {
            LockButtons();
            FadeOutAllText();
        }
        
        public void UpdateTitleColor(Color color)
        {
            title.color = new Color(color.r, color.g, color.b, title.color.a);
        }

        private void FadeOutAllText()
        {
            foreach (var text in textToFade)
            {
                Fading.FadeOutText(textFadeSpeed, text);
            }
        }

        private void FadeInAllText()
        {
            foreach (var text in textToFade)
            {
                Fading.FadeInText(textFadeSpeed, text);
            }
        }

        private void LockButtons()
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }

        private void UnlockButtons()
        {
            foreach (var button in buttons)
            {
                button.interactable = true;
            }
            buttons[0].Select();
        }
    }
}