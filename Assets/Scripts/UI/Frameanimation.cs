using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Util
{
    public class FrameAnimation : MonoBehaviour
    {
        public List<Sprite> frames; // 动画帧列表
        public float animationDuration; // 整个动画的持续时间
        public bool loop; // 是否循环播放
        public bool isStartPlay;
        public bool isDestroyMyself;
        public UnityEvent onAnimationEnd; // 动画结束后的事件

        private Image _spriteRenderer;
        private bool _isPlaying;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<Image>();
            
            if(isStartPlay) StartAnimation();
        }

        // 手动开始动画
        public void StartAnimation()
        {
            if (!_isPlaying)
            {
                StartCoroutine(PlayAnimation());
            }
        }

        // 手动停止动画
        public void StopAnimation()
        {
            StopAllCoroutines();
            _isPlaying = false;
        }

        private IEnumerator PlayAnimation()
        {
            _isPlaying = true;

            float frameDuration = animationDuration / frames.Count; // 计算每帧的持续时间

            do
            {
                foreach (var t in frames)
                {
                    _spriteRenderer.sprite = t;
                    yield return new WaitForSeconds(frameDuration);
                }
            }
            while (loop);

            _isPlaying = false;

            // 动画结束后触发事件
            onAnimationEnd.Invoke();

            if (isDestroyMyself)
            {
                Destroy(gameObject);
            }
        }
    }
}