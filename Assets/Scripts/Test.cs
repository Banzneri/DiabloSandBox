using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private void OnMouseEnter() {
		Debug.Log("Mouse enter block");
		foreach (Renderer renderer in GetComponents<Renderer>())
		{
			renderer.material.SetColor("Color", Color.green);
		}
	}

	private void OnMouseExit() {
		Debug.Log("Mouse exit block");
		foreach (Renderer renderer in GetComponents<Renderer>())
		{
			renderer.material.SetColor("Color", Color.red);
		}
	}
}
