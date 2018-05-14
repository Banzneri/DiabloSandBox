using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    [SerializeField] LayerMask _seeThroughLayer;
    GameObject _player;
    
    private float _yOffset;

    void Start () 
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _yOffset = transform.position.y - _player.transform.position.y;
    }

    void Update () 
    {
        Vector3 pos = _player.transform.position;

        pos.y += _yOffset;
        pos.z -= 13;
        transform.position = pos;
    }
}
