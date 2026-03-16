using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Give this script to the player character to have the pickups change their color.
    // -Anthony Leone
    
    
    private SpriteRenderer sR;
    
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    public void ChangeColorRed()
    {
        sR.color = Color.red;
    }
    public void ChangeColorBlue()
    {
        sR.color = Color.dodgerBlue;
    }

    public void ChangeColorYellow()
    {
        sR.color = Color.yellowNice;
    }
}
