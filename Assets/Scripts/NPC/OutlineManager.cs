﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour {
	[SerializeField] GameObject[] renderObjects;
	[SerializeField] float outLineWidth;
	
	public bool active = true;

	private void OnMouseEnter() {
		if (!active)
		{
			return;	
		}
        Debug.Log("Mouse enter npc");
		foreach (GameObject renderObj in renderObjects)	
		{
			foreach (Renderer renderer in renderObj.GetComponents<Renderer>())
			{
				foreach (Material material in renderer.materials)
				{
					material.SetFloat("_Outline", outLineWidth);
				}
			}	
		}
	}

	private void OnMouseExit() {
        Debug.Log("Mouse exit npc");
		foreach (GameObject renderObj in renderObjects)
		{
			foreach (Renderer renderer in renderObj.GetComponents<Renderer>())
			{
				foreach (Material material in renderer.materials)
				{
					material.SetFloat("_Outline", 0f);
				}
			}	
		}
		Debug.Log("Mouse exit npc");
	}
}
