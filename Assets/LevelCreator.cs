using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public GameObject current;
    public GameObject Floor;
    public GameObject Water;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            //    current.transform.position = hit.point;
            if (current)
                current.transform.position = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (current)
            {
                Destroy(current);
            }
            current = Instantiate(Floor);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (current)
            {
                Destroy(current);
            }
            current = Instantiate(Water);
        }

        if (Input.GetMouseButtonDown(0))
        {
            current = null;
        }
    }
}
