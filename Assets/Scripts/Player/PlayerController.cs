using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  [Header("Input Actions")]
  [SerializeField] private InputActionReference m_MoveAction;

  [Header("Player Properties")]
  [SerializeField] private Transform m_CameraTransform;
  [SerializeField] private CharacterController m_Controller;
  [SerializeField] private float m_PlayerSpeed;
  [SerializeField] private float m_PlayerGravity;
  [SerializeField] private float m_MouseSensitivity;
  private bool m_PauseGame = false;
  private Vector2 m_MouseMovement;
  private Vector3 m_PlayerVelocity;
  private float m_Pitch = -90f;

  [Header("Headbob Settings")]
  [SerializeField] private float m_HeadbobAmplitude;
  [SerializeField] private float m_HeadbobFrequency;
  [SerializeField] private float m_HeadbobIntensity;
  private Vector3 m_CamOriginalPos;
  private float m_Timer = 0f;

  [Header("Interaction Settings")]
  [SerializeField] private float m_InteractionDistance;

  void Start()
  {
    m_CamOriginalPos = m_CameraTransform.localPosition;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  private void OnEnable()
  {
    m_MoveAction.action.Enable();
  }

  private void OnDisable()
  {
    m_MoveAction.action.Enable();
  }

  void Update()
  {
    if (!m_Controller.isGrounded)
      m_PlayerVelocity.y = 0f;

    if (!m_PauseGame)
    {
      HandlePlayerMovement();
      HandleCameraMovement();
    }

    Debug.DrawRay(m_CameraTransform.position, m_CameraTransform.forward * m_InteractionDistance, Color.pink);
  }

  ///////////////
  // FUNCTIONS //
  ///////////////
  void HandlePlayerMovement()
  {
    Vector2 input = m_MoveAction.action.ReadValue<Vector2>();
    Vector3 move = m_CameraTransform.forward * input.y + m_CameraTransform.right * input.x;
    move = Vector3.ClampMagnitude(move, 1f);

    if (move != Vector3.zero)
      HeadBob();

    m_PlayerVelocity.y += m_PlayerGravity * Time.deltaTime;

    Vector3 finalMove = (move * m_PlayerSpeed) + (m_PlayerVelocity.y * Vector3.up);
    m_Controller.Move(finalMove * Time.deltaTime);
  }

  void HandleCameraMovement()
  {
    transform.Rotate(transform.up, m_MouseMovement.x * m_MouseSensitivity * Time.deltaTime);

    m_Pitch -= m_MouseMovement.y * m_MouseSensitivity * Time.deltaTime;
    m_Pitch = Mathf.Clamp(m_Pitch, -89f, 89f);
    m_CameraTransform.localEulerAngles = new Vector3(m_Pitch, m_CameraTransform.localEulerAngles.y, 0f);
  }

  void HeadBob()
  {
    // a * sin(t * f)
    m_Timer += Time.deltaTime * m_HeadbobFrequency;
    float bob = Mathf.Sin(m_Timer) * m_HeadbobAmplitude;
    Vector3 targetBobPos = m_CamOriginalPos + Vector3.up * bob;

    m_CameraTransform.localPosition = Vector3.Lerp(
      m_CameraTransform.localPosition,
      targetBobPos,
      Time.deltaTime * m_HeadbobIntensity
    );
  }

  // Interaction with a stain (requires cleaning).
  void HandleStainInteraction(GameObject obj)
  {
    switch (obj.GetComponent<Stain>().GetStainType())
    {
      case StainType.Regular:
        {
          Debug.Log("REGU");
          break;
        }
      case StainType.Water:
        {
          Debug.Log("Water");
          break;
        }
    }
  }

  ///////////////
  // CALLBACKS //
  ///////////////
  public void OnMouseMove(InputAction.CallbackContext context)
  {
    m_MouseMovement = context.action.ReadValue<Vector2>();
  }

  public void OnEscape(InputAction.CallbackContext context)
  {
    m_PauseGame = !m_PauseGame;
    if (m_PauseGame)
    {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
    else
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }

  public void OnInteract(InputAction.CallbackContext context)
  {
    if (context.canceled || context.performed)
      return;

    RaycastHit hitInfo;
    Ray ray = new Ray(m_CameraTransform.position, m_CameraTransform.forward * m_InteractionDistance);
    Physics.Raycast(ray, out hitInfo);
    if (hitInfo.collider == null) return;

    switch (hitInfo.collider.gameObject.layer)
    {
      case 6: // Stain
        {
          HandleStainInteraction(hitInfo.collider.gameObject);
          break;
        }
      case 7: // Door
        {
          Debug.Log("Door");
          break;
        }
      case 8: // Pickup
        {
          Debug.Log("Pickup");
          break;
        }
    }
  }
}
