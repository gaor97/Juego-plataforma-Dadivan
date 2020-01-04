using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public float jumpPowerHurt = 1.5f;
    public bool attack = false;


    private Collider2D coll;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;
    private CheckAttack attackCh;
    private bool jump;
    private bool doubleJump;
    private bool movement = true;
    private int life = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        attackCh = GetComponentInChildren<CheckAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        if (grounded) {
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (grounded)
            {
                jump = true;
                doubleJump = true;
            } else if (doubleJump) {
                jump = true;
                doubleJump = false;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Space)){ 
            anim.SetTrigger("Attacking");
            Invoke("AttackMetod", 0.22f);
            Invoke("AttackOut", 0.26f);
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.90f;

        if (grounded) {
            rb2d.velocity = fixedVelocity;
        }

        float h = Input.GetAxis("Horizontal");
        if (!movement) h = 0;


        rb2d.AddForce(Vector2.right * speed * h);

        float limiteSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limiteSpeed, rb2d.velocity.y);

        if (h > 0.1f) {
            transform.localScale = new Vector3(4f, 4f, 4f);
        }

        if (h < -0.1f) {
            transform.localScale = new Vector3(-4f, 4f, 4f);
        }

        if (jump) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        Debug.Log(rb2d.velocity.x);
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(0, 0, 0);
        life = 100;
    }

    public void EnemyKnockBack(float enemyPosX)
    {
        life = life - 50;
        jump = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPowerHurt, ForceMode2D.Impulse);
        anim.SetTrigger("Hurt");

        movement = false;
        spr.color = Color.red;

        Invoke("EnableMovement", 0.7f);


        if (life == 0)
        {
            anim.SetTrigger("Death");
            coll.enabled = !coll.enabled;

            Invoke("Respawn", 0.9f);
            life = 100;
        }
    }

    void EnableMovement() {
        movement = true;
        spr.color = Color.white;
    }

    void Respawn() {
        transform.position = new Vector3(0, 0, 0);
        coll.enabled = true;
    }

    void AttackMetod()
    {
        attackCh.pc2d.enabled = true;
    }

    void AttackOut()
    {
        attackCh.pc2d.enabled = false;
    }
}
