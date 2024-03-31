using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrixManager : MonoBehaviour
{
    public static PrixManager _activePrixManager;
    public TextMeshProUGUI textToModify;
    private int coutTotal = 2000;
	public int coutStart = 2000;


	private void Awake()
	{
		_activePrixManager = this;
	}

	void Start()
    {
        Init();

	}

    public bool UpdatePrix(int valueToAdd)
    {
        coutTotal -= valueToAdd;
        if(coutTotal < 0 ) {return false;}
        
        textToModify.text = coutTotal.ToString() + " pièces";
        return true;
    }

    public void Init()
    {
        coutTotal = coutStart;
        UpdatePrix(0);
    }

}
