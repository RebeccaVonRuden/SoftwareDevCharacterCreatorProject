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
    private int[] displayStats = new []{0,0,0,0,0,0};
    private int[] playerStats = {0,0,0,0,0,0};
    private bool charGender = false; //false = male true = female. meaning the first option for race appearance is male the second is female

    [SerializeField] private GameObject questionaireStarter;
    [SerializeField] private List<Text> attributeObjects;/*
    order goes as such: name, race, class, alignment, str, dex, con, int, wis, cha, skills(in alphabetical order), 
    str save, dex save, con save, int save, wis save, cha save, speed, can spellcast*/

    [SerializeField] private GameObject canSpellcast;

    [SerializeField] private List<Image> bodyObjects;
    //order goes: body area, armor area, hand object left, hand object right, hair
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
        SetBetween(17,20);
    }
    public void SetBetweenFighter()
    {
        SetBetween(21,26);
    }
    public void SetBetweenMonk()
    {
        charRace.speed += 10;
        SetBetween(27,31);
    }
    public void SetBetweenRanger()
    {
        SetBetween(37,40);
    }
    public void SetBetweenPaladan()
    {
        SetBetween(32,36);
    }
    public void SetBetweenRogue()
    {
        SetBetween(41,45);
    }
    public void SetBetweenScorc()
    {
        SetBetween(46,49);
    }
    public void SetBetweenWarlock()
    {
        SetBetween(50,54);
    }
    public void SetBetweenWizard()
    {
        SetBetween(55,63);
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
        int hold = new Random().Next(0, 1); //getting random integer between 0 and 1 for determining which appearance is being used
        charRace = input;
        if (charGender == false)
        {
            bodyObjects[0] = charRace.raceAppearanceM[hold]; //setting main body appearance for male
            bodyObjects[4] = charRace.Hair[hold]; //setting hair appearance for male
        }
        else
        {
            bodyObjects[0] = charRace.raceAppearanceF[hold];//setting main body appearance for female
            bodyObjects[4] = charRace.Hair[hold+2];//setting hair appearance for female, it is +2 since the hair is M1, M2, F1, F2
        }
        //above commands are done here so that its not randomised each time you choose a new class, name, or alignment
        UpdateClassARacialStats();
        updateCharacter();
    }

    public void setPlayerClass(Classes input)
    {
        playerClass = input;
        bodyObjects[1] = charRace.armorAppearance[playerClass.apparelIndex]; //setting aapparel
        bodyObjects[2] = playerClass.itemOptions[new Random().Next(0, 2)]; //setting left hand object
        bodyObjects[2] = playerClass.itemOptions[new Random().Next(3, 5)]; //setting right hand object
        //the above commands are done before the update function and seperate from it so that it is kept between the changed selections
        UpdateClassARacialStats();
        updateCharacter();
        
    }
    
    public void setPlayerName(string input)
    {
        charName = input;
        attributeObjects[0].text =(input);
    }

    public void updateCharacter()
    {
        /*order goes as such: name, race, class, alignment, str, dex, con, int, wis, cha, skills(in alphabetical order), 
    str save, dex save, con save, int save, wis save, cha save, speed, can spellcast*/
        int index = 0;
        attributeObjects[index].text =(charName);
        index++;
        attributeObjects[index].text =(charRace.RaceName);
        index++;
        attributeObjects[index].text =(playerClass.name);
        index++;
        attributeObjects[index].text =(alignment);
        index++;
        for (int i = 0; i < 8; i++)
        {
           attributeObjects[index].text =(displayStats[i].ToString());
           index++;
        }
        for (int i = 0; i < 18; i++)
        { //setting no prof, prof, expertice, or jack of all trades in all skills
            if (playerSkills[i] == 0)
            {
                attributeObjects[index].text =("");
            }
            else if (playerSkills[i] == 1)
            {
                attributeObjects[index].text =("P");
            }
            else if(playerSkills[i]==2)
            {
                attributeObjects[index].text =("E");
            }
            else
            {
                attributeObjects[index].text =("J");
            }

            index++;
        }
        for (int i = 0; i < 8; i++)
        { //setting profichent or not profichent in saving throws
            if (playerClass.savingThrows[i] == true)
            {
                attributeObjects[index].text =("P");
            }
            else
            {
                attributeObjects[index].text =("");
            }

            index++;
        }
        attributeObjects[index].text =(charRace.speed.ToString());
        if (playerClass.canSpellcast)
            canSpellcast.SetActive(true);
        else
            canSpellcast.SetActive(false);
        
    }
    

    public void GenerateRandomCharacter()
    {
        alignment = alignmentOptions[new Random().Next(0, 8)];
        playerStats =GenerateCharacteristics();
        setPlayerClassWoUpdate(charClassOptions[new Random().Next(0, charClassOptions.Length)]);
        setPlayerRaceWoUpdate(charRaceOptions[new Random().Next(0, charRaceOptions.Length)]);
        
        
        updateCharacter();
    }
    private void setPlayerClassWoUpdate(Classes input)
    {
        playerClass = input;
        bodyObjects[1] = charRace.armorAppearance[playerClass.apparelIndex]; //setting aapparel appearance
        bodyObjects[2] = playerClass.itemOptions[new Random().Next(0, 2)]; //setting left hand object
        bodyObjects[2] = playerClass.itemOptions[new Random().Next(3, 5)]; //setting right hand object
        //the above commands are done before the update function and seperate from it so that it is kept between the changed selections
        updateStats();
        if (charRace != null)
        {UpdateClassARacialStats();
        }
    }

    private void updateStats()
    {
        List<int> stats = new List<int>(playerStats);
        stats.Sort();
        playerStats[0] = stats[5-playerClass.priorityTable.IndexOf(0)];
        playerStats[1] = stats[5-playerClass.priorityTable.IndexOf(1)];
        playerStats[2] = stats[5-playerClass.priorityTable.IndexOf(2)];
        playerStats[3] = stats[5-playerClass.priorityTable.IndexOf(3)];
        playerStats[4] = stats[5-playerClass.priorityTable.IndexOf(4)];
        playerStats[5] = stats[5-playerClass.priorityTable.IndexOf(5)];
    }
    public void setPlayerRaceWoUpdate(Race input)
    {
        int hold = new Random().Next(0, 1); //getting random integer between 0 and 1 for determining which appearance is being used
        charRace = input;
        if (charGender == false)
        {
            bodyObjects[0] = charRace.raceAppearanceM[hold]; //setting main body appearance for male
            bodyObjects[4] = charRace.Hair[hold]; //setting hair appearance for male
        }
        else
        {
            bodyObjects[0] = charRace.raceAppearanceF[hold];//setting main body appearance for female
            bodyObjects[4] = charRace.Hair[hold+2];//setting hair appearance for female, it is +2 since the hair is M1, M2, F1, F2
        }

        UpdateClassARacialStats();
        //above commands are done here so that its not randomised each time you choose a new class, name, or alignment

    }

    private void UpdateClassARacialStats()
    {
        displayStats[0] = charRace.racialMods[0] + playerStats[0];
        displayStats[1] = charRace.racialMods[1] + playerStats[1];
        displayStats[2] = charRace.racialMods[2] + playerStats[2];
        displayStats[3] = charRace.racialMods[3] + playerStats[3];
        displayStats[4] = charRace.racialMods[4] + playerStats[4];
        displayStats[5] = charRace.racialMods[5] + playerStats[5];
    }
}
