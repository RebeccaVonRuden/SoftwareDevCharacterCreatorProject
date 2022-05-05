using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Race", menuName = "Race")]
public class Race : ScriptableObject
{
    public string RaceName;
    public bool hasDarkvision;
    
    //this determines the racial stat bonuses, ordered in str, dex, con, int, wis, cha
    public int[] racialMods = {0, 0, 0, 0, 0, 0};

    //this is for any skills a race might have, such as half elf giving two skills, elves having perception, and half orcs intimidation
    public int[] racialSkills =  { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public Image[] raceAppearanceF;
    public Image[] raceAppearanceM;
    public Image[] Hair;
    public Image[] armorAppearance; //ordered by class in alphabetical order
    public int speed = 30;


}
