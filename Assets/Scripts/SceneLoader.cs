using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader //jednoduchý manažer scén
{
    public static void LoadScene(string sceneName){
        Load(sceneName);
    }
    public static void LoadScene(int level){
        Load("Level" + level);
    }
    private static void Load(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
