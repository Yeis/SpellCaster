using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSettings {

    private int numberOfEnemies;
    private int simultaneousEnemies;
    private List<GameObject> typesOfEnemies;

    public int SimultaneousEnemies { get => simultaneousEnemies; set => simultaneousEnemies = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public List<GameObject> TypesOfEnemies { get => typesOfEnemies; set => typesOfEnemies = value; }

    public BattleSettings(int numberOfEnemies, int simultaneousEnemies) {
        NumberOfEnemies = numberOfEnemies;
        SimultaneousEnemies = simultaneousEnemies;
    }   
}