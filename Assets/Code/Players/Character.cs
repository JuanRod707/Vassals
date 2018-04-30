using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

    public string FirstName;
    public Player House;
    public bool IsPatriarch;
    public Gender Gender;

    public Character Father;
    public Character Mother;
    public Character Spouse;
    public List<Character> Offspring;
    public List<Character> Siblings;
}
