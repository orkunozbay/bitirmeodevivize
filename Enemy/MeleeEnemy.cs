using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] private int damage = 1;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance = 1f; //collider uzaklýðý
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer; //sadece player görünce attack için layer oluþtur

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; //her saniye +1 artýyor ki attackcd'den düþük olsun

        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0f;
            anim.SetTrigger("meleeAttack");
        }
    }

    private bool PlayerInSight()
    {
        if (boxCollider == null) 
            return false;

        Vector3 boxCenter = boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance; //collider merkezini/konumunu belirle

        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,  boxCollider.bounds.size.z); //collider büyüklüðü/geniþliði

        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.left, 0f, playerLayer); //0f rotation, vector2.left: sola bakýyor, player layer'ýnda olanlarý algýla

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>(); //collider'da player'a hit: health script
        }

        return hit.collider != null; //collider'da player varsa true döndür
    }

    // Animasyonda event damageplayer frame'i sýrasýnda hasarý verir
    private void DamagePlayer()
    {
        if (PlayerInSight() && playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void DrawGizmos() //colliderbox çizimi
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.red;

        Vector3 boxCenter =boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
