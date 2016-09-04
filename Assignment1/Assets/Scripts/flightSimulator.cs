using UnityEngine;
using System.Collections;

public class flightSimulator : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right);
        }
        if (Input.GetKey("q"))
        {
            transform.Rotate(Vector3.forward);
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(Vector3.back);
            
        }

        transform.LookAt(Input.mousePosition);

    }
}
