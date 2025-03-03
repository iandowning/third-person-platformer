using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Camera mainCamera;
    private bool isInContact = false;
    
    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
            
        rigidbody.freezeRotation = true;
    }
    
    void OnCollisionEnter(Collision collision){
        isInContact = true;
    }
    
    void OnCollisionStay(Collision collision){
        isInContact = true;
    }
    
    void OnCollisionExit(Collision collision){
        isInContact = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isInContact)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
            newVelocity.y = rigidbody.linearVelocity.y; 
            rigidbody.linearVelocity = newVelocity;
            
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            Vector3 currentVelocity = rigidbody.linearVelocity;
            rigidbody.linearVelocity = new Vector3(0, currentVelocity.y, 0);
        }
    }
}