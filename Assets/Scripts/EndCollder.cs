using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") { 
            string team = other.gameObject.GetComponent<PlayerScript>().teamName;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
