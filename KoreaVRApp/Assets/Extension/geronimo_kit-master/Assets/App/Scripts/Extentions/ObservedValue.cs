using System;

public class ObservedValue<T>
{
    private T currentValue;

    public event Action OnValueChange;
    public event Action<T> OnValueChanged;

    public ObservedValue(T initialValue)
    {
        currentValue = initialValue;
    }

    public T Value
    {
        get { return currentValue; }

        set
        {
            if (!currentValue.Equals(value))
            {
                currentValue = value;

                if (OnValueChange != null) OnValueChange();
                if (OnValueChanged != null) OnValueChanged(currentValue);
            }
        }
    }

    public void SetSilently(T value)
    {
        currentValue = value;
    }
}