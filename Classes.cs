using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Class", menuName = "Class")]
public class Classes:ScriptableObject
{
    public List<int> priorityTable = new List<int>{1,2,3,4,5,6}; //the priority table determines which stat goes where
    //it is in the order of 1= str, 2= dex, 3 =con, 4=int, 5=wis, 6 = cha. these make stats be equal to the highest in that order
    //for example, if its 2,5,4,3,6,1 (the priority table for the rogue) the highest stat would be dexterity, and the lowest strength

    public int[] skills =  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    //this is to determine how all the skills display. a 0 means its blank, 1 means profichent, 2 is expertice, 3 is jack of all trades
    public bool[] savingThrows = {false, false, false, false, false,false};

    public int apparelIndex; //goes from 0-12. barbarian, bard, cleric, druid, fighter, monk, paladan, ranger, rogue,
                             //scorc, warlock, wizard
    public bool canSpellcast = false;   

    public Sprite[] itemOptions;

    //these are the options for hand items, such as a sword, book, staff, etc. they do not go in the characters hands 
    //but will instead go in the appropriate box with the 'left' and 'right' hand denominators

}
