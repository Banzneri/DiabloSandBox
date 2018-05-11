using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    [SerializeField] LayerMask _seeThroughLayer;
    GameObject _player;

    void Start () 
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update () 
    {
        Vector3 pos = _player.transform.position;

        pos.y = transform.position.y;
        pos.z -= 13;
        transform.position = pos;
    }
}
