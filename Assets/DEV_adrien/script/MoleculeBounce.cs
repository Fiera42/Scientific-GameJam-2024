using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeBounce : MonoBehaviour {

    [SerializeField] private float jumpHeight;
    [SerializeField] private Molecule parent;
    [SerializeField] private ParticleSystem particles;
    private float phase = 0f;

    void Update() {
        //Phase += speed * deltaTime * pi
        //abs(sin(phase)) * amplitude 

        float speed = parent.speed * parent.currentSpeedModifier; // Current speed of the athom
        phase += (speed * Time.deltaTime) * Mathf.PI; // go forward in time for the SIN function

        if(phase > Mathf.PI) { // Reset the phase just to avoid useless big numbers and for potential OnJump events
            phase = 0;
            particles.Play();
        }

        float y = Mathf.Abs(Mathf.Sin(phase)) * jumpHeight; // Get the current height of the jump using a sin wave

        transform.localPosition = new Vector3(0, y, 0);
    }
}
