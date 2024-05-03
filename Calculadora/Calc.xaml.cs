namespace Calculadora;

public partial class Calc : ContentPage
{
	public Calc()
	{
        InitializeComponent();
	}

    private void insertNumber_Clicked(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        float f = float.TryParse(b.Text, out float res) ? res : 0;

        lblResultado.Text = lblResultado.Text == "0" ? f.ToString() : lblResultado.Text + f.ToString();
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Button b = (Button)sender;

        if(b == btnAC)
        {
            lblHistorico.Text = "0";
            lblResultado.Text = "0";
        }
        else if(b == btnC)
        {
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