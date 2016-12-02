using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public float spawnTime = 10f;
	// Use this for initialization
	void Start () {
        Invoke("InstantiateTree", spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void InstantiateTree()
    {
        ((BuildingManager)GameObject.Find("BuildingManager").GetComponent(typeof(BuildingManager))).instantiateTree(transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
