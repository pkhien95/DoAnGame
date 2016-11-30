using UnityEngine;
using System.Collections;
using System;

public class BuildingObject : MonoBehaviour {
    private bool isBuilding = true;
    public bool canPlace = true;
    public bool isInCollision = false;
    private Renderer renderer;
    
    [System.Serializable]
    public class Materials
    {
        public Material originMaterial;
        public Material canPlaceMaterial;
        public Material cannotPlaceMaterial;
    }

    [System.Serializable]
    public class Transformation
    {
        public float groundOffset;
    }

    public Materials materialsSetting = new Materials();
    public Transformation transformation = new Transformation();

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if(isBuilding)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Ground" && !isInCollision)
                {
                    Debug.Log("Canplaced");
                    canPlace = true;
                }
                else
                {
                    canPlace = false;
                }
                transform.position = new Vector3(hit.point.x, hit.point.y + transformation.groundOffset, hit.point.z);
            }


            if(Input.GetMouseButtonDown(0))
            {
                if(isBuilding == true && canPlace == true)
                {
                    isBuilding = false;
                }
            }
        }
        UpdateMaterial();
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ground")
        {
            isInCollision = true;
            canPlace = false;

        }
        UpdateMaterial();
        Debug.Log("triggered " + other.gameObject.name + " " + isInCollision);
    }

    public void OnTriggerExit(Collider other)
    {
        isInCollision = false;
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        if (isBuilding)
        {
            if (canPlace)
            {
                renderer.sharedMaterial = materialsSetting.canPlaceMaterial;
            }
            else
            {
                Debug.Log("XXX");
                renderer.sharedMaterial = materialsSetting.cannotPlaceMaterial;
            }
        }
        else
        {
            renderer.sharedMaterial = materialsSetting.originMaterial;
        }
    }
}
