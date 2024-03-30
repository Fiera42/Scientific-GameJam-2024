using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialJam : MonoBehaviour {

    //------------------------ PARAMS
	[Header("Params")]
	[SerializeField] private string nameMat;
	public string getName() {return nameMat;}
}
