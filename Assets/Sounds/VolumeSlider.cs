using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType {
        MASTER,

        MUSIC,

        SFX
    }

    [Header("Typer")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;
    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        switch (volumeType) {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.Instance.masterVolume;
            break;

            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.Instance.musicVolume;

                break;

            case VolumeType.SFX:
                volumeSlider.value = AudioManager.Instance.sfxVolume;

                break;
            default:
                Debug.LogWarning("Volume Typer no supported: " + volumeType);
                break;
        }
    }
    public void OnsliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.Instance.masterVolume = volumeSlider.value;
                break;

            case VolumeType.MUSIC:
                AudioManager.Instance.musicVolume = volumeSlider.value;

                break;

            case VolumeType.SFX:
                AudioManager.Instance.sfxVolume = volumeSlider.value;

                break;

            default:
                Debug.LogWarning("Volume Typer no supported: " + volumeType);
                break;
        }
    }
}
