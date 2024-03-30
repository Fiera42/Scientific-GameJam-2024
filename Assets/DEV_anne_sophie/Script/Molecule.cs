using System;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{

	//------------------------ PARAMS
	[Header("Params")]
	[SerializeField] private string nameMol; //H3COH = methanol
	public bool Is(Molecule mol) { return mol.nameMol == nameMol; }
	public float speed = 5f;
	//------------------------ MATERIALS
	[Header("Interaction with materials")]
	[SerializeField] private MaterialPropertySO[] materialList;
    [SerializeField] private float[] speedInteractionList;
    private Dictionary <string, float> speedModifiers = new Dictionary<string, float>();

	//----------------------- PRIVATE
	private int currentWaypointIndex = 0;
	[HideInInspector] public Transform[] waypoints = new Transform[0];
	private float currentSpeedModifier = 1;

	void Start() {
        if(materialList.Length != speedInteractionList.Length) {
            throw new ArgumentException("MaterialJam list is not the same lenght as speed interaction list");
        }

        for(int i = 0; i < materialList.Length; i++){
            speedModifiers.Add(materialList[i].nameMetal, speedInteractionList[i]);
        }
    }

	void Update()
	{
		if (waypoints.Length>0 && currentWaypointIndex < waypoints.Length)
		{
			Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

			if (direction.magnitude < 0.1f)
			{
				currentWaypointIndex++;
				if (currentWaypointIndex >= waypoints.Length)
				{
					switch (nameMol) {//end game, verify if it's the right molecule
						case "H3COH":
							//win
							Debug.Log("win");
							break;
						case "H":
							Destroy(gameObject);
							break;
						default:
							//loose
							Debug.Log("loose");
							break;
					}
				}
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * currentSpeedModifier * Time.deltaTime);
			}
		}
	}

	

	private void OnTriggerEnter2D(Collider2D collision)
	{

		//Molecules
		if (collision.gameObject.layer == 7) {
			Molecule newMol = InteractionManager.Instance.GetResult(this, collision.GetComponent<Molecule>());

			if (newMol != null){
				newMol.waypoints = waypoints;
				Instantiate(newMol, transform.position, newMol.transform.rotation);
				Destroy(collision.gameObject);
				Destroy(this.gameObject);
			}
		}

		//Material
		if (collision.gameObject.layer == 6) {
			Chemain collidedMat = collision.gameObject.GetComponent<Chemain>();
			if (collidedMat.typeOfMat == null) return;
			if (speedModifiers.ContainsKey(collidedMat.typeOfMat.nameMetal)) {
				currentSpeedModifier = speedModifiers[collidedMat.typeOfMat.nameMetal];
			}
		}

    }

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.layer == 6) {
			currentSpeedModifier = 1;
		}
	}

	

}
