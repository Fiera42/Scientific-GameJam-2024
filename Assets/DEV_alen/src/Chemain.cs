using UnityEngine;

public class Chemain : MonoBehaviour
{
    string material;
    public MaterialPropertySO typeOfMat;

    public Chemain(string m = "base")
    {
        material = m ;
    }

    public void SetMaterial(MaterialPropertySO mat)
    {
        typeOfMat = mat;
        this.GetComponent<SpriteRenderer>().sprite = mat.typeSprite;
    }

    public string getMaterial() { return material; }



}