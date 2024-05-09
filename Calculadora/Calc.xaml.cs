using System.Data;

namespace Calculadora;

public partial class Calc : ContentPage
{
	public Calc()
	{
        InitializeComponent();
	}

    int contador = 0;

    private void calc_Clicked(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        float f = float.TryParse(b.Text, out float res) ? res : 0;

        if (f != 0)
        {
            lblResultado.Text = lblResultado.Text == "0" ? f.ToString() : lblResultado.Text + f.ToString();
            if (contador >= 1)
                result_Clicked();
        }
        else
        {
            if(lblResultado.Text.Substring(lblResultado.Text.Length - 1) == "+" ||
                lblResultado.Text.Substring(lblResultado.Text.Length - 1) == "-" ||
                lblResultado.Text.Substring(lblResultado.Text.Length - 1) == "×" ||
                lblResultado.Text.Substring(lblResultado.Text.Length - 1) == "÷")
            {
                lblResultado.Text = lblResultado.Text.Substring(0, lblResultado.Text.Length - 1) + b.Text;
            }
            else
            {
                lblResultado.Text += b.Text;
                contador++; 
            }
        }
    }

    private void result_Clicked()
    {
        var calcular = lblResultado.Text.Replace("×", "*").Replace("÷", "/");
        double resultado = Convert.ToDouble(new DataTable().Compute(calcular, null));

        lblHistorico.Text = resultado.ToString();
    }

    private void btnIgual_Clicked(object sender, EventArgs e)
    {
        if(lblHistorico.Text != "")
        {
            lblResultado.Text = lblHistorico.Text;
            lblHistorico.Text = "";
        }
        
    }


    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Button b = (Button)sender;

        if(b == btnAC || b == btnC)
        {
            lblHistorico.Text = "0";
            lblResultado.Text = "0";
        }
        else
        {
            if(lblResultado.Text != "0")
            {
                lblResultado.Text = lblResultado.Text.Remove(lblResultado.Text.Length - 1);

                if (string.IsNullOrEmpty(lblResultado.Text))
                {
                    lblResultado.Text = "0";
                }
            }

        }

    }

    private void btnDecimal_Clicked(object sender, EventArgs e)
    {
        if(lblResultado.Text.Contains(",") is false)
        {
            lblResultado.Text += ".";
        }
    }



    //Métodos que não estão sendo utilizados
    private void btnOperador_Clicked(object sender, EventArgs e)
    {
        Button b = ( Button )sender;

        if (lblHistorico.Text == "0")
        {
            lblHistorico.Text = string.Format("{0} {1}", lblResultado.Text, b.Text);
        }
        else
        {
            lblHistorico.Text = string.Format("{0} {1} {2}", lblHistorico.Text, lblResultado.Text, b.Text);
        }
        lblResultado.Text = "0";

    }
    private void insertNumber_Clicked(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        float f = float.TryParse(b.Text, out float res) ? res : 0;

        lblResultado.Text = lblResultado.Text == "0" ? f.ToString() : lblResultado.Text + f.ToString();
    }
    private void btnResultado_Clicked(object sender, EventArgs e)
    {
        string final = lblHistorico.Text + " " + lblResultado.Text;
        string[] arr = final.Split(" ");

        float resultado = 0;
        float tempResult = 0;
        string temp = string.Empty;

        for (int i = 0; i < arr.Length; i++)
        {
            if(i % 2 == 0)
            {
                tempResult = float.TryParse(arr[i], out float result) ? result : 0;
                if (resultado == 0)
                {
                    resultado = tempResult;
                }
                if (!string.IsNullOrEmpty(temp))
                {
                    switch (temp)
                    {
                        case "+":
                            resultado += tempResult;
                            break;

                        case "-":
                            resultado -= tempResult;
                            break;

                        case "×":
                            resultado *= tempResult;
                            break;

                        case "÷":
                            resultado /= tempResult;
                            break;
                    }
                }
                temp = string.Empty;
            }
            else
            {
                temp = arr[i];
            }
        }
        lblResultado.Text = resultado.ToString();
        lblHistorico.Text = "0";
    }
}