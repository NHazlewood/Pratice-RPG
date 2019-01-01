using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelControl : MonoBehaviour {


    public GameObject character;

    GameObject statusPanel;
    Text health, stamina, energy;
    Slider healthSlider, stamSlider, energySlider;
    int terrainMask;

    void Awake()
    {
        terrainMask = LayerMask.GetMask("Terrain");
        statusPanel = GameObject.Find("StatusPanel");
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        stamina = GameObject.FindGameObjectWithTag("Stamina").GetComponent<Text>();
        energy = GameObject.FindGameObjectWithTag("Energy").GetComponent<Text>();

        //might want to just add Tags for them as well
        healthSlider = GameObject.FindObjectsOfType<Slider>()[2];
        stamSlider = GameObject.FindObjectsOfType<Slider>()[1];
        energySlider = GameObject.FindObjectsOfType<Slider>()[0];

        HidePanel();

    }

    public void ShowPanel()
    {
        statusPanel.SetActive(true);
    }

    public void HidePanel()
    {
        statusPanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            LeftClick();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            RightClick();
        }

        if (!character)
        {
            HidePanel();
            return;
        }

        ShowPanel();
        UpdatePanel();
        
    }

    void UpdatePanel()
    {
        CharacterDetails detail = character.GetComponent<CharacterDetails>();
        health.text = detail.Health() + "/" + detail.maxHealth;
        stamina.text = detail.Stam() + "/" + detail.maxStam;
        energy.text = detail.Energy() + "/" + detail.maxEnergy;

        healthSlider.maxValue = detail.maxHealth;
        healthSlider.value = detail.Health();

        stamSlider.maxValue = detail.maxStam;
        stamSlider.value = detail.Stam();

        energySlider.maxValue = detail.maxEnergy;
        energySlider.value = detail.Energy();
    }

    void LeftClick()
    {
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Character")
            {
                character = GameObject.Find(hit.transform.name);
                Debug.Log("We hit " + hit.transform.name);
            }
            else
            {
                character = null;
                Debug.Log("We hit nothing.");
            }
         }
    }

    void RightClick()
    {
        if (!character)
        {
            Debug.Log("No character selected.");
            return;
        }

        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f , terrainMask))
        {
            CharacterDetails controller = character.GetComponent<CharacterDetails>();

            Debug.Log("Move to " + hit.point.x + "," +
            hit.point.y + "," +
            hit.point.z);
            controller.Move(hit); 
        }
    }
}
