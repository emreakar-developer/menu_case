using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    //Key for playerpref
    private const string MasterVolumeKey = "MasterVolume";

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 0.5f); // Default: 1.0f

        // Apply to AudioMixer
        SetMasterVolume(savedVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);  
        PlayerPrefs.SetFloat(MasterVolumeKey, volume); // Save new value
        PlayerPrefs.Save();
    }
}
