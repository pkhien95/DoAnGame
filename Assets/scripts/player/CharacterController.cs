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

    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int pickingUpState = Animator.StringToHash("Base Layer.Pick Up");
    static int chopDownState = Animator.StringToHash("Base Layer.Chop Down");

    private GameObject targetTree = null;

    private AnimatorStateInfo currentState;

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
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right clicked ");
            //destroy current tree and create the new one at the same place
            if (targetTree != null)
            {
                (targetTree.GetComponent(typeof(TreeController)) as TreeController).stopChopping();
                //((BuildingManager)GameObject.Find("BuildingManager").GetComponent(typeof(BuildingManager))).instantiateTree(targetTree.transform.position,targetTree.transform.rotation);
                animator.SetBool("chopdown", false);
                movement.maxDistanceToTarget = 1.5f;
                //Destroy(targetTree);
                targetTree = null;
            }
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
        else
        {
            if (targetTree != null)
            {
                //stop moving and start chopping
                nav.SetDestination(transform.position);
                movement.target = transform.position;
                animator.SetBool("chopdown", true);
                //Destroy(targetTree, 5);
                ((TreeController)targetTree.GetComponent(typeof(TreeController))).startChopping();
            }
            else//tree has been chopped down
            {
                animator.SetBool("chopdown", false);
            }
        }

        movement.moveSpeed = Vector3.Distance(transform.position, movement.target);
        animator.SetFloat("speed", movement.moveSpeed);

        currentState = animator.GetCurrentAnimatorStateInfo(0);

        if(currentState.nameHash == locoState)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pick up !");
                animator.SetBool("pickup", true);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("chopdown", true);
            }
        }
        if(currentState.nameHash == pickingUpState)
        {
            if(!animator.IsInTransition(0))
            {
                animator.SetBool("pickup", false);
            }
        }
        if (currentState.nameHash == chopDownState)
        {
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetBool("chopdown", false);
            }
        }
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
        if(Vector3.Distance(target, transform.position) <= nav.stoppingDistance)
        {
            return;
        }
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
            if (hit.collider.gameObject.tag == "Tree")
            {
                targetTree = hit.collider.gameObject;
                Vector3 treeCenter = targetTree.GetComponent<BoxCollider>().center;
                treeCenter.Set(treeCenter.x, 0, treeCenter.z);
                //return center of tree;
                return treeCenter + targetTree.transform.position;
            }
            if(hit.collider.gameObject.tag == "Ground")
            {
                Debug.Log(hit.collider.gameObject.tag);
                return hit.point;
            }
        }
        return Vector3.zero;
    }
}
