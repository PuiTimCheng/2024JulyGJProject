using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAudioTrigger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickAudio();
        }
    }

    public void PlayClickAudio()
    {
        AudioManager.Instance.PlaySFX(SFXType.Click);
    }
}
