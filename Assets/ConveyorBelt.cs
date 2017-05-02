using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{


   public float speed = 15f;
    bool on = true;
    Vector2 offset = new Vector2(0f, 0f);



    void OnTriggerStay(Collider obj)
    {
        float beltVelocity = speed * Time.deltaTime;
        obj.gameObject.GetComponent<Rigidbody>().velocity = beltVelocity * transform.forward;
    }
    void OnCollisionStay(Collision obj)
    {
        float beltVelocity = speed * Time.deltaTime;
        obj.gameObject.GetComponent<Rigidbody>().velocity = beltVelocity * transform.forward;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        offset += new Vector2(0, 0.1f) * Time.deltaTime;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
    }
}
