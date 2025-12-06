using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false; //çarpmadýysa aktif
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {   
        if (hit) return; //çarptýysa deaktif
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime; //çok uzun süre uçma
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); //
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("explode"); //fireball çarptýysa explode anim
        else
            gameObject.SetActive(false); //baþka bir þeye çarparsa deaktive
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}