using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _npcLayer;
    [SerializeField] LayerMask _interactionLayer;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LayerMask _menuLayer;
    [SerializeField] GameObject _destinationParticles;
    [SerializeField] GameObject _damageZone;
    [SerializeField] GameObject _arrowPrefab;
    

    private Enemy _currentTarget = null;
    private Attack _attackInQueue = null;
    private Vector3 destination = Vector3.zero;
    private NavMeshAgent navMeshAgent;
    private Vector3 _destination;
    [HideInInspector] public bool attacking = false;

    private bool isMoving = false;

    public Vector3 Destination 
    {
        get { return _destination; }
        set { _destination = value; }
    }

    public bool IsMoving
    {
        get { return isMoving; }
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _destination = transform.position;
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

        bool mouseDown = Input.GetMouseButtonDown(0);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
 
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
 
        if (results.Count > 0) {
            //WorldUI is my layer name
            if (results[0].gameObject.layer == LayerMask.NameToLayer("Menu"))
            { 
                return;
            }
        }

        if (Physics.Raycast(ray, out hit, 1000, _enemyLayer))
        {
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(hit.point, out navMeshHit, 100, 1))
            {
                Debug.Log("EnemyCLicked");
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                _currentTarget = enemy;
                _attackInQueue = Attacks.MeleeAttackDefault();
                if (enemy.GetDistanceToPlayer > 2)
                {
                    navMeshAgent.destination = navMeshHit.position;
                    isMoving = true;   
                }
            }
        }

        else if (Physics.Raycast(ray, out hit, 1000, _interactionLayer))
        {
            ResetTargetAndAttack();
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
            ResetTargetAndAttack();
            if (mouseDown)
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

        if (_currentTarget != null)
        {
            Debug.Log("current not null");
            navMeshAgent.destination = _currentTarget.transform.position;
        }
    }

    void ResetTargetAndAttack()
    {
        _attackInQueue = null;
        _currentTarget = null;
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

        if (_currentTarget != null)
        {
            if (_currentTarget.GetDistanceToPlayer < 2)
            {
                StartCoroutine(DoAttack(_attackInQueue));
                ResetTargetAndAttack();
            }
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            StartCoroutine(DoAttack(Attacks.TestAttack1()));
        }

        else if (Input.GetMouseButton(1))
        {
            StartCoroutine(DoAttack(Attacks.MeleeAttackDefault()));
        }

        else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(DoAttack(Attacks.TestAttack2()));
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 arrowSpawnPos = transform.position;
            arrowSpawnPos.y += transform.lossyScale.y * 1.5f;
            Util.RotateToMouseDirection(transform);
            Instantiate(_arrowPrefab, arrowSpawnPos, transform.rotation);
        }
    }

    IEnumerator DoAttack(Attack attack)
    {
        attacking = true;
        navMeshAgent.destination = transform.position;
        GetComponent<Animator>().CrossFadeInFixedTime(attack._attackName, 0.1f, 1);
        GetComponent<Animator>().SetBool("Attacking", true);
        Util.RotateToMouseDirection(transform);
        FaceMonsterWhenAttacking();
        yield return new WaitForSeconds(attack._attackDuration / 2.2f);
        _damageZone.GetComponent<AttackDamageBox>().SetAttack(attack);
        _damageZone.SetActive(true);
        yield return new WaitForSeconds(attack._attackDuration * 0.5f);
        _damageZone.SetActive(false);
        attacking = false;
    }
}

