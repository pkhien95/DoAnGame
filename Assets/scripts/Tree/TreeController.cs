using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {
    public float health;
    public float timer;
    public float healthLossPerChop;

    public bool isBeingChopped = false;
    public GameObject spawner;

    private bool wait = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(health <= 0)
        {
            GameObject.Instantiate(spawner, transform.position,transform.rotation);
            Destroy(gameObject);
        }
        if(isBeingChopped == true)
        {
            if(wait == false)
            {
                Invoke("getChopped", timer);
                wait = true;
            }
        }
	}
    public void getChopped()
    {
        health -= healthLossPerChop;
        wait = false;
    }
    public void startChopping()
    {
        isBeingChopped = true;
    }
    public void stopChopping()
    {
        isBeingChopped = false;
    }
}
