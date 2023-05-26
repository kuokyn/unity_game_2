using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputField : MonoBehaviour
{

    public static InputField instance;

    NumberField lastField;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void ActivateInputField(NumberField field)
    {
		this.gameObject.SetActive(true);
        lastField = field;
	}

    public void ClickedInput(int number)
    {
        lastField.ReceiveInput(number);
        this.gameObject.SetActive(false);
    }

}
