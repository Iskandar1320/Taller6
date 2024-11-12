using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio_UI : MonoBehaviour
{

    public void PlaySoundUi()
    {
        AudioManager.Instance.ClickUi();
    }
    public void PlaySoundUiMaquina()
    {
        AudioManager.Instance.ClickMaquina();
    }
    public void PlaySoundPajaros()
    {
        AudioManager.Instance.ClickPajaros();
    }
    public void PlayGunShot()
    {
        AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.gunShot);
    }
}
