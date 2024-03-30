using System.Collections;
using UnityEngine;

public class GeneratorMolecule : MonoBehaviour
{
	[SerializeField] private Molecule molecule;
    [SerializeField] private int nbrMol = 10;
	[SerializeField] private float speedInstance = 1; //en secondes
	[SerializeField] private float waitBeforeStart = 0; //en secondes
	[SerializeField] private Transform[] waypoints;
	[SerializeField] private Vector3 direction;

	private Coroutine launchRoutine;
	public void LaunchGenerator()
	{
		launchRoutine = StartCoroutine(Launch());
	}
	public void StopGenerator()
	{
		if (launchRoutine == null) return;

		StopCoroutine(launchRoutine);
		launchRoutine = null;
	}

	private IEnumerator Launch()
	{
		yield return new WaitForSeconds(waitBeforeStart);
		for (int i = 0; i < nbrMol; i++)
		{
			Molecule newMol = Instantiate(molecule, transform.position, molecule.transform.rotation);
			newMol.waypoints = waypoints;
            yield return new WaitForSeconds(speedInstance);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, .2f);
		Gizmos.color = Color.yellow;
		if (waypoints.Length>0)
		{
			Gizmos.DrawLine(transform.position, waypoints[0].position);
			for (int i = 0; i < waypoints.Length - 1; i++)
			{
				Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
			}
		}
	}
}
