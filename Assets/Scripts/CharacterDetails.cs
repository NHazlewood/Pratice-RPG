using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetails : MonoBehaviour {

    public int maxHealth = 100;
    public int maxStam = 100;
    public int maxEnergy = 100;

    bool hasTarget = false;
    Rigidbody characterBody;
    Vector3 movement;
    RaycastHit target;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;
    int health, stam, energy;

    void Awake()
    {
        health = maxHealth;
        stam = maxStam;
        energy = maxEnergy;
        //Debug.Log("Energy = " + energy + ", MaxEnergy =" + maxEnergy);
        anim = GetComponent<Animator>();
        characterBody = GetComponent<Rigidbody>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if(hasTarget && health > 0)
        {
            nav.SetDestination(target.point);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            anim.SetTrigger("IsDead");
        }
    }

    public void Move(RaycastHit newTarget)
    {
        target.point = newTarget.point;
        hasTarget = true;
        /*
        Debug.Log("Move to " + target.point.x + "," + target.point.y + "," + target.point.z);
        movement.Set(target.point.x, target.point.y, target.point.z);
        movement = movement.normalized * speed * Time.deltaTime;
        characterBody.MovePosition(transform.position + movement);*/
    }

    public void Attack(RaycastHit newTarget)
    {
        GameObject attTarget = GameObject.Find(newTarget.transform.name);
        CharacterDetails targetController = attTarget.GetComponent<CharacterDetails>();
        Rigidbody targetBody = attTarget.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(characterBody.position, targetBody.position);

        if(distance > 2)
        {
            Debug.Log("Target is out of attack range");
            return;
        }

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
}
