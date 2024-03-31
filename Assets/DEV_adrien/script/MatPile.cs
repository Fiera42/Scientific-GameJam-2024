using UnityEngine;
using TMPro;

public class MatPile : MonoBehaviour {

    [SerializeField] private GameObject myMaterial;
    [SerializeField] private TextMeshProUGUI priceDisplay;

    // Start is called before the first frame update
    void Start() {
        priceDisplay.text = myMaterial.GetComponent<MaterialJam>().price + "";

        generateNewMaterial();
    }

    public void generateNewMaterial() {
        Instantiate(myMaterial, transform);
    }
}
