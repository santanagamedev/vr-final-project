using System;

public class ObservablePuzzelThird
{
    private bool _value; // El valor de la variable
    public event Action<bool> OnValueChanged; // Evento que notifica cambios

    public bool Value
    {
        get => _value; // Devuelve el valor actual
        set
        {
            if (_value != value) // Solo notifica si el valor cambia
            {
                _value = value;
                OnValueChanged?.Invoke(_value); // Llama a los observadores
            }
        }
    }

    // Constructor para inicializar el valor
    public ObservablePuzzelThird(bool initialValue = false)
    {
        _value = initialValue;
    }
}