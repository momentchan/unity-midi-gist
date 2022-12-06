using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mj.midi
{
    [CreateAssetMenu(fileName = "ControlMapping", menuName = "ScriptableObjects/ControlMapping")]
    public class ControlMapping : ScriptableObject
    {
        public DeviceMapper GetDeviceMapper(DeviceType device) => deviceMappers.FirstOrDefault(dm => dm.device == device);
        public ControlType GetControlType(DeviceType device, string name) => GetDeviceMapper(device).GetControlType(name);

        [SerializeField] private List<DeviceMapper> deviceMappers;
    }

    [System.Serializable]
    public class DeviceMapper
    {
        public DeviceType device;
        public ControlMapper GetControlMapper(string name) => controlMappers.FirstOrDefault(cp => cp.name == name);
        public ControlType GetControlType(string name) => GetControlMapper(name).type;
        public void Regist(string name) => controlMappers.Add(new ControlMapper() { name = name });
        [SerializeField] private List<ControlMapper> controlMappers;
    }

    [System.Serializable]
    public class ControlMapper
    {
        public ControlType type;
        public string name;
    }
    public enum DeviceType
    {
        nanoKONTROL2
    }
    public enum ControlType
    {
        Knob1,
        Knob2,
        Knob3,
        Knob4,
        Knob5,
        Knob6,
        Knob7,
        Knob8,

        Slider1,
        Slider2,
        Slider3,
        Slider4,
        Slider5,
        Slider6,
        Slider7,
        Slider8,

        Solo1,
        Solo2,
        Solo3,
        Solo4,
        Solo5,
        Solo6,
        Solo7,
        Solo8,

        Mute1,
        Mute2,
        Mute3,
        Mute4,
        Mute5,
        Mute6,
        Mute7,
        Mute8,

        Rec1,
        Rec2,
        Rec3,
        Rec4,
        Rec5,
        Rec6,
        Rec7,
        Rec8,

        TrackBack,
        TrackForward,

        Cycle,

        MarkerSet,
        MarkerBack,
        MarkerForward,

        FastBack,
        FastForward,

        Stop,
        Play,
        Record,
    }
}