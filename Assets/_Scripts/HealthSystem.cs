using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static int maxHealth = 6;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;


    // Update is called once per frame
    void Update()
    {
        foreach(Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for(int i = 0; i < maxHealth; i++)
        {
            hearts[i].sprite = fullHeart;
        }

    }
}
