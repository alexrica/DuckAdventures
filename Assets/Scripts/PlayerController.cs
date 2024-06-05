using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public BigDuck bigDuckScript;
    private CharacterController characterController;
    private Vector2 input;
    private Vector3 direction;
    bool interactTriggerOn = false;
    bool dragTriggerOn = false;
    public bool isDragging = false;
    GameObject Item;
    bool mamaPatoActiva = false;

    public CameraFollow cameraScript;
    float activeScene;
    bool lvl3 = false;
    bool lvl7 = false;

    CapsuleCollider interactuableCollider;
    Vector3 posColliderPato;
    public Vector3 posColliderPatoTop;

    bool canHide = false;
    GameObject hideableObject;

    public GameObject correrMurcielago;

    [Tooltip("How slow the character rotates. Higher values means slower rotation")]
    [SerializeField] private float smoothTime = 0.05f;
    private float currentVelocity;

    [Tooltip("How fast the character moves")]
    [SerializeField] private float baseSpeed;
    private float speed;

    private float gravity = -9.81f;
    [Tooltip("1.0f Will set Earth's gravity")]
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;

    [Tooltip("How high the character jumps")]
    [SerializeField] private float jumpPower;

    public GameManager gameManager;
    [Tooltip("Spawnpoint")]
    public GameObject start;

    [Header("Assigned in runtime")]
    public bool crossed;
    bool playerControlled = true;

    RaycastHit hit;
    private bool rayBool = false;

    Animator animator;
    private bool movN = true;
    public bool jump = false;

    AudioSource audioData;

    bool areaForQuack = false;
    public AreaForQuack areaForQuackScript;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        speed = baseSpeed;
        this.transform.position = start.transform.position;
        crossed = false;

        animator = GetComponent<Animator>();
        interactuableCollider = GetComponent<CapsuleCollider>();
        posColliderPato = interactuableCollider.center;

        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        activeScene = cameraScript.CheckScene();
        switch (activeScene)
        {
            case 3:
                if (lvl3 == false)
                {
                    lvl3 = true;
                }
                break;
            case 7:
                if (lvl7 == false)
                {
                    lvl7 = true;
                }
                break;
        }

        int rnd;
        rnd = Random.Range(0, 1000);
        //Debug.Log(rnd);

        if (rnd == 4)
        {
            animator.SetBool("Idle1", true);
        }
        else
        {
            animator.SetBool("Idle1", false);
        }

        ApplyRotation();
        ApplyMovement();
        ApplyGravity();

        MetodoR();
        if (rayBool)
        {
            if (Input.GetKey(KeyCode.R))
            {
                Debug.Log("Pulsado");
            }
        }

        if (movN)
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("Andar", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("Andar", true);
            }
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("Andar", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("Andar", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("Andar", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("Andar", false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("Andar", false);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("Andar", false);
            }
        }

        else if (movN == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("AgacharseAndar", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("AgacharseAndar", true);
            }
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("AgacharseAndar", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("AgacharseAndar", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("AgacharseAndar", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("AgacharseAndar", false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("AgacharseAndar", false);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("AgacharseAndar", false);
            }
        }

        if (Input.GetKey(KeyCode.Space) && jump == true)
        {
            animator.SetTrigger("Salto");
            jump = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            animator.SetBool("Agacharse", true);
            movN = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            movN = true;

            animator.SetBool("AgacharseAndar", false);
            animator.SetBool("Agacharse", false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            audioData.Play(0);
            if (lvl3 == true)
            {
                animator.SetBool("Saludar", true);
                gameManager.PlayerDetected(transform.position);
            }
            else if (lvl7 == true && areaForQuack == true)
            {
                animator.SetBool("Andar", false);
                animator.SetBool("Saludar", false);
                areaForQuackScript.DeletePlayerController();
            }
            else
            {
                animator.SetBool("Saludar", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetBool("Saludar", false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("Interactuar", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("Interactuar", false);
        }
    }

    private void Start()
    {
    }

    //This only happens when you press a key, and just sets the direction the character will point to and move.
    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        if (activeScene == 6)
        {
            input.y = 2;
            input.x = -input.x;
            correrMurcielago.SetActive(false);
        }
        else if (activeScene == 1 && mamaPatoActiva == false)
        {
            bigDuckScript.GoToWaypoint();
            mamaPatoActiva = true;
        }
        direction = new Vector3(input.x, 0.0f, input.y);
    }

    //This will make the character jump.
    public void Jump(InputAction.CallbackContext context)
    {
        jump = true;
        if (!context.started) return;
        if (!IsGrounded()) return;
        if (playerControlled == false) return;

        velocity += jumpPower;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactTriggerOn)
        {
            if (InputActionPhase.Canceled == context.phase)
                return;
        }

        if (dragTriggerOn)
        {
            if (InputActionPhase.Canceled == context.phase)
            {
                Item.GetComponent<DragObject>().EndDragging();
                isDragging = false;
                speed = baseSpeed;
                interactuableCollider.center = posColliderPato;
                return;
            }
            else
            {
                Item.GetComponent<DragObject>().StartDragging();
                isDragging = true;
                speed = baseSpeed * 0.5f;
                interactuableCollider.center = posColliderPatoTop;
            }
        }
    }

    public void Hide(InputAction.CallbackContext context)
    {
        if (canHide == true)
        {
            this.SendMessage("HideBush", hideableObject);
        }
    }

    public void MetodoR()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1f);
        if (Physics.Raycast(ray, out hit, 1f, 6))
        {
            rayBool = true;
            Debug.Log("Dentro");
        }
        //else if (Physics.Raycast(ray, out hit, 2f, 6))
        //{
        //    rayBool = false;
        //    Debug.Log("Fuera");
        //}
    }

    //This will actually move the character's position.
    private void ApplyMovement()
    {
        characterController.Move(direction * speed * Time.deltaTime);
    }

    //This will rotate the character to where it's moving.
    private void ApplyRotation()
    {
        //If there are no inputs, don't rotate the character.
        //If the character is dragging an object around, don't rotate the character.
        if (input.sqrMagnitude == 0 || isDragging == true) return;
        //Set the direction we want to rotate the character to.
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //Smooth the rotation so it doesn't snap to the target.
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    //This will make the character fall.
    private void ApplyGravity()
    {
        if (IsGrounded() && velocity < 0)
        {
            velocity = -1.0f;
        }

        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        direction.y = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Interactable":
                interactTriggerOn = true;
                Item = other.gameObject;
                break;

            case "Dragable":
                dragTriggerOn = true;
                Item = other.gameObject;
                break;

            case "Death":
                animator.SetBool("Muerte", true);
                gameManager.Die();
                break;

            case "CrossingHalfpoint":
                gameManager.MidCross();
                break;

            case "CrossingEndpoint":
                crossed = true;
                gameManager.HaveCrossed();
                break;

            case "Ruido":
                Debug.Log("Ruido");
                gameManager.Ruido();
                Item = other.gameObject;
                Item.gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = !Item.gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled;
                break;

            case "Hide":
                canHide = true;
                hideableObject = other.gameObject;
                break;

            case "AreaForQuack":
                areaForQuack = true;
                gameManager.CartelDelFinalVisible(1);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Interactable":
                interactTriggerOn = false;
                Item = null;
                break;

            case "Dragable":
                dragTriggerOn = false;
                Item = null;
                break;

            case "Hide":
                canHide = false;
                hideableObject = null;
                break;

            case "AreaForQuack":
                gameManager.CartelDelFinalVisible(2);
                areaForQuack = false;
                break;
        }
    }

    private bool IsGrounded() => characterController.isGrounded;
}
