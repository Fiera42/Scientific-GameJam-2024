using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialJam : MonoBehaviour {

    //------------------------ PARAMS
	[Header("Params")]
	public string nameMat;
	public float price;

	//------------------------ ATTRIBUTES
	private bool isFollowingMouse;

	void Update(){
		if(isFollowingMouse) {
			Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	cursorPos = new Vector3(cursorPos.x, cursorPos.y, transform.position.z);

			transform.position = cursorPos;
		}
	}

	void OnMouseDown() {
		isFollowingMouse = true;
	}

	void OnMouseUp() {
		isFollowingMouse = false;
	}
}
