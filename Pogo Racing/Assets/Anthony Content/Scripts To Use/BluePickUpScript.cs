using UnityEngine;

namespace PogoProject
{
    // Give this to the Blue Pickups
    // ALSO: Make sure the player has the "Player" tag if you want it to work.
    // - Anthony Leone

    public class BluePickUpScript : PickUpScript
    {
        public ColorChanger colorChanger;
        public LayerChanger layerChanger;

        void Start()
        {
            colorChanger = FindAnyObjectByType<ColorChanger>();
            layerChanger = FindAnyObjectByType<LayerChanger>();
        }


        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            if (other.gameObject.CompareTag("Player"))
            {
                colorChanger.ChangeColorBlue();
                layerChanger.ChangeLayerBlue();
            }
        }
    }
}