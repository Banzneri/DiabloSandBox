using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IComparable<Enemy> {
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private float _aggroDistance = 14;
    [SerializeField] private float _attackDistance = 3;
    [SerializeField] private int _health = 3;

    private PlayerController _player;
    private bool _attacking = false;
    private float _attackTime = 0.7f;
    private bool dead = false;
    private bool _stunned = false;

    public int Health
    {
        get { return _health; }
        set { this._health = value; }
    }

    public bool CanMove
    {
        get { return _health > 0 && !_stunned; }
    }

    public float GetDistanceToPlayer
    {
        get { return Vector3.Distance(transform.position, _player.transform.position); }
    }


	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleDying();

        if (!CanMove)
        {
            return;
        }

        HandleApproaching();
        HandleAttacking();
	}

    void HandleAttacking()
    {
        if (_attacking)
        {
            return;
        }
        if (Vector3.Distance(transform.position, _player.transform.position) < _attackDistance)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
        //GetComponent<Animator>().CrossFadeInFixedTime("Attack", 1.5f, 0);
        GetComponent<NavMeshAgent>().destination = transform.position;
        GetComponent<NavMeshAgent>().isStopped = true;
        transform.LookAt(_player.transform.position);
        GetComponent<Animator>().SetBool("Running", false);
        _attacking = true;
        yield return new WaitForSeconds(_attackTime);
        _attacking = false;
    }

    void HandleApproaching()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < _aggroDistance)
        {
            transform.LookAt(_player.transform.position);
            if (_attacking)
            {
                return;
            }
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().destination = _player.transform.position;
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
            GetComponent<NavMeshAgent>().destination = transform.position;
        }
    }

    void HandleDying()
    {
        if (_health <= 0 && !dead)
        {
            dead = true;
            Debug.Log("Die");
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        StartCoroutine(Util.FlickerMe(_renderers, 0.1f));
        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<OutlineManager>().enabled = false;
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private IEnumerator Stun(float stunTime)
    {
        Debug.Log("Stun");
        _stunned = true;
        GetComponent<Animator>().SetBool("Stunned", true);
        GetComponent<Animator>().SetTrigger("Stun");
        GetComponent<Animator>().CrossFadeInFixedTime("Knockback", 0, 0);
        yield return new WaitForSeconds(stunTime);
        GetComponent<Animator>().SetBool("Stunned", false);
        _stunned = false;
    }

    public void DoStun(float stunTime)
    {
        StartCoroutine(Stun(stunTime));
    }

    public int CompareTo(Enemy other)
    {
        // If other is not a valid object reference, this instance is greater.
        if (other == null) return 1;

        // The temperature comparison depends on the comparison of 
        // the underlying Double values. 
        return GetDistanceToPlayer.CompareTo(other.GetDistanceToPlayer);
    }
}
