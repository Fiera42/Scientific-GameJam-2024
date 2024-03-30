using Unity.Burst.CompilerServices;
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
			//BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
			Collider2D[] overlap = Physics2D.OverlapPointAll((Vector2)transform.position);
            if (overlap != null)
			{
                foreach (Collider2D col in overlap)
                {
                    if (col == GetComponent<Collider2D>()) continue;

					col.transform.GetComponent<Chemain>().SetMaterial(this.typeOfMat);
                    break;
				}
			}
			//Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
            /*if (overlap.Length > 1)
            {
				Debug.Log(overlap[1].transform.gameObject);
				overlap[1].transform.GetComponent<Chemain>().SetMaterial(this.typeOfMat);
			}*/
			Destroy(gameObject);
        }
    }

}