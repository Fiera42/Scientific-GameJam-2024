using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public List<GeneratorMolecule> allGenerator = new List<GeneratorMolecule>();
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    public GameObject windowWin;

    [HideInInspector] public bool isPlaying = false;

	public static InteractionManager Instance;
	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		startButton.onClick.AddListener(()=>StartAllGenerator());
		restartButton.onClick.AddListener(()=>RemoveAll());
	}

	public void StartAllGenerator()
    {
        isPlaying = true;

		foreach (var item in allGenerator)
        {
            item.LaunchGenerator();
        }
    }

    public void RemoveAll()
    {
        isPlaying = false;
        PrixManager._activePrixManager.Init();
		NewBehaviourScript.Instance.InitMaterial(); //grid
		foreach (var item in allGenerator)
		{
			item.StopGenerator();
		}
	}

	[Serializable] public struct MoleculeInteraction
    {
        public Molecule mol1;
        public Molecule mol2;
        public Molecule molResult;
        public AudioClip soundMerge;
    }

    public List<MoleculeInteraction> moleculeInteraction;

    public Molecule GetResult(Molecule mol1, Molecule mol2)
    {
        //Debug.Log(mol1 +" == "+(moleculeInteraction[0].mol1) + " " + mol2 + " == "+(moleculeInteraction[0].mol2));

		foreach (var interaction in moleculeInteraction)
        {
            if ((mol1.Is(interaction.mol1) && mol2.Is(interaction.mol2)) 
                /*|| (mol1.Is(interaction.mol2) && mol2.Is(interaction.mol1))*/)
            {
                if(interaction.soundMerge != null) AudioManager.instance.PlaySFX(interaction.soundMerge);
                return interaction.molResult;
            }
        }
        return null;
    }
}
