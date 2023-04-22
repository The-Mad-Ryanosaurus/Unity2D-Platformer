using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetHealth : MonoBehaviour
{
    
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "DeathScreen")
        {
            Debug.Log("HELLO");
            HealthSystem.maxHealth = 6;
            Score.totalScore = 0;
        }
    }
}
