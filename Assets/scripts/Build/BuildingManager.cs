using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
    public GameObject wall;
    public GameObject turret;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject.Instantiate(wall, Input.mousePosition, wall.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject.Instantiate(turret, Input.mousePosition, turret.transform.rotation);
        }
    }
}
