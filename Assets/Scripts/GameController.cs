using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject[] characters; 

	// Use this for initialization
	void Start () {
        characters = GameObject.FindGameObjectsWithTag("Character");
    }	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndTurn()
    {
        int n = characters.Length;
        for (int i = 0; i < n; ++i)
        {
            characters[i].GetComponent<CharacterDetails>().NewTurn();
        }
    }
}
