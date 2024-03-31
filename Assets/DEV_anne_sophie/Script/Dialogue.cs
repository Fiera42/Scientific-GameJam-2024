using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class Dialogue : MonoBehaviour
{
	public TextMeshProUGUI textComponent;
	[SerializeField] private Chercheuse chercheuse;
	public string[] lines;
	public float textSpeed;

	private int index;

	public static Dialogue instance;
	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		StartDialogue();
	}

	// Update is called once per frame
	void Update()
	{
		if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
		{
			if (textComponent.text == lines[index]) //prochain texte
			{
				NextLine();
			}
			else //passer le texte
			{
				StopAllCoroutines();
				textComponent.text = lines[index];
			}
		}
	}

	void StartDialogue() {
		index = 0;
		textComponent.text = string.Empty;
		StartCoroutine(TypeLine());
		if(chercheuse != null) chercheuse.talk();
	}

	IEnumerator TypeLine()
	{
		// Type each character 1 by 1 
		foreach (char c in lines[index].ToCharArray())
		{
			textComponent.text += c;
			yield return new WaitForSeconds(textSpeed);
		}
		if (chercheuse != null) chercheuse.stopTalk();
	}

	void NextLine()
	{
		if (index < lines.Length - 1)
		{
			index++;
			textComponent.text = string.Empty;
			StartCoroutine(TypeLine());
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}