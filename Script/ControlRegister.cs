using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace mj.midi
{
    public class ControlRegister : MonoBehaviour
    {
        [SerializeField] private ControlMapping mapping;
        [SerializeField] private string targetDevice;

        private DeviceMapper dp;
        void Start()
        {
            InputSystem.onDeviceChange += (device, change) =>
            {
                if (change != InputDeviceChange.Added) return;

                var midiDevice = device as Minis.MidiDevice;
                if (midiDevice == null) return;

                dp = mapping.GetDeviceMapper(targetDevice);
                if (dp == null)
                {
                    Debug.LogWarning($"{targetDevice} not found.");
                    return;
                }

                midiDevice.onWillControlChange += (cc, value) =>
                {
                    Regist(cc);
                };

                midiDevice.onWillNoteOn += (note, velocity) =>
                {
                    Regist(note);
                };
            };
        }

        private void Regist(InputControl control)
        {
            var cp = dp.GetControlMapper(control.displayName);
            if (cp == null)
            {
                dp.Regist(control.displayName);
                Debug.Log($"Register {control.displayName}.");
                EditorUtility.SetDirty(mapping);
                AssetDatabase.SaveAssets();
            }
            else
            {
                Debug.Log($"{control.displayName} has been registered as {cp.type}.");
            }
        }
    }
}