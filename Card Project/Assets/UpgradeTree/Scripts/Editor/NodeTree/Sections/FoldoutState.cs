using System.Collections.Generic;

public class FoldoutState
{
    private readonly Dictionary<string, bool> _state = new();

    public bool Get(string key, bool defaultValue = false)
    {
        if (!_state.ContainsKey(key))
            _state[key] = defaultValue;

        return _state[key];
    }

    public void Toggle(string key) => _state[key] = !Get(key);

    public void Set(string key, bool value) => _state[key] = value;
}