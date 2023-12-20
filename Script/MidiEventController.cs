using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace mj.midi
{
    public class MidiEventController : MonoBehaviour, IMidiUser
    {
        [SerializeField] private List<NoteEvent> notes;
        [SerializeField] private List<ControlEvent> controls;
        private string currentValue;

        public void OnControlChange(ControlType type, float value)
        {
            var c = controls.FirstOrDefault(c => c.type == type);
            if (c != null)
            {
                currentValue = $"{type}: {value}";
                c.actions.Invoke(value);
            }
            else
                Debug.Log($"{type} hasn't been assigned.");
        }

        public void OnNoteOff(ControlType type)
        {
            //var n = notes.FirstOrDefault(c => c.type == type);
            //n.actions.Invoke();
        }

        public void OnNoteOn(ControlType type, float value)
        {
            var n = notes.FirstOrDefault(c => c.type == type);
            if (n != null)
            {
                currentValue = $"{type}: {value}";
                n.actions.Invoke();
            }
            else
                Debug.Log($"{type} hasn't been assigned.");
        }

        private void OnGUI()
        {
            GUILayout.Label(currentValue);
        }
    }

    [System.Serializable]
    public class NoteEvent : MidiEvent
    {
        public UnityEvent actions;
    }

    [System.Serializable]
    public class ControlEvent : MidiEvent
    {
        public UnityEvent<float> actions;
    }

    public class MidiEvent : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField] private string name;

        public ControlType type;

        public void OnBeforeSerialize()
        {
            name = type.ToString();
        }

        public void OnAfterDeserialize()
        {
            name = type.ToString();
        }

    }
}