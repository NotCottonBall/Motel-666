using UnityEngine;

public enum InteractableType
{
  Unknown = 0,
  Coin,
  Battery,
  Door
};

public class Interactable : MonoBehaviour
{
  [SerializeField] private InteractableType m_Type = InteractableType.Unknown;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (m_Type == InteractableType.Unknown)
      Debug.LogError("Interactable Type Not Set For: " + gameObject.name);
  }

  ///////////////
  // Set Get s //
  ///////////////
  public void SetInteractableType(InteractableType type)
  { m_Type = type; }

  public InteractableType GetInteractableType()
  { return m_Type; }
}
