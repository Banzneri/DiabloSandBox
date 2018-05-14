using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageBox : MonoBehaviour {
    PlayerController controller;
    private int maxHits = 5;
    private int hits = 0;
    private float damage = 1;
    private float attackTime = 1;
    private List<Enemy> enemiesHit = new List<Enemy>();
    private Attack _attack;

    [SerializeField] private LayerMask enemyLayer;

	// Use this for initialization
	void Start () {
        
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    private void Update() {
        bool hit = false;
        Collider[] possibleTargets = GetPossibleTargets();
        Enemy[] enemies = SortEnemiesBasedOnDistance(possibleTargets);
        int maxIndex = Mathf.Clamp(maxHits, 0, enemies.Length);

        if (enemies.Length == 0)
        {
            return;
        }

        for (int i = 0; i < maxIndex; i++)
        {
            enemies[i].DoStun(0.25f);
            enemies[i].Health -= (int)damage;
            hit = true;
        }

        if (hit) this.gameObject.SetActive(false);
    }

    public void SetAttack(Attack attack)
    {
        _attack = attack;
    }

    private Collider[] GetPossibleTargets()
    {
        switch (_attack._damageAreaShape)
        {
            case Attack.DamageAreaShape.Box:
                return Physics.OverlapBox(transform.position, 
                                            new Vector3(_attack._damageAreaWidth / 2, _attack._damageAreaWidth, _attack._damageAreaLength / 2), 
                                            transform.rotation, enemyLayer);
            case Attack.DamageAreaShape.Sphere:
                return Physics.OverlapSphere(transform.position, _attack._damageAreaWidth, enemyLayer);
            case Attack.DamageAreaShape.Capsule:
                return Physics.OverlapCapsule(transform.position, transform.position, _attack._damageAreaWidth, enemyLayer);
        }

        return new Collider[0];
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
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.magenta;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        
        switch (_attack._damageAreaShape)
        {
            case Attack.DamageAreaShape.Box:
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(_attack._damageAreaWidth, _attack._damageAreaWidth, _attack._damageAreaLength));
                break;
            case Attack.DamageAreaShape.Sphere:
                Gizmos.DrawWireSphere(Vector3.zero, _attack._damageAreaWidth);
                break;
            case Attack.DamageAreaShape.Capsule:
                
                break;
        }
    }
}