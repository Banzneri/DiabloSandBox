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
        HandleSeeThroughWalls();
    }

    void HandleSeeThroughWalls()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("SeeThrough"))) {
            Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
            Color color = renderer.material.color;
            color.a = 0.5f;
            renderer.material.color = color;
        }
    }
}
