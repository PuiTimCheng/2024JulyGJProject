using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public enum BGMType
{
    Game,
    Intro,
    Credit,
    Menu
}

public enum SFXType
{
    Drag,
    Place,  
    Click,
    Ambience, // 写错了
    Digest,
    TimeUp, // TODO: 
    Merge,
    RockSword,
    Tea,
    Start,
    Receipt,
    Trash,
    R_Start,
    R_Print,
    R_End,
    GameStart
}

public class AudioManager : SerializedMonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _bgmSource1;
    [SerializeField] private AudioSource _bgmSource2;
    [SerializeField] AudioSource _sfxSource;
    [SerializeField] AudioSource _ambSource;

    [SerializeField] private float _fadeDuration = 5f;

    private AudioSource _currentSource;
    private AudioSource _nextSource;

    private Coroutine _bgmFadeCoroutine;
    private Coroutine _ambFadeCoroutine;

    public Dictionary<BGMType, AudioClip> _bgmDic;
    public Dictionary<SFXType, AudioClip> _sfxDic;

    public AudioClip _ambClip;

    bool _bgmFading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _currentSource = _bgmSource1;
        _nextSource = _bgmSource2;
    }

    public void PlayBGM(BGMType bgm)
    {
        if (_bgmDic.TryGetValue(bgm, out var clip))
        {
            PlayBGM(clip);
        }
    }

    private void PlayBGM(AudioClip clip)
    {
        if (_bgmFadeCoroutine != null)
        {
            StopCoroutine(_bgmFadeCoroutine);
            (_currentSource, _nextSource) = (_nextSource, _currentSource);
        }
        
        _bgmFadeCoroutine = StartCoroutine(FadeInOut(clip));
    }

    public void PauseBGM()
    {
        if (_currentSource.isPlaying)
        {
            _currentSource.Pause();
        }
    }

    public void ResumeBGM()
    {
        if (!_currentSource.isPlaying)
        {
            _currentSource.Play();
        }
    }
    

    public void PlaySFX(SFXType sfx)
    {
        if (_sfxDic.TryGetValue(sfx, out var clip))
        {
            _sfxSource.PlayOneShot(clip);
        }
    }

    private IEnumerator FadeInOut(AudioClip newClip)
    {
        float timer = 0f;

        float curInit = _currentSource.volume;

        _nextSource.clip = newClip;
        _nextSource.Play();
        _nextSource.time = 0;
        _nextSource.volume = 0;
        
        while (timer < _fadeDuration)
        {
            _currentSource.volume = Mathf.Lerp(curInit, 0f, timer / _fadeDuration);
            _nextSource.volume = Mathf.Lerp(0f, 1f, timer / _fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        _currentSource.volume = 0f;
        _nextSource.volume = 1f;
        
        _currentSource.Stop();

        // Swap current and next sources
        (_currentSource, _nextSource) = (_nextSource, _currentSource);

        _bgmFadeCoroutine = null;
    }

    // Fade In
    public void FadeInAmbience()
    {
        if (_ambFadeCoroutine != null)
        {
            StopCoroutine(_ambFadeCoroutine);
        }

        _ambFadeCoroutine = StartCoroutine(FadeInAmbienceCoroutine(_ambClip));
    }

    IEnumerator FadeInAmbienceCoroutine(AudioClip clip)
    {
        Debug.Log("Fade in Amb");

        _ambSource.clip = clip;
        _ambSource.Play();

        float timer = 0f;

        while (timer < _fadeDuration)
        {
            _ambSource.volume = Mathf.Lerp(0f, 1f, timer / _fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        _ambSource.volume = 1f;
    }

    IEnumerator FadeOutAmbienceCoroutine()
    {
        Debug.Log("Fade Out Amb");
        float timer = 0f;

        float initValue = _ambSource.volume;

        while (timer < _fadeDuration)
        {
            _ambSource.volume = Mathf.Lerp(initValue, 0f, timer / _fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        _ambSource.volume = 0f;
        _ambSource.Stop();
    }

    public void FadeOutAmbience()
    {
        if (!_ambSource.isPlaying)
        {
            return;
        }
        
        if (_ambFadeCoroutine != null)
        {
            StopCoroutine(_ambFadeCoroutine);
        }

        _ambFadeCoroutine = StartCoroutine(FadeOutAmbienceCoroutine());
    }
}