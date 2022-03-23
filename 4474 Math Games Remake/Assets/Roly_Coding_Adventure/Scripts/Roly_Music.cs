using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roly_Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.transform);
    } // end method
} // end method
