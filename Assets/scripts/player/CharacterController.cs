using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    [System.Serializable]
    public class Movement
    {
        public float moveSpeed = 0;
        public float rotateSpeed = 0.5f;
        public float maxDistanceToTarget = 1.5f;
        public Vector3 target;
    }

    NavMeshAgent nav;
    Animator animator;
    public Movement movement = new Movement();
    private bool isRotating = false;

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        movement.target = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If user right clicks
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Right clicked ");
            Vector3 target = getClickedPosition();
            if (target != Vector3.zero)
            {
                movement.target = target;
            }
        }
        if (Vector3.Distance(movement.target, transform.position) > movement.maxDistanceToTarget)
        {
            UpdateMovement(movement.target);
            UpdateRotation(movement.target);
        }
        movement.moveSpeed = nav.desiredVelocity.magnitude;
        animator.SetFloat("speed", movement.moveSpeed);
    }

    private void UpdateMovement(Vector3 target)
    { 
        //Set nav destination
        nav.SetDestination(target);
        nav.updateRotation = false;
    }

    private void UpdateRotation(Vector3 target)
    {
        //Look at target
        Vector3 dir = target - transform.position;
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, movement.rotateSpeed * Time.deltaTime);
    } 

    private Vector3 getClickedPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        { 
            if(hit.collider.gameObject.tag == "Ground")
            {
                Debug.Log(hit.collider.gameObject.tag);
                return hit.point;
            }
        }
        return Vector3.zero;
    }
}
