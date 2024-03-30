using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrixManager : MonoBehaviour
{
    public static PrixManager _activePrixManager;
    public TextMeshProUGUI textToModify;
    float coutTotal = 0;
    // Start is called before the first frame update
    void Start()
    {
        _activePrixManager = this;
        UpdatePrix(0);
    }

    public void UpdatePrix(float valueToAdd)
    {
        coutTotal += valueToAdd;
        textToModify.text = coutTotal.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
