using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberField : MonoBehaviour
{
    Board board;

    int x1;
    int y1;
    int value;
    string ID;

    public TextMeshProUGUI number;

    public void SetValues(int _x1, int _y1, int _value, string _ID, Board _board)
    {
        x1 = _x1;
        y1 = _y1;
        value = _value;
        ID = _ID;
		board = _board;

        // расставляем цифры на поле
        number.text = (value != 0) ? value.ToString() : "";
		if (value != 0)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            number.color = Color.black;
        }

	}

    public void ButtonClick()
    {
        InputField.instance.ActivateInputField(this);

    }

    public void ReceiveInput(int newValue)
    {
        value = newValue;
        number.text = (value != 0) ? value.ToString() : "";
        board.SetInputRiddleGrid(x1, y1, value);
    }

    public int  GetX()
    {
        return x1;
    }

	public int GetY()
	{
		return y1;
	}

    public void SetHint(int _value)
    {
        value = _value;
        number.text = value.ToString();
        number.color = Color.green;
        GetComponentInParent<Button>().interactable = false;
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
