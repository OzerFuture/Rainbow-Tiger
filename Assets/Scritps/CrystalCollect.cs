using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CrystalCollect : MonoBehaviour
{

    private float score;
    public TMP_Text scoretext;
    public TMP_Text resulttext;
    public TMP_Text levelname;
    public GameObject result;
    public GameObject main;
    public AudioSource SoundFX;
    public GameObject[] crystals;
    public int scoreToWin;

    void Update()
    {
        scoretext.text = score.ToString();

        if (Abilities.isMagnit == true && score < 3)

        {
            crystals = GameObject.FindGameObjectsWithTag("Crystal");



            foreach (GameObject crystal in crystals)
            {
                if ((transform.position - crystal.transform.position).magnitude < 5)
                {
                    Debug.Log(crystal.name);
                    crystal.transform.position = Vector3.MoveTowards(crystal.transform.position, transform.position, 2 * Time.deltaTime);
                }
            }

            
        }

        if(score == scoreToWin && scoreToWin < 10) //поміняти потім
        {
           result.SetActive(true);
           main.SetActive(false);
           GetComponent<ColourChange>().enabled = false;
           GetComponent<PlayerController>().enabled = false;
           levelname.text = "Level" + " " + Levels.currentlevel.ToString();
           Abilities.isMagnit = false;
           gameObject.GetComponent<Animator>().SetBool("Move", false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Crystal"))

        {
            score++;
            Destroy(other.gameObject);
            Abilities.isMagnit = false;
            SoundFX.Play();
            if (score == 3)
            {
                Shop.balance += 3;
            }
        }

    }



}
