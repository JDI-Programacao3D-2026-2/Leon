using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    private float speed = 5f;
    public Rigidbody rb;
    private Vector3 moveDirection;
    
    //Movement flags
    private bool isMoving = false;
    private bool isJumping = false;
    private bool canJump = true;
    
    //Shoujo Girl Ability Flags
    protected bool isShoujoGirl = false;
    protected bool canStringfy = false;
    protected bool isStringfying = false;
    protected bool canWallCling = false;
    protected bool isWallClinging = false;
    protected bool canCharm = false;
    protected bool isCharming = false;
    
    //Plataformer Boy Ability Flags
    protected bool isPlataformerBoy = false;
    protected bool canDoubleJump = false;
    protected bool isDoubleJumping = false;
    protected bool canDash = false;
    protected bool isDashing = false;
    protected bool canWallJump = false;
    protected bool isWallJumping = false;

    //RPG Hero Ability Flags
    protected bool isRPGHero = false;
    protected bool canChangeWeapon = false;
    protected bool isChangingWeapon = false;
    protected bool canCastSpell = false;
    protected bool isCastingSpell = false;
    protected bool canFreezeTime = false;
    protected bool isFreezingTime = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        //player movement input
        moveDirection = Vector3.zero;
        isMoving = false;

        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection += transform.forward;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            moveDirection -= transform.forward;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            moveDirection -= transform.right;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            moveDirection += transform.right;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canJump)
        {
            isJumping = true;
            canJump = false;
        }

        //clamping the movement direction to prevent faster diagonal movement
        if (moveDirection != Vector3.zero)
        {
            Vector3 newDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            moveDirection = new Vector3(newDirection.x, moveDirection.y, newDirection.z);
            isMoving = true;
        }
    }

    //fixed update for physics
    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
        else
        {
            //prevents sliding vvv
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        if (isJumping)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            isJumping = false;
        }
    }

    //used by CharacterStats to set the character type for abilities
    public void SetCharacterType(CharacterStats.CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterStats.CharacterType.ShoujoGirl:
                isShoujoGirl = true;
                canStringfy = true;
                break;
            case CharacterStats.CharacterType.PlataformerBoy:
                isPlataformerBoy = true;
                canDash = true;
                break;
            case CharacterStats.CharacterType.RPGHero:
                isRPGHero = true;
                canChangeWeapon = true;
                canCastSpell = true;
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            if (isPlataformerBoy)
            {
                if(isWallJumping)
                {
                    canWallJump = true;
                    isWallJumping = false;
                }
                if(isDoubleJumping)
                {
                    canDoubleJump = true;
                    isDoubleJumping = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Wall"))
        {
            if (isPlataformerBoy)
            {
                canWallJump = true;
            }
            else if (isShoujoGirl && isStringfying)
            {
                canWallCling = true;
            }
        }
    }

    //Shoujo Girl Abilities
    void Stringfy()
    {
        
    }

    void WallCling()
    {
        
    }

    void Charm()
    {
        
    }

    //Plataformer Boy Abilities
    void DoubleJump()
    {
        
    }

    void Dash()
    {
        
    }

    void WallJump()
    {
        
    }

    //RPG Hero Abilities
    void ChangeWeapon()
    {
        
    }

    void CastSpell()
    {
        
    }

    void FreezeTime()
    {
        
    }
}
