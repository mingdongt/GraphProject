using UnityEngine;

public class flightSimulator : MonoBehaviour
{

    private Vector3 mouseOrigin;
    bool start=false;
    // Update is called once per frame
    void Update()
    {
        //Boundaries
        if (transform.position.x > 1000)
        {
            transform.position += Vector3.left;
        }
        else if (transform.position.x < 0)
        {
            transform.position += Vector3.right;
        }
        else if (transform.position.z < 0)
        {
            transform.position += Vector3.forward;
        }
        else if (transform.position.z > 1000)
        {
            transform.position += Vector3.back;
        }

        float terrainHeightAtThisPoint = Terrain.activeTerrain.SampleHeight(transform.position);

        if (terrainHeightAtThisPoint > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, terrainHeightAtThisPoint+5, transform.position.z);
        }


        //Movement
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


        //Mouse control
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            start = true;
            
        }

        if (start)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            transform.RotateAround(transform.position, transform.right, -pos.y * 2);
            transform.RotateAround(transform.position, Vector3.up, pos.x * 2);    
        }

    }

}
