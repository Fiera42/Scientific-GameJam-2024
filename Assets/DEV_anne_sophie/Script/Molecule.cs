using System;
using System.Collections;
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
	[HideInInspector] public Transform[] waypoints;
	[HideInInspector] public float currentSpeedModifier = 1;

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
							AudioManager.instance.PlayWin();
							StartCoroutine(AnimateSprite());
							break;
						case "H":
							Destroy(gameObject);
							break;
						default:
							//loose
							Debug.Log("loose");
							AudioManager.instance.PlayLose();
							InteractionManager.Instance.isPlaying = false;
							StartCoroutine(Disapear());
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

			if (newMol != null)
			{
				Debug.Log(currentWaypointIndex + " / size : "+ (waypoints.Length - currentWaypointIndex));
				newMol.waypoints = new Transform[waypoints.Length-currentWaypointIndex];
				//waypoints.CopyTo(newMol.waypoints, currentWaypointIndex);
				for (int i = 0; i < waypoints.Length-currentWaypointIndex; i++)
				{
					Debug.Log(i);
					newMol.waypoints[i] = waypoints[i+currentWaypointIndex];
				}
				Instantiate(newMol, transform.position, newMol.transform.rotation, transform.parent);
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

	
	///// ANIMATIONS

	private IEnumerator Disapear(float shrinkSpeed = .1f, float rotationSpeed = 50f, float minimumScale = 0.01f)
	{
		do
		{
			transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
			transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

			if (transform.localScale.x < minimumScale)
			{
				Destroy(gameObject);
				yield break;
			}

			yield return null;
		} while (true);
	}



	IEnumerator AnimateSprite()
	{
		yield return JumpRoutine();
		yield return JumpRoutine();

		InteractionManager.Instance.windowWin.SetActive(true);

		yield return RotateRoutine();

	}

	IEnumerator JumpRoutine(float duration = 0.5f)
	{
		Vector3 startPos = transform.position;

		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float t = Mathf.Clamp01(elapsedTime / duration);
			transform.position = Vector3.Lerp(startPos, startPos, t - Mathf.Sin(t * Mathf.PI) * 0.5f);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.position = startPos;
	}

	IEnumerator RotateRoutine(float speed = 180f)
	{
		while (true)
		{
			transform.Rotate(Vector3.forward, speed * Time.deltaTime);
			yield return null;
		}
	}
}
