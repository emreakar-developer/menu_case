using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsManager : MonoBehaviour
{
    private UIDocument ui;

    private Button audioButton, videoButton, mouseButton,cancel;
    private VisualElement audioSettings, videoSettings, mouseSettings,settingsScreen;
    private List<VisualElement> settingsContainers = new List<VisualElement>();

    void OnEnable()
    {
        ui = GetComponentInParent<UIDocument>();
        if (ui == null)
        {
            Debug.LogError("❌ UIDocument bulunamadı!");
            return;
        }
        // Get the Cancel button
        cancel = ui.rootVisualElement.Q<Button>("Cancel");

        // Get category buttons
        audioButton = ui.rootVisualElement.Q<Button>("AudioButton");
        videoButton = ui.rootVisualElement.Q<Button>("VideoButton");
        mouseButton = ui.rootVisualElement.Q<Button>("MouseButton");

        // Get settings containers
        audioSettings = ui.rootVisualElement.Q<VisualElement>("AudioSettings");
        videoSettings = ui.rootVisualElement.Q<VisualElement>("VideoSettings");
        mouseSettings = ui.rootVisualElement.Q<VisualElement>("MouseSettings");

        // Add to the list
        settingsContainers.Add(audioSettings);
        settingsContainers.Add(videoSettings);
        settingsContainers.Add(mouseSettings);

        // Add event listeners to buttons
        audioButton.clicked += () => ShowSettings(audioSettings);
        videoButton.clicked += () => ShowSettings(videoSettings);
        mouseButton.clicked += () => ShowSettings(mouseSettings);

        // Get Settings Screen (Main container)
        settingsScreen = ui.rootVisualElement.Q<VisualElement>("SettingsContainer");

        // Add event listener to Cancel button
        cancel.clicked += OnCancel;
        // Show one setting by default
        audioButton.Focus();
        ShowSettings(audioSettings);
    }

    private void ShowSettings(VisualElement selectedSettings)
    {
        foreach (var container in settingsContainers)
        {
            container.style.display = DisplayStyle.None;
        }

        selectedSettings.style.display = DisplayStyle.Flex;
    }
    private void OnCancel()
    {
        settingsScreen.style.display = DisplayStyle.None;
    }
}
