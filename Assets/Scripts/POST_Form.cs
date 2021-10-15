using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Simple.Json;

public class POST_Form : MonoBehaviour
{
    [SerializeField] TMP_InputField displayTextName;
    [SerializeField] TMP_InputField displayTextEmail;
    [SerializeField] TMP_InputField displayTextBirthday;
    private string url = "https://sweetbonus.com.br/sweet-juice/trainee-test/submit?"; 

    public void SendForm()
    {
        string name = displayTextName.text;
        string email =  displayTextEmail.text;
        string birthdate = displayTextBirthday.text;
        StartCoroutine(Upload(name , email, birthdate));
        Debug.Log("Form upload complete! " + name + " " + email + " " + birthdate );
    }

    IEnumerator Upload(string name, string email, string birthdate)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("candidate", "teste");
        formData.AddField("fullname", name);
        formData.AddField("email", email);
        formData.AddField("birthdate", birthdate);

        UnityWebRequest www = UnityWebRequest.Post(url, formData);
        yield return www.SendWebRequest();
    
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        } 
        else
        {
            Debug.Log("Form upload complete!");
            var weather = JsonUtility.(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
        } 
    
    }

    private bool EmailVerify(string email)
    {
        bool validate = email.Contains("@") && email.Contains(".com");
        if (validate) return true;
        else return false;
    }
    private bool DateVerify(string birthdate)
    {
        string[] date = date.split("-");

        return true;
    }
}

