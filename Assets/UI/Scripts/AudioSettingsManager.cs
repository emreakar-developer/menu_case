using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioSettingsManager : MonoBehaviour
{
    private UIDocument ui;
    private Button cancel;
    private Button apply;

    private Slider masterVolumeSlider;

    [SerializeField]
    private AudioMixer audioMixer;

    private float savedMasterVolume;
    //Playerpref Key for MasterVolume
    private const string MasterVolumeKey = "MasterVolume";

    void OnEnable()
    {
        // Find UIDocument from Parent
        ui = GetComponentInParent<UIDocument>();
        if (ui == null)
        {
            Debug.LogError("UIDocument can not be found! AudioSettingsManager.");
            return;
        }

        // Get UI elements
        cancel = ui.rootVisualElement.Q<Button>("Cancel");
        apply = ui.rootVisualElement.Q<Button>("Apply");
        masterVolumeSlider = ui.rootVisualElement.Q<Slider>("MasterVolumeSlider");

        //I Added Event to Buttons
        cancel.clicked += OnCancel;
        apply.clicked += OnApply;

        // Instantly apply sound as slider changes
        masterVolumeSlider.RegisterValueChangedCallback(evt => SetMasterVolume(evt.newValue));

        // Initialize UI values
        InitAudioSettings();
    }

    private void OnDisable()
    {
        cancel.clicked -= OnCancel;
        apply.clicked -= OnApply;
    }

    private void OnCancel()
    {
        // Revert to saved settings
        SetMasterVolume(savedMasterVolume);
        masterVolumeSlider.value = savedMasterVolume;
        gameObject.SetActive(false);
    }

    private void OnApply()
    {
        // Save current settings
        savedMasterVolume = masterVolumeSlider.value;

        // Apply changes
        PlayerPrefs.SetFloat(MasterVolumeKey, savedMasterVolume);
        PlayerPrefs.Save();
        SetMasterVolume(masterVolumeSlider.value);

        gameObject.SetActive(false);
    }

    private void SetMasterVolume(float value)
    {
        // Apply the volume setting to the AudioMixer
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);  // Convert to decibels
    }

 

    private void InitAudioSettings()
    {

        savedMasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1.0f); 

        // Initialize UI with current values
        masterVolumeSlider.value = savedMasterVolume;

        // Apply initial settings
        SetMasterVolume(savedMasterVolume);
    }
}
