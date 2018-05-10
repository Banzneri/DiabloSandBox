using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    private float _aggroDistance = 7;
    private float _attackDistance = 3;

    private PlayerController _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, _player.transform.position) < _attackDistance)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            GetComponent<NavMeshAgent>().destination = transform.position;
            transform.LookAt(_player.transform.position);
            GetComponent<Animator>().SetBool("Running", false);
        }
        else if (Vector3.Distance(transform.position, _player.transform.position) < _aggroDistance)
        {
            transform.LookAt(_player.transform.position);
            GetComponent<NavMeshAgent>().destination = _player.transform.position;
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
	}
}
