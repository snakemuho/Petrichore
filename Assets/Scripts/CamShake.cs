using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
public class CamShake : MonoBehaviour
{
    private static CamShake _instance;
    public static CamShake Instance
    {
        get { if (_instance is null) Debug.Log("cam shake null"); return _instance; }
    }
    [SerializeField] private float _shakeDuration = .2f;
    [SerializeField] private float _shakeAmp = 2;
    [SerializeField] private float _shakeFreq = 1;
    private float _shakeElapsedTime = 0f;

    private CinemachineFreeLook _cmFreeLook;
    private void Awake()
    {
        _cmFreeLook = GetComponent<CinemachineFreeLook>();
    }
    private void Start()
    {
        _instance = this;
    }
    private void Update()
    {
        if (_shakeElapsedTime > 0)
        {
            _shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            _shakeElapsedTime = 0;
            _cmFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            _cmFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            _cmFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            _cmFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            _cmFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            _cmFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        }
    }

    public void Shake()
    {
        _shakeElapsedTime = _shakeDuration;
        _cmFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeAmp;
        _cmFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeAmp;
        _cmFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeAmp;
        _cmFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _shakeFreq;
        _cmFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _shakeFreq;
        _cmFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _shakeFreq;
    }
}
