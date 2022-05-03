using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PlayerCharacter : MonoBehaviour
{
    private int saveID;

    [SerializeField] private Race[] charRaceOptions; //list of races, should be in alphabetical order

    private Race charRace;

    [SerializeField] private Classes[] charClassOptions; //list of classes, should be in alphabetical order

    private string alignment;
    
    private Classes playerClass;

    private string charName;

    private int[] playerSkills;

    private int[] playerStats;
    private bool charGender = false; //false = male true = female. meaning the first option for race appearance is male the second is female

    [SerializeField] private GameObject questionaireStarter;
    [SerializeField] private List<TextMeshPro> attributeObjects;/*
    order goes as such: name, race, class, alignment, str, dex, con, int, wis, cha, skills(in alphabetical order), 
    str save, dex save, con save, int save, wis save, cha save, speed, can spellcast*/

    [SerializeField] private GameObject canSpellcast;

    [SerializeField] private List<Image> bodyObjects;
    //order goes: body area, armor area, hand object left, hand object right
    [SerializeField] private string[] alignmentOptions = new string[]
    {
        "Lawful Good", "Neutral Good", "Chaotic Good", "Neutral Good", "True Neutral", "Neutral Evil", "Chaotic Good",
        "Chaotic Neutral", "Chaotic Evil"
    };
    
   
    void Start()
    {
        RunQuestionnaire();
    }

    private void RunQuestionnaire()
    {
        questionaireStarter.SetActive(true);
    }

    private void setSkills()
    {
        playerSkills = playerClass.skills;
        for (int i = 0; i < 18; i++)
        {
            if (charRace.racialSkills[i] != 0 && playerSkills[i] != 2)
            {
                playerSkills[i] = charRace.racialSkills[i];
            }
        }
    }

    private void SetBetween(int start, int end)
    {
        setPlayerClass(charClassOptions[new Random().Next(start,end)]);
    }

    public void SetBetweenBarbarian()
    {
        SetBetween(0,3);
    }
    public void SetBetweenBard()
    {
        SetBetween(4,7);
    }
    public void SetBetweenCleric()
    {
        SetBetween(8,16);
    }
    public void SetBetweenDruid()
    {
        SetBetween(17,21);
    }
    public void SetBetweenFighter()
    {
        SetBetween(22,26);
    }
    public void SetBetweenMonk()
    {
        SetBetween(27,31);
    }
    public void SetBetweenRanger()
    {
        SetBetween(37,39);
    }
    public void SetBetweenPaladan()
    {
        SetBetween(32,36);
    }
    public void SetBetweenRogue()
    {
        SetBetween(40,44);
    }
    public void SetBetweenScorc()
    {
        SetBetween(45,48);
    }
    public void SetBetweenWarlock()
    {
        SetBetween(49,53);
    }
    public void SetBetweenWizard()
    {
        SetBetween(54,63);
    }
    
    

    private int[] GenerateCharacteristics()
    {
        
        int[] bestOfAttempts =  {0,0,0,0,0,0};
        int bestOfAttemptsTotal = 0;
        for (int i = 0; i < 5; i++)//this is done to remove low statistic rolls, such as all 3's or many stats below 10
        {
            List<int> ret = new List<int>() {GenerateStat(),GenerateStat(),GenerateStat(),GenerateStat(),GenerateStat(),GenerateStat() };
            int total = ret[0] + ret[1] + ret[2] + ret[3] + ret[4] + ret[5];
            if (bestOfAttemptsTotal < total) 
            {
                bestOfAttempts = ret.ToArray();
                bestOfAttemptsTotal = total;
            }

        }

        return bestOfAttempts;
    }

    private int GenerateStat()
    {
        int ret = 0;
        List<int> dieRoll = new List<int>() {new Random().Next(1, 6), new Random().Next(1, 6), new Random().Next(1, 6),
            new Random().Next(1, 6)}; //generates the 4d6 dice used for stats, the lowest number is removed
        dieRoll.Sort(); //this orders them to get the highest 3 numbers in one spot. due to the function its lowest to highest
        ret += dieRoll[1];
        ret += dieRoll[2];
        ret += dieRoll[3];
        return ret;
    }

    public void setPlayerRace(Race input)
    {
        
        charRace = input;
        if (charGender==false)
        bodyObjects[0] = charRace.raceAppearance[0];
        updateCharacter();
    }

    public void setPlayerClass(Classes input)
    {
        playerClass = input;
        bodyObjects[1] = charRace.armorAppearance[playerClass.armorType]; //setting armor
        bodyObjects[2] = playerClass.itemOptions[new Random().Next(0, 2)]; //setting left hand object
        bodyObjects[2] = playerClass.itemOptions[new Random().Next(3, 5)]; //setting right hand object
        updateCharacter();
    }

    public void setPlayerName(string input)
    {
        charName = input;
        attributeObjects[0].SetText(input);
    }

    public void updateCharacter()
    {
        /*order goes as such: name, race, class, alignment, str, dex, con, int, wis, cha, skills(in alphabetical order), 
    str save, dex save, con save, int save, wis save, cha save, speed, can spellcast*/
        int index = 0;
        attributeObjects[index].SetText(charName);
        index++;
        attributeObjects[index].SetText(charRace.RaceName);
        index++;
        attributeObjects[index].SetText(playerClass.name);
        index++;
        attributeObjects[index].SetText(alignment);
        index++;
        for (int i = 0; i < 8; i++)
        {
           attributeObjects[index].SetText(playerStats[i].ToString());
           index++;
        }
        for (int i = 0; i < 18; i++)
        { //setting no prof, prof, expertice, or jack of all trades in all skills
            if (playerSkills[i] == 0)
            {
                attributeObjects[index].SetText("");
            }
            else if (playerSkills[i] == 1)
            {
                attributeObjects[index].SetText("P");
            }
            else if(playerSkills[i]==2)
            {
                attributeObjects[index].SetText("E");
            }
            else
            {
                attributeObjects[index].SetText("J");
            }

            index++;
        }
        for (int i = 0; i < 8; i++)
        { //setting profichent or not profichent in saving throws
            if (playerClass.savingThrows[i] == true)
            {
                attributeObjects[index].SetText("P");
            }
            else
            {
                attributeObjects[index].SetText("");
            }

            index++;
        }
        attributeObjects[index].SetText(charRace.speed.ToString());
        if (playerClass.canSpellcast)
            canSpellcast.SetActive(true);
        else
            canSpellcast.SetActive(false);
        
    }
    

    public void GenerateRandomCharacter()
    {
        alignment = alignmentOptions[new Random().Next(0, 8)];
        
        playerClass = charClassOptions[new Random().Next(0, charClassOptions.Length)];
        charRace= charRaceOptions[new Random().Next(0, charRaceOptions.Length)];
        int[] stats = GenerateCharacteristics();
        playerStats[0] = stats[playerClass.priorityTable[0]]; 
        playerStats[1] = stats[playerClass.priorityTable[1]]; 
        playerStats[2] = stats[playerClass.priorityTable[2]]; 
        playerStats[3] = stats[playerClass.priorityTable[3]]; 
        playerStats[4] = stats[playerClass.priorityTable[4]]; 
        playerStats[5] = stats[playerClass.priorityTable[5]];
        updateCharacter();
    }




}
