using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _npcLayer;

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
        if (navMeshAgent.remainingDistance < 1)
        {
            isMoving = false;
        }
    }

    void HandleMovement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, _groundLayer))
        {
            navMeshAgent.destination = hit.point;
            isMoving = true;
        }
    }

    void HandleNpcInteraction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, _npcLayer))
        {
            Debug.Log("ClickNpc");
            hit.collider.gameObject.GetComponent<NpcController>().SetActivated(true);
            navMeshAgent.destination = hit.point;
        }
    }
}

