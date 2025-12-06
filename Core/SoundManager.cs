using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance { get; private set; }

    private AudioSource soundSource; // Efekt sesleri 
    private AudioSource musicSource; // Arka plan müziði

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>(); //ses efektleri
        musicSource = transform.GetChild(0).GetComponent<AudioSource>(); //sound manager ilk child al yani musicsource

           //level deðiþiminde ses kaynaðý devam etmesi
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this) //zaten soundmanager varsa 2. yi sil
        {
            Destroy(gameObject);
        }
    }

    // Kýsa efekt sesleri
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            soundSource.PlayOneShot(clip); 
    }

       //pause menü sound butonu
    public void ChangeSoundVolume(float change)
    {
        float currentVolume = PlayerPrefs.GetFloat("soundVolume", 1f); //menüde görüntü için

        currentVolume += change;

        // max volume aþarsa tekrar 0'a döndür
        if (currentVolume > 1f)
            currentVolume = 0f;
        else if (currentVolume < 0f)
            currentVolume = 1f;

        soundSource.volume = currentVolume;
        PlayerPrefs.SetFloat("soundVolume", currentVolume);
    }

    // pause menü volume butonu
    public void ChangeMusicVolume(float change)
    {
        float currentVolume = PlayerPrefs.GetFloat("musicVolume", 1f);

        currentVolume += change;

        if (currentVolume > 1f)
            currentVolume = 0f;
        else if (currentVolume < 0f)
            currentVolume = 1f;

      
        musicSource.volume = currentVolume * 0.3f;
        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }
}