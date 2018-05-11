using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {
    PlayerController controller;
    [SerializeField] private LayerMask enemyLayer;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    private void Update() {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 2, enemyLayer))
        {
            collider.GetComponent<Enemy>().Health -= 1;
            collider.GetComponent<Enemy>().DoStun(0.25f);
        }
        this.gameObject.SetActive(false);
    }
}
