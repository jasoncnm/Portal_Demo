using Unity.Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public IPlayerMode playerMode { get; private set; }

    [SerializeField] CinemachineCamera firstPerson;
    [SerializeField] Transform Player;



    void Start()
    {
        playerMode = new FirstPersonMode();
        CinemachinePanTilt panTilt = firstPerson.GetComponent<CinemachinePanTilt>();
        panTilt.PanAxis.Value = Player.rotation.eulerAngles.y;
        panTilt.TiltAxis.Value = Player.rotation.eulerAngles.x;
    }

}
