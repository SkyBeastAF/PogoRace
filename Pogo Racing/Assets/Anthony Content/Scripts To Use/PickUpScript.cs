using UnityEngine;

namespace PogoProject
{


    public class PickUpScript : MonoBehaviour
    {
        // This script is for a parent class that other specific pickup scripts will use.
        // It makes the pickup disappear after it is collected.
        // -Anthony Leone
        private Collider2D theCollider;

        void Start()
        {
            theCollider = GetComponent<Collider2D>();
        }


        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}