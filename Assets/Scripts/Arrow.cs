using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	[SerializeField] private float distance = 20;
	private Vector3 dir;
	private bool started = false;
	// Use this for initialization
	void Start () {
		dir = GameObject.FindGameObjectWithTag("Player").transform.position + transform.forward * 20;
		dir.y = GameObject.FindGameObjectWithTag("Player").transform.lossyScale.y * 1.5f;
		Debug.Log(dir);
	}
	
	// Update is called once per frame
	void Update () {
		if (!started)
		{
			
		}
		float maxDelta = 0.4f;
		transform.position = Vector3.MoveTowards(transform.position, dir, maxDelta);
		if (Vector3.Distance(transform.position, dir) < 1)
		{
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Arrow hit");
		if (other.GetComponent<Collider>().tag == "Enemy")
		{
			Debug.Log("Arrow hit");
			other.gameObject.GetComponent<Enemy>().DoStun(0.5f);
			other.gameObject.GetComponent<Enemy>().Health -= 1;
		}
		Destroy(this.gameObject);
	}
}
