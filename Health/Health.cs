using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth = 100f;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")] //invincibility frames
    [SerializeField] private float iFramesDuration = 0.5f; //toplam dokunulmazlýk süresi
    [SerializeField] private int numberOfFlashes = 3; //yanýp sönme sayýsý
    private SpriteRenderer spriteRend; //sprite düzenleme için - kýrmýzý
    private bool invulnerable; //o sýrada invulnerable

    [Header("Sounds")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable || dead) return; //dokunulmaz veya öldüyse geç

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Hasar aldý ama ölmedi
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());  
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            // Öldü
            if (!dead)
            {
                dead = true;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealth(float value) //pickuplar için
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    private IEnumerator Invulnerability() 
    {
        invulnerable = true;

        // hasar almaz olduðu sürece player ile enemy layer çarpýþamaz
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    // ölen düþmaný sahneden kaldýrmak için 
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // respawn
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);

        anim.ResetTrigger("die");
        anim.Play("Idle");

      
    }
}
