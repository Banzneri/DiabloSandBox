using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	private bool opening = false;
	private bool activated = false;
	private Quaternion _originalRotation;

	// Use this for initialization
	void Start () {
		_originalRotation = transform.parent.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (opening)
		{
			Quaternion doorTurn = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.Euler(0.0f, _originalRotation.eulerAngles.y - 90f, 0.0f), Time.deltaTime * 200);
			transform.parent.rotation = doorTurn;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
		{
			opening = true;
		}
	}
}
