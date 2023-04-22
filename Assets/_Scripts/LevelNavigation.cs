using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelNavigation : MonoBehaviour
{

    [SerializeField]
    int levelIndex;

    public void LoadScenes()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
