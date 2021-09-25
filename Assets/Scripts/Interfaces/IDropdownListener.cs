using UnityEngine.Events;
public interface IDropdownListener {
    UnityAction<int> GetDropdownListenerAction();
}
