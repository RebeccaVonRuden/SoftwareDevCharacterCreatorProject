using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PlayerCharacter : MonoBehaviour
{
    private int _saveID;

    [SerializeField] private Race[] charRaceOptions; //list of races, should be in alphabetical order

    private Race _charRace;

    [SerializeField] private Classes[] charClassOptions; //list of classes, should be in alphabetical order

    private string _alignment;
    
    private Classes _playerClass;
    private ObtainNamesFromSQLite db;
   [SerializeField] private string charName = "bob";

    private int[] _playerSkills;
    private int[] _displayStats = {0,0,0,0,0,0};
    private int[] _playerStats = {0,0,0,0,0,0};
    private bool _charGender = true; //true = male false = female. meaning the first option for race appearance is male the second is female
    private int CharSpeed =30;
    [SerializeField] private GameObject questionaireStarter;
    [SerializeField] private List<TMP_Text> attributeObjects = new List<TMP_Text>();/*
    order goes as such: name, race, class, alignment, str, dex, con, int, wis, cha, skills(in alphabetical order), 
    str save, dex save, con save, int save, wis save, cha save, speed, can spellcast*/

    [SerializeField] private GameObject canSpellcast;

    [SerializeField] private List<GameObject> bodyObjects = new List<GameObject>();
    //order goes: body area, armor area, hand object left, hand object right, hair
    [SerializeField] private string[] alignmentOptions = new string[]
    {
        "Lawful Good", "Neutral Good", "Chaotic Good", "Neutral Good", "True Neutral", "Neutral Evil", "Chaotic Good",
        "Chaotic Neutral", "Chaotic Evil"
    };

    [SerializeField] private Classes testClass;
    [SerializeField] private Race testRace;
    
   
    void Start()
    {
        Random. InitState(System.DateTime.Today.Millisecond);
        db = new ObtainNamesFromSQLite();
        RunQuestionnaire();
        //charName = "bob";
        //_alignment = alignmentOptions[Random.Range(0, 8)];
       // _playerStats =GenerateCharacteristics();
        //_charRace = testRace;
       // _playerClass = testClass;
       // SetPlayerClassWoUpdate(testClass);
       // SetPlayerRaceWoUpdate(testRace);
       // SetSkills();
        
        
        UpdateCharacter();
    }

    public void SetAlignment(int num)
    {
        _alignment = alignmentOptions[num];
        attributeObjects[3].text = _alignment;
    }

    private void RunQuestionnaire()
    {
        questionaireStarter.SetActive(true);
    }
    

    public void SetGender()
    {
        _charGender = !_charGender;
        SetPlayerRaceWoUpdate(_charRace);
        SetPlayerClassWoUpdate(_playerClass);
        
    }

    private void SetSkills()
    {
        _playerSkills = _playerClass.skills;
        for (int i = 0; i < 18; i++)
        {
            if (_charRace.racialSkills[i] != 0 && _playerSkills[i] != 2)
            {
                _playerSkills[i] = _charRace.racialSkills[i];
            }
        }
    }

    private void SetBetween(int start, int end)
    {
        SetPlayerClass(charClassOptions[UnityEngine.Random.Range(start,end)]);
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
        CharSpeed = _charRace.speed +10;
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
        for (int i = 0; i < 3; i++)//this is done to remove low statistic rolls, such as all 3's or many stats below 10
        {
            List<int> ret = new List<int>(6);
            for (int iy = 0; iy < 6; iy++)
            {
                ret.Add(GenerateStat());  
            }

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
        List<int> dieRoll = new List<int>() {Random.Range(1, 7), Random.Range(1, 7), Random.Range(1, 7),
            Random.Range(1, 7)}; //generates the 4d6 dice used for stats, the lowest number is removed. the range is 1,7
        //as random.range is EXCLUSIVE to the final number, meaning its really a number between 1 and 6
        dieRoll.Sort(); //this orders them to get the highest 3 numbers in one spot. due to the function its lowest to highest
        ret += dieRoll[1];
        ret += dieRoll[2];
        ret += dieRoll[3];
        return ret;
    }

    public void SetPlayerRace(Race input)
    {
        int hold = Random.Range(0, 4); //getting random integer between 0 and 4 for determining which appearance is being used
         _charRace = input;
         bodyObjects[4].SetActive(true);
         if (_charGender)
         { 
             bodyObjects[0].GetComponent<Image>().sprite= _charRace.raceAppearanceM[hold]; //setting main body appearance for male
             bodyObjects[4].GetComponent<Image>().sprite = _charRace.hairM[hold]; //setting hair appearance for male
             if (bodyObjects[4].GetComponent<Image>().sprite == null)
             {
                 bodyObjects[4].SetActive(false);
             }
             bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceM[_playerClass.apparelIndex];
         }
        else
         {
             bodyObjects[0].GetComponent<Image>().sprite = _charRace.raceAppearanceF[hold];//setting main body appearance for female
             bodyObjects[4].GetComponent<Image>().sprite = _charRace.hairF[hold];//setting hair appearance for female
             if (bodyObjects[4].GetComponent<Image>().sprite == null)
             {
                 bodyObjects[4].SetActive(false);
             }
             bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceM[_playerClass.apparelIndex];
         }
        // //above commands are done here so that its not randomised each time you choose a new class, name, or alignment
        UpdateClassARacialStats();
        SetSkills();
        UpdateCharacter();
    }

    public void SetPlayerClass(Classes input)
    {
        _playerClass = input;
       if(_charGender)
       {
           bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceM[_playerClass.apparelIndex]; //setting aapparel
       }
       else
       {
           bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceF[_playerClass.apparelIndex];
        }
        bodyObjects[3].GetComponent<Image>().sprite = _playerClass.itemOptions[Random.Range(0, 2)]; //setting left hand object
        bodyObjects[2].GetComponent<Image>().sprite = _playerClass.itemOptions[Random.Range(3, 5)]; //setting right hand object
        //the above commands are done before the update function and seperate from it so that it is kept between the changed selections
        UpdateClassARacialStats();
        SetSkills();
        UpdateCharacter();
        
    }

    public string GetRandomName()
    {
        // charName = db.ChooseRandomFirstName(_charRace.RaceName, _charGender);
        // charName += " " + db.ChooseRandomLastName(_charRace.RaceName);
       // attributeObjects[0].text = charName;
       return "bobberson bobo"; //this is only temporary and will be removed when all code works
    }

    public void SetPlayerName(string input)
    {
        charName = input;
        attributeObjects[0].text =(input);
    }

    public void UpdateCharacter()
    {
        /*order goes as such: name, race, class, alignment, str, dex, con, int, wis, cha, skills(in alphabetical order), 
    str save, dex save, con save, int save, wis save, cha save, speed, can spellcast*/
        int index = 0;
        attributeObjects[index].text =(charName);
        index++;
        attributeObjects[index].text = (_charRace.name);
        index++;
        attributeObjects[index].text =(_playerClass.name);
        index++;
        attributeObjects[index].text =(_alignment);
        index++;
        for (int i = 0; i < 6; i++)
        {
           attributeObjects[index].text =(_displayStats[i].ToString());
           index++;
        }
        for (int i = 0; i < 18; i++)
        { //setting no prof, prof, expertice, or jack of all trades in all skills
            if (_playerSkills[i] == 0)
            {
                attributeObjects[index].text =("");
            }
            else if (_playerSkills[i] == 1)
            {
                attributeObjects[index].text =("P");
            }
            else if(_playerSkills[i]==2)
            {
                attributeObjects[index].text =("E");
            }
            else
            {
                attributeObjects[index].text =("J");
            }

            index++;
        }
        for (int i = 0; i < 6; i++)
        { //setting profichent or not profichent in saving throws
            if (_playerClass.savingThrows[i])
            {
                attributeObjects[index].text =("P");
            }
            else
            {
                attributeObjects[index].text =("");
            }

            index++;
        }
        attributeObjects[index].text =(CharSpeed.ToString());
        if (_playerClass.canSpellcast)
            canSpellcast.SetActive(true);
        else
            canSpellcast.SetActive(false);
        
    }
    

    public void GenerateRandomCharacter()
    {
        _alignment = alignmentOptions[Random.Range(0, 8)];
        _playerStats =GenerateCharacteristics();
       // _charRace = charRaceOptions[Random.Range(0, charRaceOptions.Length)];
       _charRace = charRaceOptions[9]; //for demo purposes only, remove afterwards 
       CharSpeed = _charRace.speed;
       _playerClass = charClassOptions[Random.Range(0, charClassOptions.Length)];
       
        SetPlayerClassWoUpdate(charClassOptions[Random.Range(0, charClassOptions.Length)]);
        SetPlayerRaceWoUpdate(charRaceOptions[Random.Range(0, charRaceOptions.Length)]);
        SetSkills();
       // charName = GetRandomName();
        
        
        UpdateCharacter();
    }
    public void GenerateRandomCharacter(Classes cls)
    {
        _alignment = alignmentOptions[Random.Range(0, 8)];
        _playerStats =GenerateCharacteristics();
        //_charRace = charRaceOptions[Random.Range(0, charRaceOptions.Length)];
        
        _charRace = charRaceOptions[9]; //for demo purposes only, remove afterwards
         CharSpeed = _charRace.speed; 
         _playerClass = cls;
        SetPlayerClassWoUpdate(_playerClass);
        SetPlayerRaceWoUpdate(_charRace);
        SetSkills();
       // charName = GetRandomName();
        
        
        UpdateCharacter();
    }
    public void GenerateRandomCharacter(int input)
    { //the int is to determine class. 1 is barbarian, 2 is bard, 3 is cleric, 4 is druid, 5 is fighter
        //6 is monk, 7 is paladan, 8 is ranger, 9 is rogue, 10 is scorc, 11 is warlock, 12 is wizard
        _alignment = alignmentOptions[Random.Range(0, 8)];
        _playerStats =GenerateCharacteristics();
        //_charRace = charRaceOptions[Random.Range(0, charRaceOptions.Length)];
        _charRace = charRaceOptions[9]; //for demo purposes only, remove afterwards
        CharSpeed = _charRace.speed;
            _playerClass = charClassOptions[Random.Range(0, charClassOptions.Length)];
        SetPlayerClassWoUpdate(_playerClass);
        SetPlayerRaceWoUpdate(_charRace);
        SetSkills();
        if (input == 1)
        {SetBetweenBarbarian();}
        if (input == 2)
        {SetBetweenBard();}
        if (input == 3)
        {SetBetweenCleric();}
        if (input == 4)
        {SetBetweenDruid();}
        if (input==5)
        {SetBetweenFighter();}
        if(input==6)
        {SetBetweenMonk();}
        if(input==7)
        {SetBetweenPaladan();}
        if(input==8)
        {SetBetweenRanger();}
        if(input==9)
        {SetBetweenRogue();}
        if(input==10)
        {SetBetweenScorc();}
        if(input==11)
        {SetBetweenWarlock();}
        if(input==12)
        {SetBetweenWizard();}
       // charName = GetRandomName();

    }
    private void SetPlayerClassWoUpdate(Classes input)
    {
        _playerClass = input;
        if (bodyObjects != null)
        {
            if (_charGender)
            {
                bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceM[_playerClass.apparelIndex]; //setting aapparel
            }
            else
            {
                bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceF[_playerClass.apparelIndex];
            }
        }

        bodyObjects[3].GetComponent<Image>().sprite = _playerClass.itemOptions[Random.Range(0, 2)]; //setting left hand object
        bodyObjects[2].GetComponent<Image>().sprite = _playerClass.itemOptions[Random.Range(3, 5)]; //setting right hand object
        //the above commands are done before the update function and seperate from it so that it is kept between the changed selections
       // UpdateStats();
        if (_charRace != null)
        {UpdateClassARacialStats();
        }
    }

    private void UpdateStats()
    {
        List<int> stats = new List<int>(_playerStats);
        stats.Sort();
        _playerStats[0] = stats[FindIndex(0)];
        _playerStats[1] = stats[FindIndex(1)];
        _playerStats[2] = stats[FindIndex(2)];
        _playerStats[3] = stats[FindIndex(3)];
        _playerStats[4] = stats[FindIndex(4)];
        _playerStats[5] = stats[FindIndex(5)];
    }
    public void SetPlayerRaceWoUpdate(Race input)
    {
       
        int hold = Random.Range(0, 4); //getting random integer between 0 and 4 for determining which appearance is being used
        _charRace = input;
        bodyObjects[4].SetActive(true);
        if (_charGender)
        { 
            bodyObjects[0].GetComponent<Image>().sprite= _charRace.raceAppearanceM[hold]; //setting main body appearance for male
            bodyObjects[4].GetComponent<Image>().sprite = _charRace.hairM[hold]; //setting hair appearance for male
            if (bodyObjects[4].GetComponent<Image>().sprite == null)
            {
                bodyObjects[4].SetActive(false);
            }
            bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceM[_playerClass.apparelIndex];
        }
        else
        {
            bodyObjects[0].GetComponent<Image>().sprite = _charRace.raceAppearanceF[hold];//setting main body appearance for female
            bodyObjects[4].GetComponent<Image>().sprite = _charRace.hairF[hold];//setting hair appearance for female
            if (bodyObjects[4].GetComponent<Image>().sprite == null)
            {
                bodyObjects[4].SetActive(false);
            }
            bodyObjects[1].GetComponent<Image>().sprite = _charRace.armorAppearanceM[_playerClass.apparelIndex];
        }
        // //above commands are done here so that its not randomised each time you choose a new class, name, or alignment
        UpdateClassARacialStats();
       
        //above commands are done here so that its not randomised each time you choose a new class, name, or alignment

    }

    private void UpdateClassARacialStats()
    {
        _displayStats[0] = _charRace.racialMods[0] + _playerStats[0];
        _displayStats[1] = _charRace.racialMods[1] + _playerStats[1];
        _displayStats[2] = _charRace.racialMods[2] + _playerStats[2];
        _displayStats[3] = _charRace.racialMods[3] + _playerStats[3];
        _displayStats[4] = _charRace.racialMods[4] + _playerStats[4];
        _displayStats[5] = _charRace.racialMods[5] + _playerStats[5];
    }

    private int FindIndex(int toBeFound)
    {
        //this is specifically sued for locating the index of one of th enumbers within the 'priority table' of the players class
        //and should not be used for anything else
        for (int i = 0; i < _playerStats.Length; i++)
        {
            if (_playerClass.priorityTable[i] == toBeFound)
            {
                return i;
            }
        }
        return 0;
    }
    
}
