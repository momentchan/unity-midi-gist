//using UnityEngine;

///<summary>
/// 
///</summary>
using Minis;
namespace mj.midi
{
    public interface IMidiUser
    {
        #region Methods
        void OnControlChange(ControlType type, float value);
        void OnNoteOn(ControlType type, float value);
        void OnNoteOff(ControlType type);
        #endregion
    }
}