using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Carnival;

public class FingertipSampleBehaviour : MonoBehaviour
{
	public GameObject hammerPrefab;
	// A hammer object with collider to hit moles

	private Controller _carnivalController;
	// Controller to access frame data
		public MeshFilter handMesh;
private MeshFilter _handMeshVertices;
private int[] _indices;
private bool movingball = false;
private bool grabbedball = true;
private bool letgo = false;
private Vector3 defaultPos = new Vector3((float)0.54,(float)1.31,(float)0.508);

	// Use this for initialization
	void Start()
	{
		// Initialize controller
		Debug.Log("FingertipSampleBehaviour: Initializing sensor");
		_carnivalController = new Controller();
		_carnivalController.Init();
		_carnivalController.Start();
		
		_handMeshVertices = GameObject.Find("handMeshVertices").GetComponent<MeshFilter>();

		// Repeat method every 3 seconds
//		InvokeRepeating("showRandomMoles", 2, 3F);
	}

	// Update is called once per frame
	void Update()
	{
	    // Back button on Android is mapped to Escape
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

		foreach(GameObject hammer in GameObject.FindGameObjectsWithTag("hammer"))
		{
			Destroy(hammer);
		}

		// Get the current frame
		Frame frame = _carnivalController.Frame();
		
        #region Use point cloud to make player easier recognize if hand is inside sensor field of view
        _handMeshVertices.mesh.Clear();
        _handMeshVertices.mesh.vertices = frame.PointCloud;

		handMesh.mesh.Clear();

		if(frame.meshData.vertices.Length > 0)
		{
			// You need vertices positions and a triangle indicies array to render mesh in real time
			handMesh.mesh.vertices = frame.meshData.vertices;
			handMesh.mesh.triangles = frame.meshData.triangles;
			handMesh.mesh.RecalculateNormals();
		}

        _indices = new int[frame.PointCloud.Length];

        for (int i = 0; i < frame.PointCloud.Length; i++)
        {
            // Use depth confidence to filter noise points, this is very useful if you want distinguish hand
            // from other objects in background
            if (frame.DepthConfidence[i] > 0 )
                _indices[i] = i;
        }

        _handMeshVertices.mesh.SetIndices(_indices, MeshTopology.Points, 0);
		#endregion

		// Fingertips are always linked to a certain Hand.
		foreach(Hand hand in frame.Hands)
		{
			foreach(Fingertip tip in hand.Fingertips)
			{
				GameObject hammer = Instantiate(hammerPrefab) as GameObject;
				hammer.transform.parent = Camera.main.transform;

				// 3D loaction of fingertip is relative position to sensor. Here we keep it simple since sensor is mounted
				// quite close to your eyes which is main camera. For better UX you should actually take the distance into
				// account
				hammer.transform.localPosition = tip.Center3D;
            	if(GameObject.Find("snowglobe").GetComponent<Rigidbody>().useGravity == true ) {
					//Debug.Log("snowglobe grav true");
                	GameObject.Find("snowglobe").transform.position = hammer.transform.position;
					movingball = true;
            	}

			}
		}
		
		switch (frame.Gestures[0].Type)
        {
            case GestureType.Clamp:
				letgo = false;
				if(movingball) {
				    grabbedball = true; 
					movingball = false;
				} else if (grabbedball) {
//					GameObject.Find("snowglobe").GetComponent<Rigidbody>().useGravity = false;
//					GameObject.Find("snowglobe").GetComponent<Rigidbody>().transform.position = defaultPos;
				}
                break;
            case GestureType.Swipe:
                break;
            default:
                break;
        }
		
	}

	// Randomly show moles above the ground
	void showRandomMoles()
	{
		GameObject[] moles = GameObject.FindGameObjectsWithTag("underGround");
		System.Random rand = new System.Random();
		foreach(GameObject mole in moles)
		{
			// 50% chance to move moles beneath ground upwards
			if(rand.NextDouble() >= 0.5)
			{
				mole.GetComponent<Rigidbody>().useGravity = false;
				mole.GetComponent<Rigidbody>().velocity = Vector3.up;
				gameObject.tag = "movingUp";
			}
		}
	}

	void OnDestroy()
	{
		Debug.Log("FingertipSampleBehaviour: OnDestroy");
		_carnivalController.Stop();
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if(_carnivalController == null)
		{
			return;
		}

		Debug.Log("FingertipSampleBehaviour: OnApplicationPause -> " + pauseStatus);

		if(pauseStatus)
		{
			_carnivalController.Stop();
		}
		else
		{
			_carnivalController.Start();
		}
	}
}
