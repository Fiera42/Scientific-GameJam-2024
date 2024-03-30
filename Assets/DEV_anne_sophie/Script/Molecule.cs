using System;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{

	//------------------------ PARAMS
	[Header("Params")]
	[SerializeField] private string nameMol;
	public bool Is(Molecule mol) { return mol.nameMol == nameMol; }
	public float speed = 5f;

	//------------------------ MATERIALS
	[Header("Interaction with materials")]
	[SerializeField] private MaterialJam[] materialList;
    [SerializeField] private float[] speedInteractionList;
    private Dictionary <string, float> speedModifiers;

	//----------------------- PRIVATE
	private int currentWaypointIndex = 0;
	[HideInInspector] public Transform[] waypoints;
	private float currentSpeedModifier = 1f;

	void Start() {
        if(materialList.Length != speedInteractionList.Length) {
            throw new ArgumentException("MaterialJam list is not the same lenght as speed interaction list");
        }

        speedModifiers = new Dictionary<string, float>();

        for(int i = 0; i < materialList.Length; i++){
            speedModifiers.Add(materialList[i].getName(), speedInteractionList[i]);
        }
    }

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
				waypoints.CopyTo(newMol.waypoints, currentWaypointIndex);
				Instantiate(newMol, transform.position, newMol.transform.rotation);
				Destroy(collision.gameObject);
				Destroy(this.gameObject);
			}
		}

		//Material
		if (collision.gameObject.layer == 6) {
			
			MaterialJam collidedMat = collision.gameObject.GetComponent<MaterialJam>();
			string collidedMatName = collidedMat.getName();

			if (speedModifiers.ContainsKey(collidedMatName)) {
				currentSpeedModifier = speedModifiers[collidedMatName];
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.layer == 6) {
			currentSpeedModifier = 1;
		}
	}

	

}
