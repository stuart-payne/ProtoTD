using UnityEngine.Events;

namespace ProtoTD.Interfaces
{
    public interface IDropdownListener {
        UnityAction<int> GetDropdownListenerAction();
    }
}
