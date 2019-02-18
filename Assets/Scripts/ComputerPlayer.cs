using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour {

    GameObject[] allies;
    GameObject[] enemies;
    public int team = 1;


    void Awake () {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        allies = new GameObject[characters.Length];
        enemies = new GameObject[characters.Length];

        //splitting all characters between allies and enemies
        int n = characters.Length;
        int e = 0, a = 0;
        for (int i = 0; i < n; ++i)
        {
            if(characters[i].GetComponent<CharacterDetails>().team == team)
            {
                allies[a] = characters[i];
                ++a;
            }
            else
            {
                enemies[e] = characters[i];
                ++e;
            }
        }

    }
	
    public void ComputerPlay()
    {

        int n = allies.Length;
        for (int i = 0; i < n; ++i)
        {
            if (allies[i])
            {
                allies[i].GetComponent<Strategy>().Play(enemies);
            }
        }
    }

	void Update () {
		
	}
}
