using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEventHandler : MonoBehaviour
{
    public void OnPlSelect() {
        SceneManager.LoadScene("BattleScene");
    }

    public void OnCpuSelect() {
        Debug.Log("you selected to play against the AI. Good Luck! You're gonna need it!");
    }

    public void OnExitSelect() {
        if(UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        Application.Quit();
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
