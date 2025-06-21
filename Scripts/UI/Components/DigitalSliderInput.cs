using System;
using System.Globalization;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Components
{
    public class DigitalSliderInput : MonoBehaviour
    {
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private string _format = "0.0";
        [Space(8)]
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private Slider _slider;

        public event Action<float> ValueChanged;

        private float _currentValue;

        public void SetValue(float value)
        {
            _currentValue = value;
            _slider.SetValueWithoutNotify(_currentValue);
            _input.SetTextWithoutNotify(_currentValue.ToString(_format));
        }
        
        private void Awake()
        {
            _slider.minValue = _minValue;
            _slider.maxValue = _maxValue;
            _input.onSelect.AddListener(OnInputFocus);
            _input.onEndEdit.AddListener(OnInputEditEnd);
            _input.onValueChanged.AddListener(OnInputChanged);
            _slider.onValueChanged.AddListener(OnSliderChanged);
        }
        
        private void OnDestroy()
        {
            _input.onSelect.RemoveListener(OnInputFocus);
            _input.onEndEdit.RemoveListener(OnInputEditEnd);
            _input.onValueChanged.RemoveListener(OnInputChanged);
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
        }


        private void OnInputFocus(string val)
        {
            if (float.TryParse(val, out var result) == false)
                return;

            _currentValue = result; 
            _input.SetTextWithoutNotify(_currentValue.ToString(_format));
            
        }

        private void OnInputEditEnd(string str)
        {
            if (float.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) == false)
            {
                Debug.LogError($"DigitalSliderInput: Cannot parse value '{str}'");
                _input.SetTextWithoutNotify(_currentValue.ToString(_format));
                return;
            }
            
            if (Mathf.Approximately(_currentValue, value)) 
                return;
            
            _currentValue = value;
            _input.SetTextWithoutNotify(_currentValue.ToString(_format));
            _slider.SetValueWithoutNotify(_currentValue);

            
            ValueChanged?.Invoke(_currentValue);
        }

        private void OnSliderChanged(float value)
        {
            if (Mathf.Approximately(_currentValue, value)) 
                return;
            
            _currentValue = value;
            ValueChanged?.Invoke(_currentValue);
            _input.SetTextWithoutNotify(_currentValue.ToString(_format));
        }

        private void OnInputChanged(string str)
        {

        }
    }
}