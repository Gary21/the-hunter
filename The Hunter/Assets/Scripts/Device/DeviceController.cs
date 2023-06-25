using UnityEngine;
using UnityEngine.UI;

public class DeviceController : MonoBehaviour
{
    [SerializeField] GameObject WearStateToggle;
    [SerializeField] GameObject HeartRateToggle;
    [SerializeField] GameObject TemperatureToggle;
    [SerializeField] GameObject RespirationToggle;
    [SerializeField] GameObject RespirationRateToggle;
    [SerializeField] GameObject RRToggle;
    [SerializeField] GameObject PressureToggle;
    [SerializeField] GameObject OrientationToggle;
    [SerializeField] GameObject ECGPlotToggle;
    [SerializeField] GameObject ECGPeaksDotsToggle;

    private void Start()
    {
        Toggle toggle;

        #region Toggles
        toggle = WearStateToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveWearState);
        
        toggle = HeartRateToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveHR);
        
        toggle = TemperatureToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveTemperature);
        
        toggle = RespirationToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveRespiration);
        
        toggle = RespirationRateToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveRespirationRate);
        
        toggle = RRToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveRR);
        
        toggle = PressureToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceivePressure);
        
        toggle = OrientationToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveOrientation);
        
        toggle = ECGPlotToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(EnablePlotting);
        
        toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(EnablePlottingDetectedPeaks);
        
        #endregion Toggles
        
    }

    public void ReceiveWearState(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(DeviceOnDataReceived.OnWearStateReceived);
            //SettingsStructure.WearState = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Unsubscribe(DeviceOnDataReceived.OnWearStateReceived);
            //SettingsStructure.WearState = false;
        }
    }
    public void ReceiveHR(bool doReceive)
    {
        if(doReceive)
        {
            GameObject ECGReceiver = new GameObject("ECGReceiver");
            GameObject.DontDestroyOnLoad(ECGReceiver);

            ECGReceiver.AddComponent<ECGReceiver>();
            //SettingsStructure.HeartRate = true;
        }
        else
        {
            GameObject ECGReceiver = GameObject.Find("ECGReceiver");
            if(ECGReceiver)
                GameObject.Destroy(ECGReceiver);
            //SettingsStructure.HeartRate = false;
        }
    }
    public void ReceiveTemperature(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Subscribe(DeviceOnDataReceived.OnTemperatureReceived);
            //SettingsStructure.Temperature = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Unsubscribe(DeviceOnDataReceived.OnTemperatureReceived);
            //SettingsStructure.Temperature = false;
        }
    } 
    public void ReceiveRespiration(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Subscribe(DeviceOnDataReceived.OnRespirationReceived);
            //SettingsStructure.Respiration = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Unsubscribe(DeviceOnDataReceived.OnRespirationReceived);
            //SettingsStructure.Respiration = false;
        }
    } 
    public void ReceiveRespirationRate(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Subscribe(DeviceOnDataReceived.OnRespirationRateReceived);
            //SettingsStructure.RespirationRate = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Unsubscribe(DeviceOnDataReceived.OnRespirationRateReceived);
            //SettingsStructure.RespirationRate = false;            
        }
    }
    public void ReceiveRR(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.rr.Subscribe(DeviceOnDataReceived.OnRRReceived);
            //SettingsStructure.RR = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.rr.Unsubscribe(DeviceOnDataReceived.OnRRReceived);
            //SettingsStructure.RR = false;
        }
    } 
    public void ReceivePressure(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Subscribe(DeviceOnDataReceived.OnPressureReceived);
            //SettingsStructure.Pressure = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Unsubscribe(DeviceOnDataReceived.OnPressureReceived);
            //SettingsStructure.Pressure = false;
        }
    } 
    public void ReceiveOrientation(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Subscribe(DeviceOnDataReceived.OnOrientationReceived);
            //SettingsStructure.Orientation = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Unsubscribe(DeviceOnDataReceived.OnOrientationReceived);
            //SettingsStructure.Orientation = false;
        }
    } 
    public void EnablePlotting(bool doReceive)
    {
        if (doReceive)
        {
            GameObject FloatPlotter = new GameObject("FloatPlotter");
            GameObject.DontDestroyOnLoad(FloatPlotter);

            // FloatPlotter.transform.parent = GameObject.Find("Canvas").transform;
            FloatPlotter.transform.position = new Vector3(590, 225, 0);

            var FloatGraphComponent = FloatPlotter.AddComponent<FloatGraph>();
            GameObject ECGReceiver = GameObject.Find("ECGReceiver");
            if (ECGReceiver != null)
                ECGReceiver.GetComponent<ECGReceiver>().floatGraph = FloatGraphComponent;
            Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            toggle.interactable = true;
            //SettingsStructure.PlotECG = true;
        }
        else
        {
            GameObject FloatPlotter = GameObject.Find("FloatPlotter");
            if (FloatPlotter)
                GameObject.Destroy(FloatPlotter);
            Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            toggle.interactable = false;
            //SettingsStructure.PlotECG = false;
        }
    }
    public void EnablePlottingDetectedPeaks(bool doReceive)
    {
        GameObject FloatPlotter = GameObject.Find("FloatPlotter");
        var FloatGraphComponent = FloatPlotter.GetComponent<FloatGraph>();
        if (doReceive)
        {
            FloatGraphComponent.showDetectedPoitns = true;
            //SettingsStructure.PlotDetectedPeaks = true;
        }
        else
        {
            FloatGraphComponent.showDetectedPoitns = false;
            //SettingsStructure.PlotDetectedPeaks = false;
        }
    }
}
