using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    GameObject player;

    void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update () 
    {
        Vector3 pos = player.transform.position;

        pos.y = transform.position.y;
        pos.z -= 17;
        transform.position = pos;
    }
}
