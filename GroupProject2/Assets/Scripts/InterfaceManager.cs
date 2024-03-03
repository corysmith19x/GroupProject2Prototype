using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameObject pauseScreen;
    bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (isPaused == true) UnPause();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        Debug.Log("GamePaused");
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Debug.Log("GameUnPaused");
    }
}
