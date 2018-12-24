using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetails : MonoBehaviour {

    public int maxHealth = 100;
    public int maxStam = 100;
    public int maxEnergy = 100;

    Animator anim;
    int health, stam, energy;

    void Awake()
    {
        health = maxHealth;
        stam = maxStam;
        energy = maxEnergy;
        Debug.Log("Energy = " + energy + ", MaxEnergy =" + maxEnergy);
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            anim.SetTrigger("IsDead");
        }
    }

    public int Health()
    {
        return health;
    }

    public int Stam()
    {
        return stam;
    }

    public int Energy()
    {
        return energy;
    }
}
