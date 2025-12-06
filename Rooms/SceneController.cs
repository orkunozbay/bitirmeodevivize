using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance; //her shanede tek scenecontroller 

    private void Awake() //scenecontroller yoksa bunu ayarla
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //sahne deðiþse de korunur
        }
        else
        {
            Destroy(gameObject); //scene controller varsa zaten yok et
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); //þuanki sahne indexi+1 = sonraki sahne
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName); //sahne geçiþleri
    }
}
