using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

public class ColourChange : MonoBehaviour
{

    private float runtime;
    public TMP_Text colourtext;
    private string Colour;
    private List<string> ColorList = new List<string>();
    private bool rightcolour;
    private string currentcolor;
    private GameObject currentblock;
    public GameObject colorui;
    public AudioSource SoundFX;
    void Start()
    {
        CancelInvoke("RunTime");
        InvokeRepeating("NewColor", 5, 3);
        Colour = "Yellow";
        ColorList.Add("Yellow");
        ColorList.Add("Green");
        ColorList.Add("Red");
        ColorList.Add("Purple");
        runtime = 0;
    }

    void Update()
    {
        ColorShow();
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentblock = hit.collider.gameObject;
            currentcolor = currentblock.tag;
        }


        if (currentcolor != Colour)
        {
            InvokeRepeating(nameof(RunTime), 2f, 2f);

            if (runtime > 2)
            {
                if (!Abilities.isShield)
                {
                    Lose();
                    if (transform.position.y <= 3)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        runtime = 0;
                    }
                }
                else
                {
                    Abilities.isShield = false;
                    runtime = 0;
                    CancelInvoke("RunTime");
                }

            }

        }

        else
        {
            CancelInvoke("RunTime");
            runtime = 0;
        }

        if (transform.position.y <= 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            runtime = 0;
        }

        if (currentblock.transform.position.y <= 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            runtime = 0;
        }
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)

        {
            Colour = other.gameObject.transform.tag;
            Debug.Log(Colour);
            ColorShow();
            CancelInvoke("NewColor");
            InvokeRepeating("NewColor", 5, 3);
            SoundFX.Play();
            Destroy(other.gameObject);
        }
    }


    public void NewColor()

    {
        // Tutorial.istutorial = false;
        if (!Tutorial.istutorial)
        {
            Colour = ColorList[Random.Range(0, 4)];
        }
        Debug.Log(Colour);
    }    
    

    public void RunTime()

    {
        runtime++;

    }

    public void ColorShow()
    {
        if (Colour == "Red")
        {
            colorui.GetComponent<CanvasRenderer>().SetColor(Color.red);
        }
        if (Colour == "Yellow")
        {
            colorui.GetComponent<CanvasRenderer>().SetColor(Color.yellow);
        }
        if (Colour == "Green")
        {
            colorui.GetComponent<CanvasRenderer>().SetColor(Color.green);
        }
        if (Colour == "Purple")
        {
            colorui.GetComponent<CanvasRenderer>().SetColor(Color.magenta);
        }
    }

    public void Lose()
    {
        GameObject current = currentblock;
        current.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Animator>().SetBool("IsFall", true);

        if (TryGetComponent<PlayerController>(out PlayerController playerController))
            GetComponent<PlayerController>().enabled = false;

        else if (TryGetComponent<PlayerMove>(out PlayerMove playerMove))
            GetComponent<PlayerMove>().enabled = false;

        //gameObject.GetComponent<Animator>().SetBool("Move", false); вернуть для другого режима

    }

}
