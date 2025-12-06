using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void RespawnCheck()
    {
        if (currentCheckpoint == null) //chekcpoint gelmediyse gameover
        {
            uiManager.GameOver();
            return;
        }

        playerHealth.Respawn(); //healt scripti respawn
        transform.position = currentCheckpoint.position; //player checkpointe ýþýnla

        //camerayý checkpointe ýþýnla
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint") //checkpoint dokunma - dokunduðun obje adý chekpoint mi
        {
            currentCheckpoint = collision.transform; //yeri kaydet
            SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false; //tekrar dokununca hata olmamasý için deaktive et
            collision.GetComponent<Animator>().SetTrigger("activate"); //animasyon oynat
        }
    }
}