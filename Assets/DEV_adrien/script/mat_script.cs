using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour {

    [SerializeField] private GameObject[] materialList;
    [SerializeField] private float[] speedInteractionList;
    private Dictionary <GameObject, float> speedModification;

    void Start() {
        if(materialList.Length != speedInteractionList.Length) {
            throw new ArgumentException("Material list is not the same lenght as speed interaction list");
        }

        speedModification = new Dictionary<GameObject, float>();

        for(int i = 0; i < materialList.Length; i++){
            speedModification.Add(materialList[i], speedInteractionList[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Destroy(gameObject);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
    }
}
