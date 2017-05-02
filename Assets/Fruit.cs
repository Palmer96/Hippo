using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    Transform Player;
    bool taken = false;


    public float waterLevel = 4;
    public float floatHeight = 2;
    public float bounceDamp = 0.05f;
    public Vector3 buoyancyCenterOffset;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 upLift;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (taken)
        {
            transform.position = Vector3.Lerp(transform.position, Player.position, 0.1f);
        }
        else
        {

        }
    }

    public void Collect(Transform trans)
    {
        if (!taken)
        {
            taken = true;
            Player = trans;
            rb.isKinematic = true;
            //   GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (taken)
        {
            if (col.transform.CompareTag("Player"))
            {
                col.transform.GetComponent<Player>().score++;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("Water"))
        {
            Ray ray = new Ray(transform.position, -Vector3.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.CompareTag("Water"))
                    // if (transform.position.y > hit.point.y)
                    waterLevel = hit.point.y;
            }
            if (!taken)
            {

                actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
                forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

                if (forceFactor > 0)
                {
                    upLift = -Physics.gravity * (forceFactor - rb.velocity.y * bounceDamp);
                    rb.AddForceAtPosition(upLift, actionPoint);
                }



            }
        }
        else
        {
        }
    }
}
