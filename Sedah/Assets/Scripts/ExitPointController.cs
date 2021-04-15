using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointController : MonoBehaviour
{
    private LevelController levelController;
    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Stayed on collider");
            levelController.FinishLevel();
        }
    }
}
