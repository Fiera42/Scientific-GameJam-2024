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
	public Vector3 direction;
	//------------------------ MATERIALS
	[Header("Interaction with materials")]
	[SerializeField] private MaterialPropertySO[] materialList;
    [SerializeField] private float[] speedInteractionList;
    private Dictionary <string, float> speedModifiers = new Dictionary<string, float>();

	//----------------------- PRIVATE
	//private int currentWaypointIndex = 0;
	//[HideInInspector] public Transform[] waypoints = new Transform[0];
	private float currentSpeedModifier = 1f;

	void changeDirection(char directionChange)
	{
		if(directionChange == 'd')
		{
            direction = new Vector3(direction.y, -direction.x, direction.z);
		}
		else
		{
			direction = new Vector3(-direction.y, direction.x, direction.z);

        }
    }

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
			}
			else
			{
				//transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * currentSpeedModifier * Time.deltaTime);
				transform.position = new Vector3(transform.position.x+(direction.x*speed*currentSpeedModifier*Time.deltaTime),transform.position.y+(direction.y*speed*currentSpeedModifier*Time.deltaTime), transform.position.z);
				if(transform.position.x<-20 || transform.position.x>20 || transform.position.y <-20 || transform.position.y > 20)
				{
					Debug.Log("destroy");
					Destroy(gameObject);
				}
			}
		}
	}

	

	private void OnTriggerEnter2D(Collider2D collision)
	{

		//Molecules
		if (collision.gameObject.layer == 7) {
			Molecule newMol = InteractionManager.Instance.GetResult(this, collision.GetComponent<Molecule>());

			if (newMol != null){
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
			if (collidedMat.typeOfMat.direction)
			{

				this.changeDirection(collidedMat.typeOfMat.directionChange);
			}
		}

    }

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.layer == 6) {
			currentSpeedModifier = 1;
		}
	}

	

}
