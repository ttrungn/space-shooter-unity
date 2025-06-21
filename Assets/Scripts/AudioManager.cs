using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip shoot;

    public Text musicToggleButtonText;

    private bool sfxOn = true;

    void Start()
    {
        musicToggleButtonText.text = "Music On";
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxOn)
            sfxSource.PlayOneShot(clip);
    }

    public void OnOffBackground()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            sfxOn = !sfxOn;
            if (musicToggleButtonText != null)
                musicToggleButtonText.text = "Music Off";
        }
        else
        {
            musicSource.Play();
            sfxOn = !sfxOn;
            if (musicToggleButtonText != null)
                musicToggleButtonText.text = "Music On";
        }
    }
}