using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class VideoSettingsManager : MonoBehaviour
{
    private UIDocument ui;
    private Button cancel;
    private Button apply;

    private DropdownField displayResolution;
    private DropdownField quality;

    void OnEnable()
    {
        // Find UIDocument from Parent
        ui = GetComponentInParent<UIDocument>();
        if (ui == null)
        {
            Debug.LogError("UIDocument can not found! SettingsManager.");
            return;
        }
        cancel = ui.rootVisualElement.Q<Button>("Cancel");
        apply = ui.rootVisualElement.Q<Button>("Apply");

        cancel.clicked += OnCancel;
        apply.clicked += OnApply;

        // Initialize settings UI elements
        InitDisplayResolution();
        InitQualitySettings();
    }

    private void OnDisable()
    {
        // Unregister button callbacks to prevent memory leaks
        cancel.clicked -= OnCancel;
        apply.clicked -= OnApply;
    }

    // Cancel button action: Close settings panel
    void OnCancel()
    {
        gameObject.SetActive(false);
    }
    // Apply button action: Set resolution and quality settings
    void OnApply()
    {
        var resolution = Screen.resolutions[displayResolution.index];
        Screen.SetResolution(resolution.width, resolution.height, true);
        QualitySettings.SetQualityLevel(quality.index, true);
    }
    // Initialize resolution dropdown with available screen resolutions
    void InitDisplayResolution()
    {
        displayResolution = ui.rootVisualElement.Q<DropdownField>("DisplayResolution");
        displayResolution.choices = Screen.resolutions.Select(resolution => $"{resolution.width}x{resolution.height}").ToList();
        displayResolution.index = Screen.resolutions
            .Select((resolution, index) => (resolution, index))
            .First((value) => value.resolution.width == Screen.currentResolution.width && value.resolution.height == Screen.currentResolution.height)
            .index;
    }
    // Initialize quality settings dropdown with available quality levels
    void InitQualitySettings()
    {
        quality = ui.rootVisualElement.Q<DropdownField>("Quality");
        quality.choices = QualitySettings.names.ToList();
        quality.index = QualitySettings.GetQualityLevel();
    }
}
