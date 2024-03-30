using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Color color = Color.cyan;
    [SerializeField] private float size = 10;

	private void OnDrawGizmos()
	{
		Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, size);
	}
}
