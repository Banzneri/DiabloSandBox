using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {
    PlayerController controller;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Vector3 knockbackForce = controller.transform.forward;
            knockbackForce.y = 1;
            other.GetComponent<Rigidbody>().AddForce(knockbackForce * 20, ForceMode.Impulse);
            this.gameObject.SetActive(false);
        }
    }
}
