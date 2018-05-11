using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
	[SerializeField] private GameObject _monsterPrefab;
	[SerializeField] private float _spawnTime;

	private float _spawnCounter = 0;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_spawnCounter += Time.deltaTime;

		if (_spawnCounter > _spawnTime)
		{
			_spawnCounter = 0;
			Instantiate(_monsterPrefab, transform.position, transform.rotation);
		}
	}
}
