using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isInputEnabled = true;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (!isInputEnabled)
        {
            return;
        }
    }
}
