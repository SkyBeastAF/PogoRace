
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    // Give this script to the player so the pickups also change their layer.
    // -Anthony Leone
    
    
    private LayerMask layerMask;
    
    public void ChangeLayerRed()
    {
        gameObject.layer = LayerMask.NameToLayer("Red Layer");
    }

    public void ChangeLayerBlue()
    {
        gameObject.layer = LayerMask.NameToLayer("Blue Layer");
    }

    public void ChangeLayerYellow()
    {
        gameObject.layer = LayerMask.NameToLayer("Yellow Layer");
    }
}
