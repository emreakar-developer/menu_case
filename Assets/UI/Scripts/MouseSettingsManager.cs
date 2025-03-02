using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MouseSettingsManager : MonoBehaviour
{
    private UIDocument ui;
    private Button cancel;
    private Button apply;

    private Slider sensitivitySlider;

    private float mouseSensitivity;

    private VisualElement settingsScreen;
    void OnEnable()
    {
        // Find UIDocument from Parent
        ui = GetComponentInParent<UIDocument>();
        if (ui == null)
        {
            Debug.LogError("UIDocument cannot be found!");
            return;
        }

        // Find buttons
        cancel = ui.rootVisualElement.Q<Button>("Cancel");
        apply = ui.rootVisualElement.Q<Button>("Apply");
        // Settings Screen
        settingsScreen = ui.rootVisualElement.Q<VisualElement>("SettingsContainer");

        // Register button callbacks
        cancel.clicked += OnCancel;
        apply.clicked += OnApply;

        // Find slider and set the initial value
        sensitivitySlider = ui.rootVisualElement.Q<Slider>("MouseSensitivity");
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f); // Get saved sensitivity or default to 1

        // **Load initial settings**
        InitMouseSettings();

        // Update mouse sensitivity when slider value changes
        sensitivitySlider.RegisterValueChangedCallback(evt => UpdateSensitivity(evt.newValue));
    }

    private void OnDisable()
    {
        cancel.clicked -= OnCancel;
        apply.clicked -= OnApply;
    }

    // OnCancel: Hide the settings or reset to previous values
    void OnCancel()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f); // This can be changed to resetting to original values
        sensitivitySlider.SetValueWithoutNotify(mouseSensitivity);

        // Open Settings Screen
        settingsScreen.style.display = DisplayStyle.None;
    }

    // OnApply: Save the sensitivity value and apply it
    void OnApply()
    {
        float newSensitivity = sensitivitySlider.value; 
        float oldSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        if (oldSensitivity != newSensitivity)
        {
            // Save the new sensitivity value
            PlayerPrefs.SetFloat("MouseSensitivity", newSensitivity);
            PlayerPrefs.Save();

            Debug.Log("Mouse sensitivity is saved; " + newSensitivity);

            mouseSensitivity = newSensitivity;


        }
    }

    // Update mouse sensitivity value based on the slider
    void UpdateSensitivity(float value)
    {
        Debug.Log("Mouse sensitivity is changed; " + value);
        mouseSensitivity = value;
    }


    private void InitMouseSettings()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        sensitivitySlider.SetValueWithoutNotify(mouseSensitivity);
        Debug.Log("Mouse sensitivity initialized: " + mouseSensitivity);
    }

}
