using UnityEngine;

public class Room : MonoBehaviour
{
  [Header("Room Properties")]
  [SerializeField] private AnomalyAffectedObject[] m_AnomalyAffectedObjs = { };
  [SerializeField] private Collider m_RoomArea;
  [SerializeField] private AnomalyPoint[] m_AnomalyPoints = { };
  [SerializeField] private short m_RoomNo = -1;
  private GameObject m_Occupant;
  // @TODO: make sure to add a function somewhere that poplates rooms with occupants
  // Also, some other things like what to spawn on an anomaly point etc...

  void Start()
  {
    if (m_AnomalyAffectedObjs.Length == 0)
      Debug.LogError("No Anomaly Objects Found In Room " + gameObject.name + ". Every Room Must Have At Least One Anomaly Effected Object");
    if (!m_RoomArea)
      Debug.LogError("No Room Area Was Set For Room " + gameObject.name + ". Every Room Must Have It's Own Area");
    if (m_AnomalyPoints.Length == 0)
      Debug.LogError("No Anomaly Points Were Set In Room " + gameObject.name + ". Every Room Must Have At Least One Anomaly Point");
    if (m_RoomNo == -1)
      Debug.LogError("Room Number For Room " + gameObject.name + " Is '-1'. Which Means You Didn't Set The Room Number For This Room.");

    if (m_RoomArea.isTrigger == false)
      Debug.Log("Room Area Must Be A Trigger. The Room Area Collider For Room " + gameObject.name + " Is Not Set As A Trigger");
  }

  void Update()
  {

  }
}
