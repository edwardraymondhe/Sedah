using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnvironmentSO", menuName = "EnvironmentSO")]
public class EnvironmentSO : ScriptableObject
{
    public EnvironmentType environmentType;
    public List<GameObject> empty;
    public GameObject start, exit;
    public GameObject startSymbol, exitSymbol;

    public List<GameObject> obstacles;
    public List<GameObject> decorations;
    public List<GameObject> items;
}
