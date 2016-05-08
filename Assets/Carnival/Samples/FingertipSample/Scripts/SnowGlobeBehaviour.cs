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
        GetComponent<Rigidbody>().transform.position = col.attachedRigidbody.transform.position;      
    }
}
