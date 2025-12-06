using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc ile pause
        {
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    public void GameOver() //gameover ekraný fonk.
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    public void PauseGame(bool status)
    {
        //pause screen status çek
        pauseScreen.SetActive(status);

        if (status) //pause screen aktif ise durdur
            Time.timeScale = 0;
        else //deðilse devam
            Time.timeScale = 1;
    }
    public void SoundVolume() //sesleri 20þer arttýr
    {
        SoundManager.instance.ChangeSoundVolume(0.2f); 
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

}