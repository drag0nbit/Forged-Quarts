using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string keyword;
    public Sprite icon;
    public string action;
    public float cooldown;
    public float knockback;
    public bool costHealth;
    public float cost;
    public float radius;
    public float heat;
    public GameObject spawn;
}