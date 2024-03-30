using System.Collections;
using UnityEngine;

public class GeneratorMolecule : MonoBehaviour
{
	[SerializeField] private Molecule molecule;
    [SerializeField] private int nbrMol = 10;
	[SerializeField] private float speedInstance = 1; //en secondes
	[SerializeField] private Transform[] waypoints;

	private void Start()
	{
		StartCoroutine(Launch());
	}

	public IEnumerator Launch()
	{
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
		Gizmos.DrawSphere(transform.position, .5f);
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, waypoints[0].position);
		for (int i = 0; i < waypoints.Length - 1; i++)
		{
			Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
		}
	}
}
