using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAudioTrigger : MonoBehaviour
{
    void Update()
    {

    }

    public void PlayClickAudio()
    {
        AudioManager.Instance.PlaySFX(SFXType.Click);
    }
}
