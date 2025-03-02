using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

public class LanguageManager : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button englishButton;
    private Button turkishButton;

    void OnEnable()
    {
        uiDocument = GetComponentInParent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("can not found UIDocument!");
            return;
        }

        //Find Butons
        englishButton = uiDocument.rootVisualElement.Q<Button>("EnglishButton");
        turkishButton = uiDocument.rootVisualElement.Q<Button>("TurkishButton");

        if (englishButton != null)
            englishButton.clicked += () => ChangeLanguage(0); // Translate to English

        if (turkishButton != null)
            turkishButton.clicked += () => ChangeLanguage(1); // Translate to Turkish
    }

    void ChangeLanguage(int localeIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
    }


}
