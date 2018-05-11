using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    private float _aggroDistance = 14;
    private float _attackDistance = 3;
    private int _health = 3;

    private PlayerController _player;
    private bool _attacking = false;
    private float _attackTime = 1;
    private bool dead = false;

    public int Health
    {
        get { return _health; }
        set { this._health = value; }
    }


	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleDying();

        if (_health <= 0)
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
        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<Animator>().SetBool("Dead", true);
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
