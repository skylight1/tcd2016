    using UnityEngine;
using System.Collections;
using Carnival;

public class SnowGlobeBehaviour : MonoBehaviour
{
//    Controller _carnivalController;
    public bool flag = false;
    void Start()
    {
//        _carnivalController = new Controller();
//		_carnivalController.Init();
//		_carnivalController.Start();
		
    }

    // Update is called once per frame
    void Update()
    {
//        if(flag) {
//    		Frame frame = _carnivalController.Frame();
//            foreach(Hand hand in frame.Hands)
//            {
//                if(hand.Handedness == Handedness.Right) {
//                    transform.localPosition = hand.CenterOfGravity;
//                }
//            }
//        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("SnowGlobeBhavior.OnTriggerEnter");
        GetComponent<Rigidbody>().useGravity = true;
        flag = true;
//        transform.position = col.attachedRigidbody.transform.position;  
        
        GameObject.Find("mole").GetComponent<Rigidbody>().useGravity = false;
        GameObject.Find("mole").transform.position = new Vector3((float)-0.0974,(float)1.23,(float)0.667);
    
    }
}
