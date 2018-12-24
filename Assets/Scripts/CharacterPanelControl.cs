using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelControl : MonoBehaviour {


    public GameObject character;

    GameObject characterPanel;
    Text health, stamina, energy;
    Slider healthSlider, stamSlider, energySlider;
    bool active;

    void Awake()
    {
        characterPanel = GameObject.Find("CharacterPanel");
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        stamina = GameObject.FindGameObjectWithTag("Stamina").GetComponent<Text>();
        energy = GameObject.FindGameObjectWithTag("Energy").GetComponent<Text>();

        //might want to just add Tags for them as well
        healthSlider = GameObject.FindObjectsOfType<Slider>()[2];
        stamSlider = GameObject.FindObjectsOfType<Slider>()[1];
        energySlider = GameObject.FindObjectsOfType<Slider>()[0];

        HidePanel();
        ShowPanel();
    }

    public void ShowPanel()
    {
        characterPanel.SetActive(true);
        active = true;
    }

    public void HidePanel()
    {
        characterPanel.SetActive(false);
        active = false;
    }

    public void Update()
    {
        if (!character || !active) { return; }
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
}
