using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text TxtFps;

    private const float FpsMeasurePeriod = 0.5f;
    private int _fpsAccumulator = 0;
    private float _fpsNextPeriod = 0;
    private int _currentFps;
    private const string Display = "{0} FPS";

    private void Start()
    {
        _fpsNextPeriod = Time.realtimeSinceStartup + FpsMeasurePeriod;
    }

    private void Update()
    {
        _fpsAccumulator++;
        if (Time.realtimeSinceStartup > _fpsNextPeriod)
        {
            _currentFps = (int) (_fpsAccumulator/FpsMeasurePeriod);
            _fpsAccumulator = 0;
            _fpsNextPeriod += FpsMeasurePeriod;
            TxtFps.text = string.Format(Display, _currentFps);
        }
    }
}