using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject[] characters; 
    private ComputerPlayer CP;

	// Use this for initialization
	void Start () {
        characters = GameObject.FindGameObjectsWithTag("Character");
        CP = gameObject.GetComponent<ComputerPlayer>();
    }	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndTurn()
    {
        CP.ComputerPlay();
        int n = characters.Length;
        for (int i = 0; i < n; ++i)
        {
            characters[i].GetComponent<CharacterDetails>().NewTurn();
        }
    }
}
