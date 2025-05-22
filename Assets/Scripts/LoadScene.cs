using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGameplay() {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitGame() {
        // Menutup aplikasi (hanya bekerja di build, bukan di editor)
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}
