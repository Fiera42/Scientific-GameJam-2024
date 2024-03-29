using UnityEngine;

public class MoleculeMovement : MonoBehaviour
{

    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(obj);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.one*Time.deltaTime);
    }
}
