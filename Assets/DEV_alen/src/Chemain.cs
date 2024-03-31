using UnityEngine;

public class Chemain : MonoBehaviour
{
    string material;
    public MaterialPropertySO typeOfMat;
    public bool dragable = false;

    [HideInInspector] public bool isOnTable = false;
    private Sprite spriteAtStart;

	private void Start()
	{
		spriteAtStart = GetComponent<SpriteRenderer>().sprite;
	}

	public void SetMaterial(MaterialPropertySO mat)
    {
        typeOfMat = mat;
        if(mat == null) this.GetComponent<SpriteRenderer>().sprite = spriteAtStart;
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
        if (InteractionManager.Instance.isPlaying) return;

        if (dragable)
        {
            Instantiate(this.gameObject, this.transform.position, Quaternion.identity, transform.parent);
            PlaySound();
            isFollowingMouse = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        else if (isOnTable)
        {
            dragable = true;
			Instantiate(NewBehaviourScript.Instance.prefabTile, this.transform.position, Quaternion.identity, NewBehaviourScript.Instance.gameObject.transform);
			PlaySound();
			isFollowingMouse = true;
			transform.position = new Vector3(transform.position.x, transform.position.y, -1);
			PrixManager._activePrixManager.UpdatePrix(-this.typeOfMat.price);
		}

    }

    void OnMouseUp()
	{
		if (InteractionManager.Instance.isPlaying) return;

		isFollowingMouse = false;
        isOnTable = false;
        if (dragable)
        {
			Collider2D[] overlap = Physics2D.OverlapPointAll((Vector2)transform.position);
            if (overlap != null)
			{
                foreach (Collider2D col in overlap)
                {
                    if (col == GetComponent<Collider2D>()) continue;
                    if (col.transform.GetComponent<Chemain>() == null) continue;
                    if(col.transform.GetComponent<Chemain>().dragable)continue;

                    if (PrixManager._activePrixManager.UpdatePrix(this.typeOfMat.price))
                    {
                        PlaySound();
                        col.transform.GetComponent<Chemain>().SetMaterial(this.typeOfMat);
                        col.transform.GetComponent<Chemain>().isOnTable = true;
                    }
                    else
                    {
                        AudioManager.instance.PlaynoMoney();
                    }
                    break;
				}
			}
            Destroy(gameObject);
        }
    }

    public void OnMouseExit(){
        if(typeOfMat) this.GetComponent<SpriteRenderer>().sprite = typeOfMat.typeSprite;
    }

    public void OnMouseEnter(){
       if(typeOfMat) this.GetComponent<SpriteRenderer>().sprite = typeOfMat.selectSprite;
    }

    private void PlaySound()
    {
        switch (typeOfMat.name)
        {
            case "Fer":
                AudioManager.instance.PlayFer();
                break;
			case "AlFer":
				AudioManager.instance.PlayAlFer();
				break;
			case "Aluminium":
				AudioManager.instance.PlayAl();
				break;
			case "platine":
				AudioManager.instance.PlayPlatine();
				break;
		}
    }
}