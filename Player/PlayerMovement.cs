    using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private void Awake()
    {
     
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //sağ, sol inputları al
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); //y sabit x*speed
      
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one; //sağa giderken scale
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1); //sola gidersen sola scale

        //animator parametreleri tanımla
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

      
        if (Input.GetKeyDown(KeyCode.Space)) //zıplama
            Jump();

    }

    private void Jump()
    {
            if (isGrounded()) //yere değmiyorsa zıplamayı engelle
                body.velocity = new Vector2(body.velocity.x, jumpPower);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); //karakter hitbox ground layer ile temas ediyorsa grounded
        return raycastHit.collider != null; 
    }

    public bool canAttack()
    {
        return Mathf.Abs(horizontalInput) < 0.01f && isGrounded(); //hareket etmiyorken ve yerdeyken saldırır
    }
}