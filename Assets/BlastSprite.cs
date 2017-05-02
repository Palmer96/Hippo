using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastSprite : MonoBehaviour
{

    public float radius;

    public float speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.localScale.x < radius-0.1f)
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(radius, radius, radius), speed);
        //else
        //{
        //}
            Color col = GetComponent<SpriteRenderer>().color;
            float alpha = col.a - 0.01f;
            GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, alpha);
            if (col.a <= 0.01f)
                Destroy(gameObject);
            
    }
}
