﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {
    PlayerController controller;
    private int maxHits = 2;
    private int hits = 0;
    private float damage = 1;
    private float attackTime = 1;
    private List<Enemy> enemiesHit = new List<Enemy>();

    [SerializeField] private LayerMask enemyLayer;

	// Use this for initialization
	void Start () {
        
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    private void Update() {
        bool hit = false;
        Collider[] possibleTargets = Physics.OverlapSphere(transform.position, 2f, enemyLayer);
        Enemy[] enemies = SortEnemiesBasedOnDistance(possibleTargets);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].DoStun(0.25f);
            enemies[i].Health -= (int)damage;
            hit = true;
            Debug.Log("Hithithit");
            if (i+1 == maxHits)
            {
                break;
            }
        }

        if (hit) this.gameObject.SetActive(false);
    }

    private Enemy[] SortEnemiesBasedOnDistance(Collider[] colliders)
    {
        Enemy[] enemies = new Enemy[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            enemies[i] = colliders[i].GetComponent<Enemy>();
        }
        System.Array.Sort(enemies);
        return enemies;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}