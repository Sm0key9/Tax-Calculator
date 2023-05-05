using UnityEngine;
using SpeechLib;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class TaxCalculator : MonoBehaviour
{
    //Quinn è simpatico!
    // Constant rate for the Medicare Levy
    const double MEDICARE_LEVY = 0.02;
    public TMP_Dropdown dropdown;
    // Variables
    bool textToSpeechEnabled = true;
    public int stage = 0;

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
        // Initialisation of variables
        double medicareLevyPaid = 0;
        double incomeTaxPaid = 0;

        // Input
        double grossSalaryInput = GetGrossSalary();
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
        double grossYearlySalary = 1000;
        return grossYearlySalary;
    }

    private string GetSalaryPayPeriod()
    {
        // Get from user. E.g. combobox or radio buttons
        string salaryPayPeriod = "weekly";
        return salaryPayPeriod;
    }

    private double CalculateGrossYearlySalary(double grossSalaryInput, string salaryPayPeriod)
    {
        // This is a stub, replace with the real calculation and return the result
        double grossYearlySalary = 50000;
        return grossYearlySalary;
    }

    private double CalculateNetIncome(double grossYearlySalary, ref double medicareLevyPaid, ref double incomeTaxPaid)
    {
        // This is a stub, replace with the real calculation and return the result
        medicareLevyPaid = CalculateMedicareLevy(grossYearlySalary);
        incomeTaxPaid = CalculateIncomeTax(grossYearlySalary);
        double netIncome = 33000;        
        return netIncome;
    }

    private double CalculateMedicareLevy(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result
        double medicareLevyPaid = 2000;        
        return medicareLevyPaid;
    }

    private double CalculateIncomeTax(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result
        double incomeTaxPaid = 15000;
        return incomeTaxPaid;
    }

    private void OutputResults(double medicareLevyPaid, double incomeTaxPaid, double netIncome)
    {
        // Output the following to the GUI
        // "Medicare levy paid: $" + medicareLevyPaid.ToString("F2");
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
}
