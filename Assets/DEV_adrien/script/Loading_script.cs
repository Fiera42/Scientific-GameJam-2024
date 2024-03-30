using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_script : MonoBehaviour
{
    //script used by the black transitions
    private float initPosition;
    public Rigidbody2D myBody;

    void Start() {
        myBody.velocity = new Vector2(-1, 0);
        initPosition = transform.position.x;
    }

    void Update(){
        myBody.velocity += new Vector2(myBody.velocity.x * 10 * Time.deltaTime, 0);
        if(transform.position.x - initPosition < -25) myBody.velocity = Vector2.zero;
    }
}
