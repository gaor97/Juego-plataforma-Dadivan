using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMn : MonoBehaviour
{
    public int sceneNumb;
    public BoxCollider2D bc2d;
    private GemsEvent gems;
    // Start is called before the first frame update

    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        gems = new GemsEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cargarLvl(string nameLvl) {
        SceneManager.LoadScene(nameLvl);
    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            Invoke("Gema", 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           SceneManager.LoadScene(sceneNumb);
        }
    }

    void Gema()
    {
        bc2d.isTrigger = true;
    }

}
