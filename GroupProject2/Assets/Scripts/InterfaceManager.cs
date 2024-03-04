using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    public GameObject pauseScreen;
	public GameObject arrow;
	public GameObject arrow1;
	
    public bool isPaused = false;
	public bool pauseStart = false;
	private bool m_isAxisInUse = false;
	
	
    // Update is called once per frame
    void Update()
    {
		Scene scene = SceneManager.GetActiveScene();
		
		if (Input.GetAxisRaw("Vertical") != 0)
		{
			if (m_isAxisInUse == false)
			{
				if (arrow.activeSelf == true)
				{
					arrow.SetActive(false);
					arrow1.SetActive(true);
				}
				else if (arrow1.activeSelf == true)
				{
					arrow.SetActive(true);
					arrow1.SetActive(false);
				}
				
				m_isAxisInUse = true;
			}
		}
		
        if (Input.GetButtonDown("Submit"))
        {
			if (scene.name == "LevelDesign")
			{
				if (isPaused == true) UnPause();
				else Pause();
			}
			else
			{
				if (arrow.activeSelf == true)
					SceneManager.LoadScene("LevelDesign");
				else if (arrow1.activeSelf == true)
					Application.Quit();
			}
        }
		
		if (Input.GetAxisRaw("Vertical") == 0) m_isAxisInUse = false;
		
		if (Input.GetButtonDown("Cancel")) Application.Quit();
    }

    public void Pause()
    {
		if (pauseStart == false)
		{
			pauseScreen.SetActive(true);
			arrow.SetActive(true);
			arrow1.SetActive(false);
			isPaused = true;
			pauseStart = true;
			Time.timeScale = 0;
		}
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
		pauseStart = false;
        Time.timeScale = 1;
    }

	public void gameOver()
    {
		SceneManager.LoadScene("GameOver");
	}
	public void winScene()
    {
		SceneManager.LoadScene("WinScreen");
    }
}
