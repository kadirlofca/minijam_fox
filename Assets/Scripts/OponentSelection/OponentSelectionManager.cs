using System;

using UnityEngine;

public class OponentSelectionManager : MonoBehaviour
{
    public static Action<SelectableOponent> OnSetCurrentOponent;
    
    private SelectableOponent _current;
    private SelectableOponent _previous;

    private void OnEnable()
    {
        OnSetCurrentOponent += SelectedOponent;
    }

    private void OnDisable()
    {
        OnSetCurrentOponent -= SelectedOponent;
    }
    
    private void SelectedOponent(SelectableOponent oponent)
    {
        _previous = _current;
        _current = oponent;
        
        HubPlayBtn.OnEnableButton?.Invoke();
        
        if (_previous != null)
        {
            _previous.DisableSelection();
        }
    }
}
