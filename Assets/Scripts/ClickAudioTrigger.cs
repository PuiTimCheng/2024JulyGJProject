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
        Debug.Log("Play Click");
        AudioManager.Instance.PlaySFX(SFXType.Click);
    }
}
