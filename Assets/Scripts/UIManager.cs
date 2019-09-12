using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private SortSettings m_SortSettings;

    [SerializeField]
    private GameObject m_SortOptionsPanel;

    [SerializeField]
    private Dropdown m_SortAlgorithmsDropdown;  // for showing all supported sort algorithms

    [SerializeField]
    private Dropdown m_OrderingDropdown;    // the order can be Ascending or Descending

    [SerializeField]
    private Slider m_ArraySizeSlider;   // for changing number of bars on screen
    [SerializeField]
    private Text m_ArraySizeGUIText;
    private int m_ArraySize;            // only when (int)value of m_ArraySizeSlider is changed, listener will work

    [SerializeField]
    private Slider m_DelayTimeSlider;

    [SerializeField]
    private Button m_ResetButton;   // for some reasons like changing the sort algorithm from its dropdown

    [SerializeField]
    private Button m_RunButton;     // for starting selected sort algorithm from its dropdown

    [Header("Timing")]
    [SerializeField]
    private Text m_DelayText;

    [SerializeField]
    private uint m_MinimumDelayTime = 1;
    [SerializeField]
    private uint m_MaximumDelayTime = 1000;

    [Header("Bars Range")]
    /* i used uint because this ui is not good for negative numbers */
    [SerializeField]
    [Tooltip("minimum is inclusive")]
    private uint m_MinimumRange = 0;

    [SerializeField]
    [Tooltip("maximum is inclusive")]
    private uint m_MaximumRange = 500;

    private void Start()
    {
        InitUI();
    }

    /// <summary>
    /// call all initializers
    /// </summary>
    private void InitUI()
    {
        InitSortAlgorithmsDropdown();
        InitOrderingDropdown();
        InitArraySizeSlider();
        InitDelayTimeSlider();
        InitButtons();

        m_DelayText.text = $"{(int)m_DelayTimeSlider.value} ms";
    }

    private void InitSortAlgorithmsDropdown()
    {
        m_SortAlgorithmsDropdown.ClearOptions();

        List<string> options = new List<string>(System.Enum.GetNames(typeof(SortAlgorithms)));

        // making space between camel-case words for displaying better result in dropdown menu using Regexp
        for (int i = 0; i < options.Count; i++)
            options[i] = Regex.Replace(options[i], "(\\B[A-Z])", " $1");

        // inserting SearchAlgorithm enum values into dropdown menu
        m_SortAlgorithmsDropdown.AddOptions(options);
    }

    private void InitOrderingDropdown()
    {
        m_OrderingDropdown.ClearOptions();

        List<string> options = new List<string>(System.Enum.GetNames(typeof(Ordering)));

        // making space between camel-case words for displaying better result in dropdown menu using Regexp
        for (int i = 0; i < options.Count; i++)
            options[i] = Regex.Replace(options[i], "(\\B[A-Z])", " $1");

        // inserting SearchAlgorithm enum values into dropdown menu
        m_OrderingDropdown.AddOptions(options);
    }

    private void InitArraySizeSlider()
    {
        // setting min and max values due to the m_SortSettings min and max
        m_ArraySizeSlider.minValue = m_SortSettings.NumberOf_MinimumElements;
        m_ArraySizeSlider.maxValue = m_SortSettings.NumberOf_MaximumElements;

        //m_ArraySize = (int)m_ArraySizeSlider.minValue;

        // show some bars due to the first value of m_ArraySizeDropDown
        OnArraySizeChanged(m_ArraySizeSlider.minValue);

        m_ArraySizeSlider.onValueChanged.AddListener(OnArraySizeChanged);
    }

    private void InitDelayTimeSlider()
    {
        m_DelayTimeSlider.minValue = m_MinimumDelayTime;
        m_DelayTimeSlider.maxValue = m_MaximumDelayTime;

        m_DelayTimeSlider.onValueChanged.RemoveAllListeners();
        m_DelayTimeSlider.onValueChanged.AddListener(OnDelayTimeSliderChangedValue);
    }

    private void InitButtons()
    {
        m_RunButton.onClick.RemoveAllListeners();
        m_RunButton.onClick.AddListener(OnRunButtonClicked);

        m_ResetButton.onClick.RemoveAllListeners();
        m_ResetButton.onClick.AddListener(OnResetButtonClicked);
    }

    #region Listeners
    private void OnArraySizeChanged(float value)
    {
        m_ArraySizeSlider.value = (int)value;

        if ((int)value != m_ArraySize)
        {
            m_ArraySize = (int)value;
            m_ArraySizeGUIText.text = m_ArraySize.ToString();

            // size of this array will be set from given slider
            BarsManager.Instance.SetValues(ArrayGenerator.CreateRandomArray(m_ArraySize, m_MinimumRange, m_MaximumRange));
        }
    }

    private void OnDelayTimeSliderChangedValue(float value)
    {
        m_DelayText.text = $"{(int)value} ms";
    }

    private void OnRunButtonClicked()
    {
        DisableUIElements();

        switch ((SortAlgorithms)m_SortAlgorithmsDropdown.value)
        {
            case SortAlgorithms.BubbleSort:
                BubbleSort.Instance.Sort(BarsManager.Instance.Bars, (int)m_DelayTimeSlider.value / 1000.0f, (Ordering)m_OrderingDropdown.value, EnableUIElements);
                break;

            case SortAlgorithms.InsertionSort:
                InsertionSort.Instance.Sort(BarsManager.Instance.Bars, (int)m_DelayTimeSlider.value / 1000.0f, (Ordering)m_OrderingDropdown.value, EnableUIElements);
                break;

            case SortAlgorithms.QuickSort:
                QuickSort.Instance.Sort(BarsManager.Instance.Bars, (int)m_DelayTimeSlider.value / 1000.0f, (Ordering)m_OrderingDropdown.value, EnableUIElements);
                break;

            case SortAlgorithms.SelectionSort:
                SelectionSort.Instance.Sort(BarsManager.Instance.Bars, (int)m_DelayTimeSlider.value / 1000.0f, (Ordering)m_OrderingDropdown.value, EnableUIElements);
                break;

            case SortAlgorithms.ShellSort:
                ShellSort.Instance.Sort(BarsManager.Instance.Bars, (int)m_DelayTimeSlider.value / 1000.0f, (Ordering)m_OrderingDropdown.value, EnableUIElements);
                break;

            default:
                Debug.LogError("Algorithm is not implemented!");
                EnableUIElements();
                break;
        }

    }

    private void OnResetButtonClicked()
    {
        BarsManager.Instance.SetValues(ArrayGenerator.CreatedArray);
    }
    #endregion

    #region UI activity
    // when run button clicked, UI elements should be disabled
    private void DisableUIElements()
    {
        // dropdowns
        m_SortAlgorithmsDropdown.interactable = false;
        m_OrderingDropdown.interactable = false;
        m_ArraySizeSlider.interactable = false;

        // sliders
        m_DelayTimeSlider.interactable = false;

        // buttons
        m_RunButton.interactable = false;
        m_ResetButton.interactable = false;
    }

    // when sort is finished, UI elements should be enabled
    private void EnableUIElements()
    {
        // dropdowns
        m_SortAlgorithmsDropdown.interactable = true;
        m_OrderingDropdown.interactable = true;
        m_ArraySizeSlider.interactable = true;

        // sliders
        m_DelayTimeSlider.interactable = true;

        // buttons
        m_RunButton.interactable = true;
        m_ResetButton.interactable = true;
    }
    #endregion
}