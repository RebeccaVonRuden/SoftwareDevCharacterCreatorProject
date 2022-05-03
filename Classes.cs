using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Class", menuName = "Class")]
public class Classes:ScriptableObject
{
    public int[] priorityTable = {1,2,3,4,5,6}; //the priority table determines which stat goes where
    //it is in the order of 1= str, 2= dex, 3 =con, 4=int, 5=wis, 6 = cha. these make stats be equal to the highest in that order
    //for example, if its 2,5,4,3,6,1 (the priority table for the rogue) the highest stat would be dexterity, and the lowest strength

    public int[] skills =  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    //this is to determine how all the skills display. a 0 means its blank, 1 means profichent, 2 is expertice, 3 is jack of all trades
    public bool[] savingThrows = {false, false, false, false, false,false};

    public int armorType; //this determines which of the 4 armor sets will be used. armor types ar 'clothed',
                             //'light', 'medium', and 'heavy'. with the numbers ranging from 0 to 3. this will be used
                             // in the player class
    public bool canSpellcast = false;

    public Image[] itemOptions;
    //these are the options for hand items, such as a sword, book, staff, etc. they do not go in the characters hands 
    //but will instead go in the appropriate box with the 'left' and 'right' hand denominators

}
