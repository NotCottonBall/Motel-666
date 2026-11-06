using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
  [Header("Light Properties")]
  [SerializeField] private Light m_Flashlight;

  void Start()
  {
    m_Flashlight.enabled = false;
  }

  public void OnFlashlightToggle(InputAction.CallbackContext context)
  {
    m_Flashlight.enabled = !m_Flashlight.enabled;
  }
}
