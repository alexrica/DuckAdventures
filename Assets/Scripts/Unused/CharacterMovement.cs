using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool jump = false;
    public float rotationSpeed = 300f; // Velocidad de rotación en grados por segundo
    public Transform groundCheck;
    public LayerMask groundMask;

    private bool isGrounded;
    private bool isRotating = false;
    private Rigidbody rb;
    private Transform mainCameraTransform;
    private Quaternion targetRotation; // Rotación objetivo gradual en el eje Z

    public GameObject start;

    public GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCameraTransform = Camera.main.transform;
        // Inicializa la rotación objetivo con la rotación actual del personaje
        targetRotation = transform.rotation;

        this.transform.position = start.transform.position;
    }

    private void Update()
    {
        // Verificar si el personaje está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        // Movimiento horizontal y vertical en relación con la cámara
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (cameraForward * verticalInput + mainCameraTransform.right * horizontalInput) * speed;

        // Aplicar el movimiento directamente al Rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Girar el modelo del jugador hacia la dirección del movimiento
        if (movement != Vector3.zero)
        {
            Vector3 targetForward = Vector3.ProjectOnPlane(movement, Vector3.up).normalized;
            targetRotation = Quaternion.LookRotation(targetForward, Vector3.up);
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }

        // Rotar gradualmente en el eje Z
        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jump == true)
        {
            rb.AddForce(new Vector2(0, 1)*3, ForceMode.Impulse);
            jump = false;
        }
        // Salto
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            this.transform.position = start.transform.position;
        }
    }
}
