using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum element
{
    none,
    fire,
    water,
    wind,

}
public class CardStats : MonoBehaviour
{
    public element element1;
    public element element2;
    public int health;
    public int attack;

  public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void TakeFire()
    {
        if (element1 == element.wind|| element2 == element.wind)
        {
            health -= 2;
            print("fire damage");
        }
    }
    public void TakeWater()
    {
        if (element1 == element.fire || element2 == element.fire)
        {
            health -= 2;
            print("water damage");

        }
    }
    public void TakeWind()
    {
        if (element1 == element.water || element2 == element.water)
        {
            health -= 2;
            print("wind damage");

        }
    }
}
