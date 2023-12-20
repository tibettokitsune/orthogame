using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instatnce;
    public Antigravity Antigravity;
    public GameObject Player;
    void Awake()
    {
        if (instatnce == null) instatnce = this;
        else Destroy(this);

        DontDestroyOnLoad(gameObject);
        Antigravity.Initialize();
    }
}
