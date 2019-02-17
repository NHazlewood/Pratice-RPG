using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetails : MonoBehaviour {

    public int maxHealth = 100;
    public int maxStam = 100;
    public int maxEnergy = 100;
    public int attackCost = 10;
    public int stamRegen = 30;
    public int healthRegen = 0;


    bool hasTarget = false;
    Rigidbody characterBody;
    Vector3 movement;
    RaycastHit target;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;
    int health, stam, energy, team = 0, range = 2;

    void Awake()
    {
        health = maxHealth;
        stam = maxStam;
        energy = maxEnergy;
        anim = GetComponent<Animator>();
        characterBody = GetComponent<Rigidbody>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if(hasTarget && health > 0)
        {
            nav.SetDestination(target.point);
            if(Vector3.Distance(characterBody.position, target.point) < 1f)
            {
                anim.SetBool("IsWalking", false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(health <= 0)
        {
            return;
        }
        health -= damage;
        if(health <= 0)
        {
            anim.SetTrigger("IsDead");
        }
        else
        {
            anim.SetTrigger("IsHit");
        }
    }

    public void Move(RaycastHit newTarget)
    {
        target.point = newTarget.point;
        hasTarget = true;
        anim.SetBool("IsWalking", true);
    }

    public void Attack(RaycastHit newTarget)
    {
        if(health <= 0)
        {
            Debug.Log("Dead Characters can't attack");
            return;
        }
        if(energy < attackCost)
        {
            Debug.Log("This character does not have enough energy to attack.");
            return;
        }
        GameObject attTarget = GameObject.Find(newTarget.transform.name);
        CharacterDetails targetController = attTarget.GetComponent<CharacterDetails>();
        Rigidbody targetBody = attTarget.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(characterBody.position, targetBody.position);

        if(distance > range)
        {
            Debug.Log("Target is out of attack range");
            return;
        }

        energy -= attackCost;
        anim.SetTrigger("IsAttacking");
        targetController.TakeDamage(2);
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

    public void NewTurn()
    {
        //recovering health
        health += healthRegen;
        health = Mathf.Min(health, maxHealth);
        //recovering stamina
        stam += stamRegen;
        stam = Mathf.Min(stam, maxStam);
        //converting stamina to energy
        stam += energy;
        energy = Mathf.Min(stam, maxEnergy);
        stam -= Mathf.Min(stam, maxEnergy);
        stam = Mathf.Min(stam, maxStam);
    }
}
