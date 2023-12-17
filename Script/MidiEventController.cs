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

        public void OnControlChange(ControlType type, float value)
        {
            var c = controls.FirstOrDefault(c => c.type == type);
            if (c != null)
                c.actions.Invoke(value);
        }

        public void OnNoteOff(ControlType type)
        {
            //var n = notes.FirstOrDefault(c => c.type == type);
            //n.actions.Invoke();
        }

        public void OnNoteOn(ControlType type, float value)
        {
            var n = notes.FirstOrDefault(c => c.type == type);
            n.actions.Invoke();
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

    public class MidiEvent
    {
        public ControlType type;
    }
}