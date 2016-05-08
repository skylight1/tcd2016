using UnityEngine;
using System.Collections;

public class SphereBehaviourScript : MonoBehaviour {

    private float _initY;

    void Start()
    {
        _initY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SphereCollider>().bounds.max.y < -1) {
			Application.LoadLevel ("GestureSample");
		}

	}
}
