using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace mj.midi
{
    public class MidiController : MonoBehaviour
    {
        [SerializeField] private ControlMapping mapping;
        [SerializeField] private DeviceType targetDevice;

        void Start()
        {
            InputSystem.onDeviceChange += (device, change) =>
            {
                if (change != InputDeviceChange.Added) return;

                var midiDevice = device as Minis.MidiDevice;
                if (midiDevice == null) return;

                var dp = mapping.GetDeviceMapper(targetDevice);
                if (dp == null)
                {
                    Debug.LogWarning($"{targetDevice} not found.");
                    return;
                }

                var users = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IMidiUser>();

                midiDevice.onWillControlChange += (cc, value) =>
                {
                    foreach (var user in users)
                        user.OnControlChange(mapping.GetControlType(targetDevice, cc.displayName), value);
                };

                midiDevice.onWillNoteOn += (note, value) =>
                {
                    foreach (var user in users)
                        user.OnNoteOn(mapping.GetControlType(targetDevice, note.displayName), value);
                };

                midiDevice.onWillNoteOff += note =>
                {
                    foreach (var user in users)
                        user.OnNoteOff(mapping.GetControlType(targetDevice, note.displayName));
                };
            };
        }
    }
}