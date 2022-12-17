using UnityEngine;
using UnityEngine.UI;

public class MusicToggler : MonoBehaviour
{
    public Image musicIcon;
    public Sprite musicOn;
    public Sprite musicOff;
    private AudioSource musicSource;

    private void Start()
    {
        var music = GameObject.FindWithTag("MusicSource");
        if (music && music.TryGetComponent(out AudioSource musicSource))
        {
            this.musicSource = musicSource;
            musicIcon.sprite = musicSource.isPlaying ? musicOn : musicOff;
        }
    }

    public void ToggleMusic()
    {
        if (musicSource)
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
                musicIcon.sprite = musicOff;
            }
            else
            {
                musicSource.Play();
                musicIcon.sprite = musicOn;
            }
        }
        else
        {
            Debug.LogError("Music source not found");
        }
    }
}