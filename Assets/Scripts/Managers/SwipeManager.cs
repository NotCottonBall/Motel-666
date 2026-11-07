using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
  public static SwipeManager Instance;

  [Header("Properties")]
  private float m_CameraZoomAmount;
  private float m_RequiredDistanceToClean = 0f;
  private float m_MouseMoveDistance = 0f;

  [Header("References")]
  [SerializeField] private Slider m_ProgressSlider;
  private Camera m_MainCamera;

  void Awake()
  {
    Instance = this;
  }

  void Start()
  {
    m_ProgressSlider.gameObject.SetActive(false);
  }

  ///////////////
  // Functions //
  ///////////////
  public void StartSwiping(GameObject stain, Action<bool> OnFinished)
  {
    if (m_RequiredDistanceToClean <= 0)
      Debug.LogError("Required Distance Value Is <= 0!!! It has to be more than 0!");
    if (m_MainCamera == null)
      Debug.LogError("Swipe Manager's Main Camera Was Not Set!!!");

    m_ProgressSlider.value = 1f;
    m_MouseMoveDistance = 0f;

    // @TODO: Zoom in or out, the Stain using camera FOV.

    StopPlayer();
    StartCoroutine(SwipeRoutine(stain, OnFinished));
  }

  IEnumerator SwipeRoutine(GameObject stain, Action<bool> OnFinished)
  {
    while (m_MouseMoveDistance < m_RequiredDistanceToClean)
    {
      Vector2 mousePos = Mouse.current.position.ReadValue();
      Ray ray = m_MainCamera.ScreenPointToRay(mousePos);
      Physics.Raycast(ray, out RaycastHit hitInfo);
      bool onStain = (hitInfo.collider.gameObject.layer == 6);

      if (!PlayerController.Instance.IsPaused() && onStain)
        m_MouseMoveDistance += Mathf.Abs(Mouse.current.delta.x.ReadValue());

      m_ProgressSlider.value = 1f - (m_MouseMoveDistance / m_RequiredDistanceToClean);

      yield return null;
    }

    MovePlayer();
    Destroy(stain);
    OnFinished(true);
  }

  void StopPlayer()
  {
    m_ProgressSlider.gameObject.SetActive(true);
    PlayerController.Instance.TogglePlayerMovement(false);
    m_MainCamera.fieldOfView -= m_CameraZoomAmount;
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }
  void MovePlayer()
  {
    m_ProgressSlider.gameObject.SetActive(false);
    PlayerController.Instance.TogglePlayerMovement(true);
    m_MainCamera.fieldOfView += m_CameraZoomAmount;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  ///////////////
  // Set Get s //
  ///////////////
  public void SetRequiredDistanceToClean(float value)
  {
    m_RequiredDistanceToClean = value;
  }

  public void SetMainCamera(Camera camera)
  {
    m_MainCamera = camera;
  }
}
