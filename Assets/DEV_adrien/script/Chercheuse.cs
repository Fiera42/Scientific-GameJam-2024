using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chercheuse : MonoBehaviour {

    [SerializeField] private Animator myAnymator;

    public void talk() {
        myAnymator.Play("Talking");
    }

    public void stopTalk() {
        myAnymator.Play("Idle");
    }
}
