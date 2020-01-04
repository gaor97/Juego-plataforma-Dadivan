using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttack : MonoBehaviour
{
    
    public PolygonCollider2D pc2d;

    // Start is called before the first frame update
    void Start()
    {
       
        pc2d = GetComponent<PolygonCollider2D>();
    }

    

}
