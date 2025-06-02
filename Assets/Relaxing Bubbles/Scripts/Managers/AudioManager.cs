using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;     
    [SerializeField] private AudioSource _effectsSource;   

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _backgroundMusic;   
    [SerializeField] private AudioClip _deadSound;         
    [SerializeField] private AudioClip _itemCollectSound;  

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 0.75f;    
    [Range(0f, 1f)]
    [SerializeField] private float _effectsVolume = 0.75f; 

    private bool _musicEnabled = true;
    private bool _effectsEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        UpdateVolumes();
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void UpdateVolumes()
    {
        _musicSource.volume = _musicEnabled ? _musicVolume : 0f;
        _effectsSource.volume = _effectsEnabled ? _effectsVolume : 0f;
    }

    public void PlayBackgroundMusic()
    {
        if (_backgroundMusic != null && _musicEnabled)
        {
            _musicSource.clip = _backgroundMusic;
            _musicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (_musicSource != null && _musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
    }

    public void ToggleMusic(bool enabled)
    {
        _musicEnabled = enabled;
        
        if (_musicEnabled)
        {
            _musicSource.volume = _musicVolume;
            if (!_musicSource.isPlaying && _backgroundMusic != null)
            {
                _musicSource.Play();
            }
        }
        else
        {
            _musicSource.volume = 0f;
        }
    }

    public void ToggleEffects(bool enabled)
    {
        _effectsEnabled = enabled;
        _effectsSource.volume = _effectsEnabled ? _effectsVolume : 0f;
    }

    public void PlayItemCollectSound()
    {
        if (_effectsEnabled && _itemCollectSound != null)
        {
            _effectsSource.PlayOneShot(_itemCollectSound, _effectsVolume);
        }
    }

    public void PlayDeadSound()
    {
        if (_effectsEnabled && _deadSound != null)
        {
            _effectsSource.PlayOneShot(_deadSound, _effectsVolume);
        }
    }

    public bool IsMusicEnabled()
    {
        return _musicEnabled;
    }

    public bool IsEffectsEnabled()
    {
        return _effectsEnabled;
    }
} 