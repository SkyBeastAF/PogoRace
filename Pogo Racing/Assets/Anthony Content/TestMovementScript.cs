using UnityEngine;

public class TestMovementScript : MonoBehaviour
{
    // This is a script I am making to let me move the character so I can test things. Can be removed after we get the
    // true movement script. 
    // - Anthony Leone
    
    public float speed;
    public Vector2 moveDirection;
    public bool inLeftPosition = true;
    public bool inRightPosition = true;
    public bool inUpPosition = true;
    public bool inDownPosition = true;
    private Rigidbody2D myRigidbody2D;
    
    void Start()
    { 
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        canMoveLeft();
        canMoveRight();
        canMoveUp();
        canMoveDown();
        
        if (inLeftPosition == false)
        {
            moveDirection.x = 1;

        }

        if (inRightPosition == false)
        {
            moveDirection.x = -1;
        }

        if (inUpPosition == false)
        {
            moveDirection.y = -1;
        }

        if (inDownPosition == false)
        {
            moveDirection.y = 1;
        }
        
        myRigidbody2D.MovePosition(myRigidbody2D.position + moveDirection * speed);
    }
    void canMoveLeft()
    {
        if (myRigidbody2D.position.x < -9)
        {
            inLeftPosition = false;
        }
        else
        {
            inLeftPosition = true;
        }
    }

    void canMoveRight()
    {
        if (myRigidbody2D.position.x > 9)
        {
            inRightPosition = false;
        }
        else
        {
            inRightPosition = true;
        }
    }

    void canMoveUp()
    {
        if (myRigidbody2D.position.y > 4)
        {
            inUpPosition = false;
        }
        else
        {
            inUpPosition = true;
        }
    }

    void canMoveDown()
    {
        if (myRigidbody2D.position.y < -4)
        {
            inDownPosition = false;
        }
        else
        {
            inDownPosition = true;
        }
    }
}
