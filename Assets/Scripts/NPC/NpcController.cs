using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour {
    [SerializeField] private LayerMask _layer;
    [SerializeField] private LayerMask _interactLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private string _greeting;
    [SerializeField] private string _name;
    [SerializeField] private Material _activeMaterial;
    [SerializeField] private Material _inActiveMaterial;
    [SerializeField] private GameObject _selectionOutline;

    GameObject player;

    bool show = false;
    bool activated = false;
    float showTime = 2.0f;
    float activateDistance = 2f;

    public bool Activated
    {
        get { return activated; }
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        HandleActivate();
        if (activated && !show)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < activateDistance)
            {
                DoInteraction();
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

    void DoInteraction()
    {
        show = true;
        activated = false;
        player.GetComponent<NavMeshAgent>().destination = player.transform.position;
        Vector3 lookAt = transform.position;
        lookAt.y = player.transform.position.y;
        player.transform.LookAt(lookAt);
        
        StartCoroutine(Util.Rotateme(transform, player.transform.position, 10));
        StartCoroutine(StopShowing());
    }

    void HandleActivate()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, _interactLayer))
            {
                NpcController npc = hit.collider.gameObject.GetComponentInParent<NpcController>();
                bool active = npc.gameObject.Equals(this.gameObject);
                activated = active;
                _selectionOutline.SetActive(active);
                if (active)
                {
                    if (hit.collider.bounds.Contains(player.transform.position))
                    {
                        
                    }
                }
            }
            else if (Physics.Raycast(ray, out hit, 1000, _groundLayer))
            {
                activated = false;
                _selectionOutline.SetActive(false);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            
        }
    }

    IEnumerator RotateMe(Vector3 targetPosition, float speed) 
    {
        while (true)
        {
            Debug.Log("RotateMe");
            Vector3 dir = targetPosition - transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rot) < 1)
            {
                yield break;
            }
            yield return null;
        }
    }

    private void OnGUI()
    {
        if (show)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            screenPos.y = Screen.height - screenPos.y - 100;
            screenPos.x -= 100;
            GUI.Box(new Rect(screenPos.x, screenPos.y, 200, 25 ), _greeting + " " + _name);
        }
    }

    private void SetMaterial(bool active)
    {
        GetComponentInChildren<Renderer>().material = active ? _activeMaterial : _inActiveMaterial;
    }
}
