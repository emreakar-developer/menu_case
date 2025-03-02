using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button playButton;
    private Button settingsButton;
    private Button exitButton;

    private VisualElement settingsScreen;

    void OnEnable()
    {
        // UIDocument
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument bulunamadý!");
            return;
        }

        // Buttons
        playButton = uiDocument.rootVisualElement.Q<Button>("PlayButton");
        settingsButton = uiDocument.rootVisualElement.Q<Button>("SettingsButton");
        exitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");

        // Settings Screen
        settingsScreen = uiDocument.rootVisualElement.Q<VisualElement>("SettingsContainer");

        // Add Event to Button
        playButton.clicked += OnPlayButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        exitButton.clicked += OnExitButtonClicked;
    }

    private void OnDisable()
    {
        // Remove events from Buttons
        playButton.clicked -= OnPlayButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        exitButton.clicked -= OnExitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        // Game Starting
        Debug.Log("Play butonuna týklandý. Oyun baþlatýlacak.");

    }

    private void OnSettingsButtonClicked()
    {
        // Open Settings Screen
        settingsScreen.style.display = DisplayStyle.Flex; // Görünür yap
    }

    private void OnExitButtonClicked()
    {
        // Exit to Game
        Debug.Log("Exit butonuna týklandý. Oyun kapanacak.");
        Application.Quit();
    }
}
