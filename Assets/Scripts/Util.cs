using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
