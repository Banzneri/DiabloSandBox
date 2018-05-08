using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour {
    [SerializeField] private LayerMask _layer;
    [SerializeField] private string _greeting;
    GameObject player;

    bool show = false;
    bool activated = false;
    float waitCounter = 0.0f;
    float waitTime = 2.0f;
    float activateDistance = 2f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, _layer))
            {
                activated = true;
            }
            else if (Physics.Raycast(ray, out hit))
            {
                show = false;
                activated = false;
            }
        }

        if (activated)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < activateDistance)
            {
                show = true;
                player.GetComponent<NavMeshAgent>().destination = player.transform.position;
            }
        }
    }

    private void OnGUI()
    {
        if (show)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 20 ), _greeting);
        }
    }
}
