using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class NewBehaviourScript : MonoBehaviour
{

    private List<List<Chemain>> grille;
    private int tailleX;
    private int tailleY;

    public GameObject cr;

    private bool once = true;

    public string getMaterial(int x, int y)
    {
        return this.grille[x][y].getMaterial();

    }


    void Start()
    {
        this.grille = new List<List<Chemain>>();
        tailleX = 10;
        tailleY = 10;
        GenerateGrid(10, 10);

    }

    private void GenerateGrid(int x, int y)
    {

        for (int i = 0; i < x; i++)
        {
            this.grille.Add(new List<Chemain>());
            for (int j = 0; j < y; j++)
            {
                this.grille[i].Add(new Chemain());
            }
        }
    }

    void Update()
    {
        if (once) {
            Dessiner();
            once = false;
        }

    }


    public void Dessiner()
    {
        GameObject referenceTile = Instantiate(cr);

        for (int i = 0; i < tailleX; i++)
        {
            for (int j = 0; j < tailleY; j++)
            {
                switch (getMaterial(i, j))
                {
                    case "base":
                        GameObject tile = (GameObject)Instantiate(referenceTile, transform);
                        Debug.Log(transform.root.position);

                        tile.transform.position = new Vector2(transform.root.position.x +i, transform.root.position.y - j);
                        break;

                    default:
                        Debug.Log("pas trouve");
                        break;
                }
            }
        }

        Destroy(referenceTile);
    }
}
