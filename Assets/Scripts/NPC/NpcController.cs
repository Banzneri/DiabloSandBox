using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour {
    [SerializeField] private LayerMask _layer;
    [SerializeField] private string _greeting;
    [SerializeField] private string _name;
    GameObject player;

    bool show = false;
    bool activated = false;
    float showTime = 2.0f;
    float activateDistance = 2f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (activated && !show)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < activateDistance)
            {
                show = true;
                activated = false;
                player.GetComponent<NavMeshAgent>().destination = player.transform.position;
                Vector3 lookAt = transform.position;
                lookAt.y = player.transform.position.y;
                player.transform.LookAt(lookAt);
                StartCoroutine(StopShowing());
            }
        }
    }

    IEnumerator StopShowing()
    {
        yield return new WaitForSeconds(showTime);
        show = false;
        Debug.Log("StopShowing");
    }

    public void SetActivated(bool activate)
    {
        if (show)
        {
            return;
        }
        activated = activate;
    }

    private void OnGUI()
    {
        if (show)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 20 ), _greeting + " " + _name);
        }
    }
}
