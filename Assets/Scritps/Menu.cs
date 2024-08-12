using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject player;
    public GameObject pause;
    public GameObject pausebutton;
    public TMP_Text shield;
    public TMP_Text magnit;
    public void Pause()

    {
        Time.timeScale = 0;
        pausemenu.SetActive(true);
        player.GetComponent<ColourChange>().enabled = false;
        //player.GetComponent<PlayerController>().enabled = false;
        pause.SetActive(false);
        player.GetComponent<Animator>().SetBool("Move", false);
    }
    public void Resume()

    {
        Time.timeScale = 1;
        pausemenu.SetActive(false);
        player.GetComponent<ColourChange>().enabled = true;
        //player.GetComponent<PlayerController>().enabled = true;
        pause.SetActive(true);
        player.GetComponent<Animator>().SetBool("Move", true);
    }

    public void Restart()

    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()

    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()

    {
        Levels.currentlevel++;
        if (Levels.currentlevel < 10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Levels.currentlevel >= 10)
        {
            SceneManager.LoadScene(Levels.currentlevel - 9);
        }


    }

    private void Update()
    {
        /*if (Tutorial.istutorial)
        {
            pausebutton.SetActive(false);
        }
        else
        {
            pausebutton.SetActive(true);
        }*/

        magnit.text = Shop.magnitcount.ToString();
        shield.text = Shop.shieldcount.ToString();
    }

}
