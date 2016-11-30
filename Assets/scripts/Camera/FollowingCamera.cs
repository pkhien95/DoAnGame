using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour {

    public GameObject cameraPivot;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = cameraPivot.transform.position;
	}
}
