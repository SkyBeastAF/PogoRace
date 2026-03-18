using PogoProject;
using UnityEngine;

namespace PogoProject
{
    
    // Give this to the Red PickUps
    // ALSO: Make sure the player has the "Player" tag if you want it to work.
    // - Anthony Leone
    
    public class RedPickUpScript : PickUpScript
    {
        public ColorChanger colorChanger;
        public LayerChanger layerChanger;
        void Start()
        {
            colorChanger = FindAnyObjectByType<ColorChanger>();
            layerChanger = FindAnyObjectByType<LayerChanger>();
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
          //  base.OnCollisionEnter2D(other);
            if (other.gameObject.CompareTag("Player"))
            {
                colorChanger.ChangeColorRed();
                layerChanger.ChangeLayerRed();
            }
        }
    }
}
