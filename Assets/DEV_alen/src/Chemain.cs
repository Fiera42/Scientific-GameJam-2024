using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Chemain : MonoBehaviour
{
    string material;
    public MaterialPropertySO typeOfMat;
    public bool dragable = false;



    public void SetMaterial(MaterialPropertySO mat)
    {
        typeOfMat = mat;
        this.GetComponent<SpriteRenderer>().sprite = mat.typeSprite;
    }

    public string getMaterial() { return material; }
    private bool isFollowingMouse;

    void FixedUpdate()
    {

        if (dragable && isFollowingMouse)
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPos = new Vector3(cursorPos.x, cursorPos.y, transform.position.z);

            transform.position = cursorPos;
        }
    }

    void OnMouseDown()
    {
        if (dragable)
        {
            Instantiate(this.gameObject, this.transform.position, Quaternion.identity);
            isFollowingMouse = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }

    }

    void OnMouseUp()
    {
        isFollowingMouse = false;
        if (dragable)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, new Vector3(0, 0, 5));
            Debug.DrawRay(transform.position, new Vector3(0, 0, 5), Color.blue,100f);
            if(Physics.Raycast(ray, out hit))
            {
                print(hit.transform.gameObject);
                hit.transform.GetComponent<Chemain>().SetMaterial(this.typeOfMat);
            }
            Destroy(gameObject);
        }
    }

}