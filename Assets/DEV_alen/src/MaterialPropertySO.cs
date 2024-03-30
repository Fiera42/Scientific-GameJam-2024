using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewMaterial",menuName ="Metal")]
public class MaterialPropertySO : ScriptableObject
{
    public Sprite typeSprite;
    public string nameMetal;
    public float speed;
    public bool direction;
    public char directionChange;
}
