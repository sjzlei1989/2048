using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    /// <summary>
    /// 块的值, 两个相同值得块可以加到一起
    /// </summary>
    private int _cellValue = 0;
    public int cellValue {
        get {
            return _cellValue;
        }
        set {
            _cellValue = value;
            if(0 == value) {
                img.enabled = false;
                txt.enabled = false;
            }
            else {
                img.enabled = true;
                img.color = new Color(255.0f / 8192.0f * value, 255.0f / 8192.0f * value, 255.0f / 8192.0f * value);
                txt.enabled = true;
                txt.text = value.ToString();
            }
        }
    }

    /// <summary>
    /// 横坐标位置
    /// </summary>
    public int cellX = 0;

    /// <summary>
    /// 纵坐标位置
    /// </summary>
    public int cellY = 0;

    public Image img;
    public Text txt;
}
