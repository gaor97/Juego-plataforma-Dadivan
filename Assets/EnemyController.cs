using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float speed = 1f;
    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed);
        float limiteSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limiteSpeed, rb2d.velocity.y);

        if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f) {
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }

        if (speed < 0)
        {
            transform.localScale = new Vector3(4f, 4f, 4f);
        } 
        else if (speed > 0)
        {
            transform.localScale = new Vector3(-4f, 4f, 4f);
        }

        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.SendMessage("EnemyKnockBack", transform.position.x);
        }

        if (collider.gameObject.tag == "Sword")
        {
            anim.SetTrigger("Death");
            speed = 0;
            Invoke("destroyEnemy", 1f);
        }
    }

    void destroyEnemy() {
        Destroy(gameObject);
    }
}
