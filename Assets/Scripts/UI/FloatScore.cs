using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class FloatScore : MonoBehaviour
    {
        public TMP_Text text;
        public bool Available { get; private set; } = true;

        public Animator _animator;
        
        public void PlayScore(Vector3 position, int score, bool add)
        {
            transform.position = position;
            Available = false;
            text.text = add ? $"+{score}" :  $"-{score}" ;
            //play score animation here
            _animator.Play(add ? "FloatScore_Add" : "FloatScore_Minues");
            StartCoroutine(PlayAnimation());
        }

        IEnumerator PlayAnimation()
        {
            yield return new WaitForSeconds(0.4f);
            Available = true;
        }
    }
}