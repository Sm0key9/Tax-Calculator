using UnityEngine;
using SpeechLib;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using System;

public class TaxCalculator : MonoBehaviour
{
    //Quinn è no simpatico
    // Constant rate for the Medicare Levy
    const double MEDICARE_LEVY = 0.02;
    public TMP_Dropdown dropdown;
    // Variables
    public bool textToSpeechEnabled = true;
    public int stage = 0;
    public TMP_InputField grosssalaryinput;
    public TextMeshProUGUI NetIncome;
    public TextMeshProUGUI TaxPaid;
    public TextMeshProUGUI LevyPaid;

    private void Start()
    {
        Speak("Welcome to the A.T.O. Tax Calculator Press 1 for weekly. Press 2 for monthly. or Press 3 for Yearly");
        stage++;
       

    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        if (stage == 1)
        {
            string payperiod = "";
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Speak("You selected Weekly.");
                dropdown.value = 0;
                payperiod = "weekly";
                Speak("Please enter your " + payperiod + " income and press enter");
                stage = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                dropdown.value = 1;
                Speak("You selected Monthly.");
                payperiod = "monthly";
                Speak("Please enter your " + payperiod + " income and press enter");
                stage = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                dropdown.value = 2;
                Speak("You selected Yearly.");
                payperiod = "yearly";
                Speak("Please enter your " + payperiod + " income and press enter");
                stage = 2;
            }


        }
        else if (stage == 2)
        {
            Speak("Welcome to stage 2");
        }
    }
    // Run this function on the click event of your 'Calculate' button
    public void Calculate()
    {
        GetSalaryPayPeriod();
        // Initialisation of variables
        double medicareLevyPaid = 0;
        double incomeTaxPaid = 0;

        // Input
        double grossSalaryInput = GetGrossSalary();
        print(grossSalaryInput);
        string salaryPayPeriod = GetSalaryPayPeriod();

        // Calculations
        double grossYearlySalary = CalculateGrossYearlySalary(grossSalaryInput, salaryPayPeriod);

        double netIncome = CalculateNetIncome(grossYearlySalary, ref medicareLevyPaid, ref incomeTaxPaid);

        // Output
        OutputResults(medicareLevyPaid, incomeTaxPaid, netIncome);
    }

    private double GetGrossSalary()
    {
        // Get from user. E.g. input box
        // Validate the input (ensure it is a positive, valid number)
        print("!" + grosssalaryinput.text + "!");
        double grossYearlySalary = 12;
        if (double.TryParse((grosssalaryinput.text), out grossYearlySalary))
        {
            return grossYearlySalary;
        }
        else { return 0; }
    }

    private string GetSalaryPayPeriod()
    {
        // Get from user. E.g. combobox or radio buttons
        string salaryPayPeriod = dropdown.value.ToString();
        return salaryPayPeriod;
    }

    private double CalculateGrossYearlySalary(double grossSalaryInput, string salaryPayPeriod)
    {
        // This is a stub, replace with the real calculation and return the result
        double grossYearlySalary = 50000;
        if (int.Parse(salaryPayPeriod) == 0)
        {
            grossYearlySalary = grossSalaryInput * 52;    
        }
        else if (int.Parse(salaryPayPeriod) == 1)
        {
            grossYearlySalary = grossSalaryInput * 12;
        }
        else
        {
            grossYearlySalary = grossSalaryInput;
        }
        return grossYearlySalary;
    }

    private double CalculateNetIncome(double grossYearlySalary, ref double medicareLevyPaid, ref double incomeTaxPaid)
    {
        // This is a stub, replace with the real calculation and return the result
        medicareLevyPaid = CalculateMedicareLevy(grossYearlySalary);
        incomeTaxPaid = CalculateIncomeTax(grossYearlySalary);
        double netIncome = grossYearlySalary-medicareLevyPaid-incomeTaxPaid;        
        return netIncome;
    }

    private double CalculateMedicareLevy(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result
        double medicareLevyPaid = grossYearlySalary*0.02;        
        return medicareLevyPaid;
    }

    private double CalculateIncomeTax(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result
        double incomeTaxPaid = 0;
        if (grossYearlySalary <= 18200)
        {
            incomeTaxPaid = 0;
        }
        else if (grossYearlySalary > 18200 && grossYearlySalary <= 45000)
        {
            incomeTaxPaid = (grossYearlySalary - 18200) * 0.19;
        }
        else if (grossYearlySalary > 45000 && grossYearlySalary <= 120000)
        {
            incomeTaxPaid = (((grossYearlySalary - 45000) * 0.325)+5092);
        }
        else if (grossYearlySalary > 120000 && grossYearlySalary <= 180000)
        {
            incomeTaxPaid = (grossYearlySalary-120000)*0.37+29467;
        }
        else
        {
            incomeTaxPaid = (grossYearlySalary - 180000) * 0.45 + 51667;
        }
        //double incomeTaxPaid = 15000;
        return incomeTaxPaid;
    }

    private void OutputResults(double medicareLevyPaid, double incomeTaxPaid, double netIncome)
    {
        // Output the following to the GUI

        // "Medicare levy paid: $" + medicareLevyPaid.ToString("F2");
        NetIncome.text = ($"Net Income: {netIncome}");
        TaxPaid.text = ($"Income Tax Paid: {incomeTaxPaid}");
        LevyPaid.text = ($"Medicare Levy Paid: {medicareLevyPaid}");
        // "Income tax paid: $" + incomeTaxPaid.ToString("F2");
        // "Net income: $" + netIncome.ToString("F2");
    }

    // Text to Speech
    public async void Speak(string textToSpeech)
    {
        if(textToSpeechEnabled)
        {
            SpVoice voice = new SpVoice();
            await SpeakAsync(textToSpeech);
        }
    }

    private Task SpeakAsync(string textToSpeak)
    {
        return Task.Run(() =>
        {
            SpVoice voice = new SpVoice();
            voice.Speak(textToSpeak);
        });
    }
    // Gryyfyynen ar en fisk
}
