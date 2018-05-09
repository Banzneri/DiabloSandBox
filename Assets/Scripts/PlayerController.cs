using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _npcLayer;
    [SerializeField] LayerMask _interactionLayer;
    [SerializeField] GameObject _destinationParticles;
    

    private Vector3 destination = Vector3.zero;
    private NavMeshAgent navMeshAgent;

    private bool isMoving = false;

    public bool IsMoving
    {
        get { return isMoving; }
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleMovement();
        }
        HandleAttacking();
        if (navMeshAgent.velocity.magnitude < 0.01)
        {
            isMoving = false;
        }
    }

    IEnumerator RotateMe(Vector3 targetPosition, float speed) 
    {
        while (true)
        {
            Vector3 dir = targetPosition - transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rot) < 1)
            {
                yield break;
            }
        }
    }

    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, _interactionLayer))
        {
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(hit.point, out navMeshHit, 100, 1))
            {
                    NpcController npc = hit.collider.gameObject.GetComponentInParent<NpcController>();
                    navMeshAgent.destination = navMeshHit.position;
                    isMoving = true;
            }
        }

        else if (Physics.Raycast(ray, out hit, 1000, _groundLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(_destinationParticles, hit.point, Quaternion.Euler(Vector3.zero));
            }
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(hit.point, out navMeshHit, 100, 1))
            {
                   navMeshAgent.destination = navMeshHit.position;
                   isMoving = true;
            }
        }
    }

    void HandleAttacking()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<Animator>().SetTrigger("Attack");
            navMeshAgent.destination = transform.position;
        }

        else if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Animator>().SetTrigger("Attack1");
            navMeshAgent.destination = transform.position;
        }

        else if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Animator>().SetTrigger("Attack2");
            navMeshAgent.destination = transform.position;
        }
    }
}

