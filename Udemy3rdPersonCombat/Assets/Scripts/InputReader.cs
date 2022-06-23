using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
