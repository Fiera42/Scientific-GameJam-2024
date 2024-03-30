using UnityEngine;

public class Chemain : MonoBehaviour
{
    string material;
    public MaterialPropertySO typeOfMat;
    public bool dragable = false;

    public void SetMaterial(MaterialPropertySO mat)
    {
        typeOfMat = mat;
        if(mat == null) this.GetComponent<SpriteRenderer>().sprite = null;
		else this.GetComponent<SpriteRenderer>().sprite = mat.typeSprite;
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
                    if (col.transform.GetComponent<Chemain>() == null) continue;

					col.transform.GetComponent<Chemain>().SetMaterial(this.typeOfMat);
                    break;
				}
			}
			Destroy(gameObject);
        }
    }

}