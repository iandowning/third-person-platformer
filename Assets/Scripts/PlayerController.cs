using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    private bool isTimerRunning = false;
    private bool isInContact = false;
    private int numJumps = 0;
    
    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
            
        rb.freezeRotation = true;
    }
    
    void OnCollisionEnter(Collision collision){
        numJumps = 0;
    }
    
    void OnCollisionStay(Collision collision){
        numJumps = 0;
    }
    
    void OnCollisionExit(Collision collision){
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numJumps < 2) 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            numJumps++;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            
            Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;
            
            Vector3 newVelocity = moveDirection * moveSpeed;
            newVelocity.y = rb.linearVelocity.y; 
            rb.linearVelocity = newVelocity;
            
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            Vector3 currentVelocity = rb.linearVelocity;
            rb.linearVelocity = new Vector3(0, currentVelocity.y, 0);
        }
    }
    public void StartDash()
    {
        if (!isTimerRunning)
        {
            moveSpeed *= 2;
            StartCoroutine(TimerCoroutine());
        }
    }

     private IEnumerator TimerCoroutine()
    {
        isTimerRunning = true; 
        float elapsedTime = 0f;
        float duration = 5f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isTimerRunning = false;
        
        TimerCompleted();
    }
    
    private void TimerCompleted()
    {
        moveSpeed /= 2;
    }
}