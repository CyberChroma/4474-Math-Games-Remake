using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Roly_Command : MonoBehaviour
{   
    public GameObject player;

    void start(){
        player = GameObject.FindGameObjectWithTag("Player");
    } // end start

    public abstract void CommandRoly();
} // end class
