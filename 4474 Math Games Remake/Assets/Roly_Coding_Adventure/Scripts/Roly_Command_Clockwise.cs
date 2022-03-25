using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roly_Command_Clockwise : Roly_Command
{   
    void start(){
        player = GameObject.FindGameObjectWithTag("Player");
    } // end start

    override
    public void CommandRoly(){
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject.FindGameObjectWithTag("Player").GetComponent<Roly_Movement>().TurnClockwise();
    } // end method
} // end class
