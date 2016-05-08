using UnityEngine;
using System.Collections;

public class SnowmanBehaviour : MonoBehaviour {
	public GameObject thermometerFluid;

	void OnTriggerEnter(Collider other) {
					Vector3 scale = thermometerFluid.transform.localScale;
		
					thermometerFluid.transform.localScale = new Vector3(scale.x, scale.y - 0.15f, scale.z);
		Destroy(other.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
