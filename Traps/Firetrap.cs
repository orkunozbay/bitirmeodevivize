using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered; 
    private bool active; 

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
                StartCoroutine(ActivateFiretrap()); //geri sayým baþlat activate için

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage); //aktifse hasar ver
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        //geri sayým rengi
        triggered = true;
        spriteRend.color = Color.red;

        
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; //aktive olduktan sonra orijinal renge dönüþtür
        active = true;
        anim.SetBool("activated", true);

        //x saniye aktif kal ve default hale dönüþtür
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}