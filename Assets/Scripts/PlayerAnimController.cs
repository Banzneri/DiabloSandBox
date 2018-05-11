using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour {
	private PlayerController _player;
	private Animator _anim;
	// Use this for initialization
	void Start () {
		_player = GetComponent<PlayerController>();
		_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		_anim.SetBool("Running", _player.IsMoving);
		_anim.SetBool("Attacking", _player.attacking);
	}
}
