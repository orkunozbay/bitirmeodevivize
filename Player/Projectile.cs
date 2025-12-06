using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return; //mermi hit kontrolü
        float movementSpeed = speed * Time.deltaTime * direction; //hareket etmeyi durdur
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime; 
        if (lifetime > 3) gameObject.SetActive(false); //3 sn bi þeye çarpmazsa durdur - range
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        hit = true; 
        boxCollider.enabled = false;
        anim.SetTrigger("explode"); //hit olduðunda collider kapat ve animasyon oynat

        if (collision.tag == "Enemy") //çarptýðý layer Enemy ise health scriptten takedamage çaðýr 1
            collision.GetComponent<Health>()?.TakeDamage(1);
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0; //lifetime 0lanýr
        direction = _direction; //yön
        gameObject.SetActive(true); //aktif et
        hit = false; 
        boxCollider.enabled = true;


        //mermi fýrlatýldýðý yöne doðru döndür
        float localScaleX = transform.localScale.x; //o anki x konumunu al (sað veya sol)
        if (Mathf.Sign(localScaleX) != _direction) //eðer o anki ile direciton farklý ise eþitle
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); //mermiyi o yöne çevir
    }
    private void Deactivate()
    {
        gameObject.SetActive(false); //mermi bittiðinde havuza geri döner deaktif edilir
    }
}