using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _npcLayer;
    [SerializeField] LayerMask _interactionLayer;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] GameObject _destinationParticles;
    [SerializeField] GameObject _damageZone;
    [SerializeField] GameObject _arrowPrefab;
    

    private Vector3 destination = Vector3.zero;
    private NavMeshAgent navMeshAgent;
    [HideInInspector] public bool attacking = false;

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

    void FaceMonsterWhenAttacking()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("FaceNotYet");
        if (Physics.Raycast(ray, out hit, 1000, _enemyLayer))
        {
            Debug.Log("FaceYes");
            transform.LookAt(hit.collider.transform.position);
        }
    }

    void HandleAttacking()
    {
        if (attacking)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Attack(1f, "Attack1"));
        }

        else if (Input.GetMouseButton(1))
        {
            StartCoroutine(Attack(1f, "Attack2"));
        }

        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(Attack(1f, "Attack3"));
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 arrowSpawnPos = transform.position;
            arrowSpawnPos.y += transform.lossyScale.y * 1.5f;
            Util.RotateToMouseDirection(transform);
            Instantiate(_arrowPrefab, arrowSpawnPos, transform.rotation);
        }
    }

    IEnumerator Attack(float attackTime, string attackName)
    {
        attacking = true;
        navMeshAgent.destination = transform.position;
        GetComponent<Animator>().CrossFadeInFixedTime(attackName, 0.1f, 1);
        GetComponent<Animator>().SetBool("Attacking", true);
        Util.RotateToMouseDirection(transform);
        FaceMonsterWhenAttacking();
        yield return new WaitForSeconds(attackTime / 2.2f);
        _damageZone.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        _damageZone.SetActive(false);
        attacking = false;
    }
}

