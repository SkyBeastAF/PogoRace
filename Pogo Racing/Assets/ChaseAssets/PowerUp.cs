using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
 void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spring"))
        {
            Destroy(other.gameObject);
            this.transform.Translate(transform.up * 1.2f);
            //This should theoretically send the player upward a bit when they hit the spring.
            //Obviously, we'll need to test once the player script is done
        }
        
        else if (other.gameObject.CompareTag("Gas"))
        {
            Destroy(other.gameObject);
            //speed = speed * 2;
            //Increases speed by 2x when hitting gas can
            StartCoroutine(StopSpeedUp());
        }
    }

    IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(2.5f);
        //speed = speed / 2;
        //Then after 2 and a half seconds, the speed boost divides by 2, restoring base speed
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
