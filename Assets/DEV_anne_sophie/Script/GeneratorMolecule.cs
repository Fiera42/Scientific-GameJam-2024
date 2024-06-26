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
		foreach (var molecules in GetComponentsInChildren<Molecule>())
		{
			Destroy(molecules.gameObject);
		}
		if (launchRoutine == null) return;

		StopCoroutine(launchRoutine);
		launchRoutine = null;
	}

	private IEnumerator Launch()
	{
		yield return new WaitForSeconds(waitBeforeStart);
		for (int i = 0; i < nbrMol; i++)
		{
			Molecule newMol = Instantiate(molecule, transform.position, molecule.transform.rotation, transform);
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

    public GameObject prefabTile;
    public void Start()
    {
		Transform previous = this.transform;
        GameObject tile = null;

		foreach (var i in waypoints)
		{
			float x = ((int)(previous.position.x - i.position.x));
			float y = ((int)(previous.position.y - i.position.y));

			

            tile = Instantiate(prefabTile);
			tile.transform.localScale = new Vector3(Mathf.Abs(x)+1, Mathf.Abs(y) + 1, 1);


			tile.transform.position = new Vector3(previous.position.x - (x/2), previous.position.y - (y/2), previous.position.z);

			previous = i;

        }
    }
}
