using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSelectMat : MonoBehaviour
{

    public List<MaterialPropertySO> availableMaterial;

    public GameObject prefabTile;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tile = null;
        int i = 0;
        foreach (MaterialPropertySO m in availableMaterial)
        {
            tile = Instantiate(prefabTile, transform);
            tile.GetComponent<Chemain>().SetMaterial(m);
            tile.transform.position = new Vector2(transform.root.position.x, transform.root.position.y - i);
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
