using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strategy : MonoBehaviour {

    public string mode = "Zombie";
    private CharacterDetails self;
	
    // Use this for initialization
	void Start () {
        self = gameObject.GetComponent<CharacterDetails>();
	}
	
	// Update is called once per frame
	void Update () {
        if(self.Health() < 0)
        {
            mode = "Dead";
        }
	}

    public int Play(GameObject[] enemies)
    {
        if(mode == "Dead")
        {
            return 0;
        }
        if (mode == "Zombie")
        {
            ZombiePlay(enemies);
            return 1;
        }
        return 0;
    }

    private int ZombiePlay(GameObject[] enemies)
    {
        int n = enemies.Length;
        int ret = 1;
        //try to attack anything nearby
        for (int i = 0; i < n; ++i)
        {
            if (enemies[i] && ret>0)
            {
                ret = self.Attack(enemies[i]);
                if(ret == -1)
                {
                    mode = "Dead";
                    Debug.Log("Mode error a dead character wasn't maked as dead.");
                    return -1;
                }
                if(ret == -2)
                {
                    Debug.Log("Character doesn't have enough energy to attack.");
                    return 0;
                }
            }
            ret = 1;
        }
        //move to a "random" enemy
        int rnd = Random.Range(0, n - 1);
        ret = -1;
        for(int i = rnd; i < n; ++i)
        {
            if (enemies[i] && ret < 0)
            {
                self.Move(enemies[i].GetComponent<Rigidbody>());
                ret = i;
            }
        }
        if(ret < 0)
        {
            for (int i = 0; i < rnd; ++i)
            {
                if (enemies[i] && ret < 0)
                {
                    self.Move(enemies[i].GetComponent<Rigidbody>());
                    ret = i;
                }
            }
        }
        if(ret < 0)
        {
            Debug.Log("No valid enemies to move towards");
            return -1;
        }
        ret = 1;       
        //try to attack anything nearby
        for (int i = 0; i < n; ++i)
        {
            if (enemies[i] && ret > 0)
            {
                ret = self.Attack(enemies[i]);
                if (ret == -2)
                {
                    Debug.Log("Character doesn't have enough energy to attack.");
                    return 0 ;
                }
            }
            ret = 1;
        }
        return 0;
    }

}
