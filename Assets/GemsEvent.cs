using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsEvent : MonoBehaviour
{
    public bool gem;
    public float x, y;
    private BoxCollider2D bc2d;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gem = true;
            transform.position = new Vector3(x, y, 0);
            bc2d.isTrigger = false;
            rb2d.isKinematic = false;
        }
    }

}
