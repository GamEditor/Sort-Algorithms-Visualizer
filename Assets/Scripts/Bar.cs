using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]   // for changing height of bar
[RequireComponent(typeof(Image))]
public class Bar : MonoBehaviour
{
    private RectTransform m_RectTransform;  // for changing height of bar
    private Image m_Image;

    [SerializeField]
    private Text m_ValueText;

    private Color m_OriginalColor;          // starter color

    private int m_Value;
    public int Value
    {
        set
        {
            m_Value = value;

            // changing the height and value text of bar
            m_RectTransform.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x, m_Value);
            m_ValueText.text = m_Value.ToString();
        }

        get => m_Value;
    }

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Image = GetComponent<Image>();

        m_OriginalColor = m_Image.color;
    }

    public void SetColor(Color color)
    {
        m_Image.color = color;
    }

    public void ResetColorToOriginal()
    {
        m_Image.color = m_OriginalColor;
    }
}