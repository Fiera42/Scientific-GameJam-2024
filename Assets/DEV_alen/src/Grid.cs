using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<List<Chemain>> grille;
    public int tailleX = 10;
    public int tailleY = 10;

    public GameObject prefabTile;
    private List<GameObject> createdObject;
    public List<MaterialPropertySO> availableMaterial;

    public static NewBehaviourScript Instance;
	private void Awake()
	{
		 Instance = this;
	}

	public string getMaterial(int x, int y)
    {
        return this.grille[x][y].getMaterial();
    }


    public void setMaterial(Chemain c, int x, int y)
    {
        this.grille[x][y] = c;
    }

    void Start()
    {
        createdObject = new List<GameObject>();
        this.grille = new List<List<Chemain>>();
        Dessiner();
    }

    public void InitMaterial()
    {
        for (int i = 0; i < this.grille.Count; i++)
        {
            for (int j = 0; j < this.grille[i].Count; j++)
            {
                if (this.grille[i][j] == null)
                {
					GameObject tile = Instantiate(prefabTile, transform);
					createdObject.Add(tile);
					tile.transform.position = new Vector2(transform.root.position.x + i, transform.root.position.y - j);

					grille[i][j] = tile.GetComponent<Chemain>();
				}
                else
                {
                    grille[i][j].SetMaterial(null);
                    grille[i][j].gameObject.GetComponent<SpriteRenderer>().sprite = prefabTile.GetComponent<SpriteRenderer>().sprite;
				}
            }
        }
    }

	public void Dessiner()
    {
        GameObject tile = null;

        for (int i = 0; i < tailleX; i++)
        {
            this.grille.Add(new List<Chemain>());
            for (int j = 0; j < tailleY; j++)
            {
                tile = Instantiate(prefabTile, transform);
                createdObject.Add(tile);
                tile.transform.position = new Vector2(transform.root.position.x + i, transform.root.position.y - j);

                this.grille[i].Add(tile.GetComponent<Chemain>());

            }
        }
    }
}
