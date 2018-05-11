using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Util : MonoBehaviour {

	public static IEnumerator Rotateme(Transform transform, Vector3 targetPosition, float speed)
	{
		while (true)
        {
            Debug.Log("RotateMe");
            Vector3 dir = targetPosition - transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rot) < 5)
            {
                yield break;
            }
            yield return null;
        }
	}

    public static IEnumerator FlickerMe(Renderer[] renderers, float flickerRate)
    {
        bool visible = true;
        Debug.Log("Debugging");
        yield return new WaitForSeconds(1.8f);

        while (true)
        {
            Debug.Log("While");
            yield return new WaitForSeconds(flickerRate);
            visible = !visible;
            SetRenderers(renderers, visible);
        }
    }

    public static void SetRenderers(Renderer[] renderers, bool enabled)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = enabled;   
        }
    }

    public static void RotateToMouseDirection(Transform transform)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(hit.point, out navMeshHit, 100, 1))
            {
                transform.LookAt(navMeshHit.position);
            }
        }
    }
}
