using UnityEngine;
using UnityEngine.Events;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] 
    private MoveRoboVac _moveRoboVac;

    [Header("Звуки")]
    [SerializeField]
    private AudioClip _roboSound;

    [Header("Громкость")]
    [SerializeField]
    private float _roboSoundVolume = 0.5f;
    
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_moveRoboVac == null)
            _moveRoboVac = GetComponent<MoveRoboVac>();

        PlayMotorSound();
    }
    private void PlayMotorSound()
    {
        if (_roboSound != null)
        {
            _audioSource.clip = _roboSound;
            _audioSource.loop = true;
            _audioSource.volume = _roboSoundVolume;
            _audioSource.Play();
        }
    }

    private void OnDisable()
    {
        if (_audioSource != null)
            _audioSource.Stop();
    }
}
