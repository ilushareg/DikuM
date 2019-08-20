using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinimalisticTelnet;
using System.Text;

public class testTelnet : MonoBehaviour
{
    // Start is called before the first frame update
    UnityEngine.UI.Text text = null;
    UnityEngine.UI.Text input = null;

    TelnetConnection tc = null;
    string prompt = "";

    private void Start()
    {
        text = UnityEngine.GameObject.Find("Text111").GetComponent<UnityEngine.UI.Text>();
        input = UnityEngine.GameObject.Find("InputField").GetComponentInChildren<UnityEngine.UI.Text>();
        input.text = "test";

        TestTelnet();

    }
    private void Awake()
    {

    }

    void CWrite(string s)
	{
        if(text != null && s.Length > 0)
        { 
            text.text += s;
        }
    }

    void TestTelnet()
	{
		//create a new telnet connection to hostname "gobelijn" on port "23"
		tc = new TelnetConnection("localhost", 4000);
	}

	// Update is called once per frame
	void Update()
    {

        if (tc.IsConnected && prompt.Trim() != "exit")
        {

            // display server output
            CWrite(tc.Read());

            // send client input to server
            if (prompt.Length > 0)
            {
                //var utf8bytes = Encoding.UTF8.GetBytes(prompt);
                //var win1252Bytes = Encoding.Convert(Encoding.UTF8, Encoding.ASCII, utf8bytes);
                //foreach (var item in win1252Bytes)
                //{
                //    tc.Write(item + " ");
                //}

                //tc.Write("\n\r");
                tc.Write(prompt + "\n\r");
                prompt = "";
            }

            // display server output
            CWrite(tc.Read());
        }

        //Console.ReadLine();

    }


    public void OnSend()
    {
        Debug.Log(input.text as string);
    }

    public void OnClear()
    {
        text.text = "";

    }

    public void OnHide()
    {
        bool active = !text.transform.gameObject.active;
        text.transform.gameObject.SetActive(active);
    }

    public void OnTextChanged()
    {

    }
    public void OnTextChangedComplete()
    {
        string s = input.text;
        Debug.Log(s);
        prompt = input.text;
    }
}
