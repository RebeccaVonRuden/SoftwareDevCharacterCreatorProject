using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Race", menuName = "Race")]
public class Race : ScriptableObject
{
    public string RaceName;
    public string RaceDBFinder;
    public bool hasDarkvision;
    
    
    //this determines the racial stat bonuses, ordered in str, dex, con, int, wis, cha
    public int[] racialMods = {0, 0, 0, 0, 0, 0};

    //this is for any skills a race might have, such as half elf giving two skills, elves having perception, and half orcs intimidation
    public int[] racialSkills =  { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    //the below sections are for appearance options
    public Sprite[] raceAppearanceF =  {null, null, null, null};
    public Sprite[] raceAppearanceM ={null, null, null, null};
    public Sprite[] hair ={null, null, null, null};
    public Sprite[] armorAppearance = {null, null, null, null, null, null, null, null, null, null, null, null}; //ordered by class in alphabetical order
    // this is for racial speed, usually only for those with more or less then 30 ft speed
    public int speed = 30;
    


}
