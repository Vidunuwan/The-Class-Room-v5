using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private float playerSpeed;
    [SerializeField] private float playerRunSpeed = 5.0f;
    [SerializeField] private float playerWalkSpeed = 2.0f;

    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 2f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction runAction;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barellTransform;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletMiss = 25f;

    private Transform cameraTransform;


    public Animator animator;
    //int moveXAnimator;
    //int moveZAnimator;
    int jumpAnimation;




    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        runAction = playerInput.actions["Run"];



        cameraTransform = Camera.main.transform;

        playerSpeed = playerWalkSpeed;

        animator = transform.GetComponent<Animator>();

        //moveXAnimator=Animator.StringToHash("MoveX");
        //moveZAnimator= Animator.StringToHash("MoveZ");
        jumpAnimation = Animator.StringToHash("Pistol Jump");
    }

    private void OnEnable()
    {
        shootAction.performed += _ => Shoot();
        runAction.performed += _ => Run();
        runAction.canceled += _ => StopRun();

    }

    private void OnDisable()
    {
        shootAction.performed -= _ => Shoot();
        runAction.performed -= _ => Run();
        runAction.canceled -= _ => StopRun();

    }

    private void Run()
    {
        playerSpeed = playerRunSpeed;
        animator.SetBool("Runing", true);
    }
    private void StopRun()
    {
        playerSpeed = playerWalkSpeed;
        animator.SetBool("Runing", false);
    }

    private void Shoot()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barellTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (Physics.Raycast(cameraTransform.position,cameraTransform.forward,out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position+cameraTransform.forward*bulletMiss;
            bulletController.hit = false;
        }
    }

    void Update()
    {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Jumping..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.CrossFade(jumpAnimation,0.15f);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveZ", input.y);

        //animator.SetFloat("MoveX", 1);
        //animator.SetFloat("MoveZ", 1);

        //Debug.Log(animator.GetFloat("MoveX"));
        //Debug.Log(animator.GetFloat("MoveZ"));

    }
}