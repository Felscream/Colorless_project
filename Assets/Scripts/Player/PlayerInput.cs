using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class PlayerInput : MonoBehaviour {

    private static PlayerInput instance = null;

    [SerializeField]
    private float mouseSensibility, controllerSensibility;
    private Transform camTransform;
    private Player player;
    private PlayerMovement mv;
    private PlayerActions pa;
    private Footstep fs;
    private float movementX;
    private float movementY;
    private float lookX;
    private float lookY;
    public enum eInputType
    {
        MouseKeyboard,
        Controller
    };
    private eInputType inputType = eInputType.MouseKeyboard;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        camTransform = Camera.main.transform;

    }
    private void Start()
    {
        mv = PlayerMovement.GetInstance();
        player = Player.GetInstance(); 
        pa = PlayerActions.GetInstance();
        fs = GetComponent<Footstep>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (ObjectsInstantiated() && !player.IsDead())
        {
            GetPlayerControlInput();
        }
        else
        {
            if (player.IsDead())
            {
                movementX = 0f;
                movementY = 0f;
                Debug.Log("Player is dead");
            }
            else
            {
                Debug.Log("Objets non instanciés");
            }
        }
    }

    void OnGUI()
    {
        switch (inputType)
        {
            case eInputType.MouseKeyboard:
                if (IsControllerInput())
                {
                    inputType = eInputType.Controller;
                    Debug.Log("JoyStick being used");
                }
                break;
            case eInputType.Controller:
                if (IsMouseKeyboard())
                {
                    inputType = eInputType.MouseKeyboard;
                    Debug.Log("Mouse & Keyboard being used");
                }
                break;
        }
    }

    private bool ObjectsInstantiated()
    {
        if(mv != null && camTransform != null && pa !=null && player != null)
            return true;
        return false;
    }
    private bool IsMouseKeyboard()
    {
        // mouse & keyboard buttons
        if (Event.current.isKey ||
            Event.current.isMouse)
        {
            return true;
        }
        // mouse movement
        if (InputManager.GetAxis("MouseX") != 0.0f ||
            InputManager.GetAxis("MouseY") != 0.0f)
        {
            return true;
        }
        return false;
    }

    private bool IsControllerInput()
    {
        // joystick buttons
        if (InputManager.GetKey(KeyCode.JoystickButton0) ||
           InputManager.GetKey(KeyCode.JoystickButton1) ||
           InputManager.GetKey(KeyCode.JoystickButton2) ||
           InputManager.GetKey(KeyCode.JoystickButton3) ||
           InputManager.GetKey(KeyCode.JoystickButton4) ||
           InputManager.GetKey(KeyCode.JoystickButton5) ||
           InputManager.GetKey(KeyCode.JoystickButton6) ||
           InputManager.GetKey(KeyCode.JoystickButton7) ||
           InputManager.GetKey(KeyCode.JoystickButton8) ||
           InputManager.GetKey(KeyCode.JoystickButton9) ||
           InputManager.GetKey(KeyCode.JoystickButton10) ||
           InputManager.GetKey(KeyCode.JoystickButton11) ||
           InputManager.GetKey(KeyCode.JoystickButton12) ||
           InputManager.GetKey(KeyCode.JoystickButton13) ||
           InputManager.GetKey(KeyCode.JoystickButton14) ||
           InputManager.GetKey(KeyCode.JoystickButton15) ||
           InputManager.GetKey(KeyCode.JoystickButton16) ||
           InputManager.GetKey(KeyCode.JoystickButton17) ||
           InputManager.GetKey(KeyCode.JoystickButton18) ||
           InputManager.GetKey(KeyCode.JoystickButton19))
        {
            return true;
        }

        // joystick axis
        if (InputManager.GetAxis("GamepadHorizontal") != 0.0f ||
        InputManager.GetAxis("GamepadVertical") != 0.0f)
        {
            return true;
        }
        
        

        return false;
    }

    

    
    /**************************
    ****     GET INPUT      ***
    ***************************/
    public void GetPlayerControlInput()
    {
        //WEAPON RELATED ACTIONS
        if (pa.HasWeapon())
        {
            //FIRING
            if ((InputManager.GetAxis("Fire0") > 0 || InputManager.GetMouseButton(0)))
            {
                pa.PlayerFire();
                pa.SetFiring(true);
            }
            else
            {
                pa.SetFiring(false);
            }

            //RELOADING
            if (InputManager.GetButtonDown("Reload"))
            {
                pa.PlayerReload();
            }
        
        }

		if(InputManager.GetAxis("Capacity") != 0 || InputManager.GetButton("Capacity"))
		{
			pa.DoCapacity();
		}

        //INTERACTIONS
        if (InputManager.GetButtonDown("Submit"))
        {
            pa.PlayerInteraction();
        }

        //INVENTORY NAVIGATION
        
        if (!pa.IsSwappingWeapon())
        {
            int inputChangeWeapon = (int)InputManager.GetAxisRaw("ChangeWeapon");
            if (inputChangeWeapon != 0)
            {
                StartCoroutine(pa.ChangeWeapon(inputChangeWeapon));
            }

            if (inputType == eInputType.Controller)
            {
                if (InputManager.GetButtonDown("LatestWeapon"))
                {
                    StartCoroutine(pa.EquipLatestWeapon());
                }
            }
            else
            {
                if (InputManager.GetButtonDown("WeaponSlot0"))
                {
                    StartCoroutine(pa.EquipWeaponSlot(0));
                }
                else if (InputManager.GetButtonDown("WeaponSlot1"))
                {
                    StartCoroutine(pa.EquipWeaponSlot(1));
                }
                else if (InputManager.GetButtonDown("WeaponSlot2"))
                {
                    StartCoroutine(pa.EquipWeaponSlot(2));
                }
                else if (InputManager.GetButtonDown("WeaponSlot3"))
                {
                    StartCoroutine(pa.EquipWeaponSlot(3));
                }
            }
        }
        

        if (inputType == eInputType.MouseKeyboard)
        {
            movementX = InputManager.GetAxisRaw("Horizontal");
            movementY = InputManager.GetAxisRaw("Vertical");
            lookX = InputManager.GetAxis("LookHorizontal") * mouseSensibility;
            lookY = InputManager.GetAxis("LookVertical") * mouseSensibility;
        }
        else
        {
            movementX = InputManager.GetAxis("Horizontal");
            movementY = InputManager.GetAxis("Vertical");
            lookX = InputManager.GetAxis("LookHorizontal") * controllerSensibility;
            lookY = InputManager.GetAxis("LookVertical") * controllerSensibility;
            
        }

        //AIMING
        if (mv.IsGrounded() && !fs.GetStep() && (movementX != 0 || movementY != 0))
        {
            fs.PlayConcrete();
        }
        mv.Rotate(lookX, lookY, camTransform);
    }

    private void FixedUpdate()
    {
        //MOVING
        mv.Move(movementX, movementY);

        if (InputManager.GetButtonDown("Jump"))
        {
            mv.Jump();
        }
        
    }

    public void PrintInput()
    {
        if(movementX != 0 || movementY !=0)
        {
            Debug.Log("Movement X : " + movementX +", Movement Y : "+ movementY);
        }
        if(lookX != 0 || lookY != 0)
        {
            Debug.Log("Look X : " + lookX + ", Look Y : " + lookY);
        }
    }
    public eInputType GetInputState()
    {
        return inputType;
    }

    public static PlayerInput GetInstance()
    {
        if(instance == null)
        {
            Debug.Log("No instance of "+typeof(PlayerInput));
            return null;
        }
        return instance;

    }

    public float GetMovementX()
    {
        return movementX;
    }

    public float GetMovementY()
    {
        return movementY;
    }

}
