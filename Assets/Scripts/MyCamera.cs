using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    [SerializeField] LayerMask _seeThroughLayer;
    GameObject _player;
    
    private float _yOffset;
    private float _zOffset;

    void Start () 
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _yOffset = transform.position.y - _player.transform.position.y;
        _zOffset = transform.position.z - _player.transform.position.z;
    }

    void Update () 
    {
        Vector3 pos = _player.transform.position;

        pos.y += _yOffset;
        pos.z += _zOffset;
        transform.position = pos;
    }
}
