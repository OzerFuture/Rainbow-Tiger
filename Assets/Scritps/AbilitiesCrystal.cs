using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesCrystal : MonoBehaviour
{
    void Start()
    {
        Abilities.isShield = false;
        Abilities.isMagnit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if(other.gameObject.CompareTag("Magnit"))

             {
                Abilities.isMagnit = true;
                Invoke(nameof(Abilities.EndMagnit), 10f);
                Debug.Log("Magnit");
                Destroy(other.gameObject);
            }


            if (other.gameObject.CompareTag("Shield"))
            {
                Abilities.isShield = true;
                Debug.Log("Shield");
                Destroy(other.gameObject);
            }
        }
    }
}
