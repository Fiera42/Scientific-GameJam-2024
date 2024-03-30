using System;
using UnityEngine;

public class Molecule : MonoBehaviour
{
	[SerializeField] private string nameMol;
	public bool Is(Molecule mol) { return mol.nameMol == nameMol; }

	public float speed = 5f;
	private int currentWaypointIndex = 0;
	[HideInInspector] public Transform[] waypoints;

	void Update()
	{
		if (currentWaypointIndex < waypoints.Length)
		{

			Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

			if (direction.magnitude < 0.1f)
			{
				currentWaypointIndex++;
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
			}
		}
	}

	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Molecule newMol = InteractionManager.Instance.GetResult(this, collision.GetComponent<Molecule>());
		if (newMol != null){
			waypoints.CopyTo(newMol.waypoints, currentWaypointIndex);
			Instantiate(newMol, transform.position, newMol.transform.rotation);
			Destroy(collision.gameObject);
			Destroy(this.gameObject);
		}
	}

	

}
