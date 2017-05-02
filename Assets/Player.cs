using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject blastSprite;
    public float power;
    public float range;

    public float speed;

    public Text scoreText;
    public int score;

    public float viewAngle;
    public bool onGround;
    public GameObject view;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        //transform.position += movement * speed;
        rb.AddForce(movement * speed);

        view.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        view.GetComponent<RectTransform>().Rotate(0, 0, viewAngle / 2);
        view.GetComponent<Image>().fillAmount = viewAngle / 360;

        view.transform.localScale = new Vector3(range / 5, range / 5, range / 5);
        transform.GetChild(1).transform.localScale = new Vector3(range, range, range);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.LookAt(hit.point);
            Transform trans = transform;
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            transform.Rotate(new Vector3(0, trans.rotation.y, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(onGround)
            {
                onGround = false;
            rb.AddForce(transform.up * 300);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ViewCheck();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Boom();
            GameObject blast = Instantiate(blastSprite, transform.position, blastSprite.transform.rotation);
            blast.transform.position -= new Vector3(0, 0.9f, 0);
            blast.GetComponent<BlastSprite>().radius = range;
        }


    }


    public void Boom()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < fruits.Length; i++)
        {
            fruits[i].GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, range);
        }
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, range);
        }
    }

    public void ViewCheck()
    {
        Collider[] listOfObjects = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), range);
        for (var i = 0; i < listOfObjects.Length; i++)
        {
            if (listOfObjects[i].CompareTag("Fruit"))
            {
                Vector3 directionToTarget = transform.position - listOfObjects[i].transform.position;
                float angle = Vector3.Angle(transform.forward, -directionToTarget);

                //   float angle = Vector3.Angle(transform.forward, listOfObjects[i].gameObject.transform.position);

                if (angle < viewAngle / 2)
                {
                    listOfObjects[i].GetComponent<Fruit>().Collect(transform);
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            Collider[] listOfObjects = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.5f);
            for (var i = 0; i < listOfObjects.Length; i++)
            {
                if (listOfObjects[i].CompareTag("Ground"))
                {
                    onGround = true;    
                }
            }
        }
    }


}
