using UnityEngine;

public enum AnomalyPointType
{
  Unknown = 0,
  DeadBodySpawn,
  BloodStainSpawn,
  BeingBodySpawn
}

public class AnomalyPoint : MonoBehaviour
{
  [SerializeField] private AnomalyPointType m_TypeToSpawn;
}
