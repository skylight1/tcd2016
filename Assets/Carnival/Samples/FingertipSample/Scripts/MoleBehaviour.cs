using UnityEngine;
using System.Collections;

public class MoleBehaviour : MonoBehaviour
{
    private float _initY;

    void Start()
    {
        // Remember initial position
        _initY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Tag mole object according to its position

        // Arrived its initial position?
        if (transform.position.y >= _initY)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.tag = "aboveGround";
        }

        // Underneath ground?
        if (GetComponent<CapsuleCollider>().bounds.max.y < 0)
        {
            gameObject.tag = "underGround";
            GetComponent<Rigidbody>().useGravity = false;
            Vector3 vector3 = transform.position;
            vector3.y = _initY;
            transform.position = vector3;   
            GetComponent<Rigidbody>().transform.position = vector3;         
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (gameObject.tag == "aboveGround")
        {
            // Mole falls down when it is hit by collider
            GetComponent<Rigidbody>().useGravity = true;
            GameObject.Find("snowball").GetComponent<Rigidbody>().useGravity = true;
            gameObject.tag = "movingDown";
        } 
        if(col.gameObject.name == "snowglobe") {        
            GameObject.Find("snowglobe").GetComponent<Rigidbody>().useGravity = true;
            GameObject.Find("snowglobe").GetComponent<Rigidbody>().transform.position = new Vector3(0,-1,0);
//            GameObject.Find("Cylinder2").GetComponent<Rigidbody>().isKinematic = false;
            GameObject.Find("Cylinder2").GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
