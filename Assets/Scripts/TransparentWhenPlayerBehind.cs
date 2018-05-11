using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWhenPlayerBehind : MonoBehaviour {
	[SerializeField] private LayerMask _seeThroughLayer;
	[SerializeField] private Material _standardMaterial;
	[SerializeField] private Material _transparentMaterial;
	private GameObject _player;

	private bool _visible = true;
	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		GetComponent<Renderer>().enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(_player.transform.position));
        if (Physics.Raycast(ray, out hit, 1000, _seeThroughLayer))
        {
            if (hit.collider.gameObject == this.gameObject)
			{
				if (Physics.Raycast(ray, out hit, 1000, _seeThroughLayer)) 
				{
            		GetComponent<Renderer>().enabled = false;
				}
			}
        }
	}
}
