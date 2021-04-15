using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "New Environment", menuName = "Environment")]
public class Environment : MonoBehaviour
{
    public EnvironmentType environmentType;
    public List<GameObject> empty, road;
    public GameObject start, exit;
    public GameObject startSymbol, exitSymbol;

    public List<GameObject> obstacles;
    public List<GameObject> decorations;
    public List<GameObject> items;
}
