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
            HandleNpcInteraction();
        }
        HandleAttacking();
        if (navMeshAgent.velocity.magnitude < 0.01)
        {
            isMoving = false;
        }
    }

    void HandleMovement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            navMeshAgent.destination = hit.point;
            isMoving = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.tag == "Ground")
                {
                    Instantiate(_destinationParticles, hit.point, Quaternion.Euler(Vector3.zero));
                }
            }   
        }
    }

    void HandleNpcInteraction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, _interactionLayer))
        {
            NpcController npc = hit.collider.gameObject.GetComponentInParent<NpcController>();
            navMeshAgent.destination = npc.transform.position;;
        }
    }

    void HandleAttacking()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}

