using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputScript : MonoBehaviour
{
    // This is a script I am making to let me get player input for
    // the character so I can test things. Can be removed after we get the
    // true player input script. 
    // - Anthony Leone
    
    public InputActionReference leftButton;
    public InputActionReference rightButton;
    public InputActionReference upButton;
    public InputActionReference downButton;
        
    private TestInputScript TIS;
    private TestMovementScript TMS;
    void Awake()
    {
        TMS = GetComponent<TestMovementScript>();
    }

    
    void Update()
    {
            if (leftButton.action.IsPressed())
            {
                LeftPressed();
            }

            if (rightButton.action.IsPressed())
            {
                RightPressed();
            } 
            if (upButton.action.IsPressed())
            {
                UpPressed();
            }
            if (downButton.action.IsPressed())
            {
                DownPressed();
            }

            if (!leftButton.action.IsPressed() && !rightButton.action.IsPressed() 
                                               && !upButton.action.IsPressed() && !downButton.action.IsPressed())
            {
                NothingPressed();
            }
        
    }
    void LeftPressed()
    {
        TMS.moveDirection = new Vector2(-1, 0);
    }

    void RightPressed()
    {
        TMS.moveDirection = new Vector2(1, 0);
    }

    void UpPressed()
    {
        TMS.moveDirection = new Vector2(0, 1);
    }

    void DownPressed()
    {
        TMS.moveDirection = new Vector2(0, -1);
    }

    void NothingPressed()
    {
        TMS.moveDirection = new Vector2(0, 0);
    }
}
