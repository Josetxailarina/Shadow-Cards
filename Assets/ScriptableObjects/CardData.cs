using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    public ElementType element;
    public int health;
    public int attack;
    public int cost;
    public bool isElementalCard;
}
