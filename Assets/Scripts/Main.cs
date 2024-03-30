using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    string heroName;

    void Awake() 
    {
        heroName = StringUtils.GenerateHeroFullName();
        Debug.Log($"> heroName = {heroName}");
        var firstNameLastName = heroName.Split(' ');
        // char firstChar = heroName[0];
        // Debug.Log($"> firstChar = {firstChar}");
        Debug.Log($"> first = {firstNameLastName[0]}");
        Debug.Log($"> last = {firstNameLastName[1]}");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
