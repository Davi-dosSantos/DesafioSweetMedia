using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;
using System.Text.RegularExpressions;

public class POST_Form : MonoBehaviour
{
    
    [SerializeField] GameObject ErrorAlert;
    [SerializeField] GameObject EmailAlert;
    [SerializeField] GameObject DateAlert;
    [SerializeField] GameObject EmailAndDateAlert;
    [SerializeField] GameObject CompleteAlert;

    [SerializeField] TMP_InputField displayTextName;
    [SerializeField] TMP_InputField displayTextEmail;
    [SerializeField] TMP_InputField displayTextBirthday;
    [SerializeField] TextMeshProUGUI ErrorText;
    
    private string url = "https://sweetbonus.com.br/sweet-juice/trainee-test/submit?"; 

    public void SendForm()
    {
        string name = displayTextName.text.Trim();
        string email =  displayTextEmail.text.Trim();
        string birthdate = displayTextBirthday.text.Trim();
        Debug.Log("Valores Recebidos: " + name + " " + email + " " + birthdate);

        bool emailVerify = EmailVerify(email);
        bool dateVerify = DateVerify(birthdate);

        if (emailVerify && dateVerify)
        {
            Debug.Log("Form upload complete! " + name + " " + email + " " + birthdate);
            StartCoroutine(Upload(name, email, birthdate));
        }else
        {
            Alerts(emailVerify, dateVerify);
        }
        
        
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
            ErrorText.text = www.error;
        } 
        else
        {
            Debug.Log("Form upload complete!");
            CompleteAlert.SetActive(true);
        } 
    
    }
    private bool EmailVerify(string email)
    {
        Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

        if (rg.IsMatch(email)) return true;
        else return false;
        
    }
    private bool DateVerify(string birthdate)
    {
        string[] date = birthdate.Split('-');

        Debug.Log(date[0] + " " + date[0].Length + " " + date[1] + " " + date[1].Length + " " + date[2] + " " + date[2].Length );

        int dayInt = date[0].Length;
        int monthInt = date[1].Length;
        int yearInt = date[2].Length;
        if (dayInt == 2 && monthInt == 2 && yearInt == 4)
        {
            Debug.Log("Verificação de tamanho e formato da data completa ");

            int day = Int32.Parse(date[0]);
            int month = Int32.Parse(date[1]);
            int year = Int32.Parse(date[2]);
            if (day != 0 && day <= 31 && month != 0 && month <= 12 && year > 1910 && year < 2015)
            {
                return true;
            }
            else return false;
        }

        return false;
    }

    private void Alerts(bool emailVerify, bool dateVerify)
    {
        if (dateVerify && !emailVerify) EmailAlert.SetActive(true);
        else if (!dateVerify && emailVerify) DateAlert.SetActive(true);
        else if (!dateVerify && !emailVerify) EmailAndDateAlert.SetActive(true);
        
    }

    public void DesativeEmailAndDateAlert()
    {
        EmailAndDateAlert.SetActive(false);
    }

    public void DesativeEmailAlert()
    {
        EmailAlert.SetActive(false);
    }

    public void DesativeDateAlert()
    {
        DateAlert.SetActive(false);
    }
    public void DesativeCompleteAlert()
    {
        CompleteAlert.SetActive(false);
    }
    public void DesativeErrorAlert()
    {
        ErrorAlert.SetActive(false);
    }
    
}

