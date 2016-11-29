using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
    public Transform target;
    private NavMeshAgent agent;
    private float distanceToPlayer;
    private Animator anim;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);
        distanceToPlayer = Vector3.Distance(this.gameObject.transform.position, target.position);
        if (distanceToPlayer < 2)
        {
            anim.SetBool("nearPlayer", true);
        }
        else
        {
            anim.SetBool("nearPlayer", false);
        }
    }
}
