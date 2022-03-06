using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    private Vector3 dir;
    public float speed;
          

    void Start()
    {          
        dir = new Vector3(0,1,0);
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime; 
        speed *= 0.85f;
        StartCoroutine(Move());     
    }

    IEnumerator Move()
    {        
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);                    
    }
    
    
}
