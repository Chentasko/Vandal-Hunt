using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text thisText;
    void Start()
    {
        thisText = GetComponent<TMP_Text>();
    }

    public void Won()
    {
        thisText.text = "Vandal Hunted! Press R to play again";

    }

    public void WonPassive()
    {
        thisText.text = "Congratulations! Press R to play again";

    }
    public void Lost()
    {
        thisText.text = "You lost! Press R to try again";

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
