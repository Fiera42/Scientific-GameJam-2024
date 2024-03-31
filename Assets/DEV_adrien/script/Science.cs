using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Science : MonoBehaviour {
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float speed = 1;
    private Vector3 initialPosition;
    private float phase = 0f;

    void Start(){
        initialPosition = transform.localPosition;
    }

    void Update() {
        
        //Phase += speed * deltaTime * pi
        //abs(sin(phase)) * amplitude 
        phase += (speed * Time.deltaTime) * Mathf.PI; // go forward in time for the SIN function

        if(phase > 2 * Mathf.PI) { // Reset the phase just to avoid useless big numbers and for potential OnJump events
            phase = 0;
        }

        float y = Mathf.Sin(phase) * amplitude; // Get the current height of the jump using a sin wave

        transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + y, initialPosition.z);
    }
}
