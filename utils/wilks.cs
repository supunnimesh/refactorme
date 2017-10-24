using System;

public static class Wilks
{
    // https://en.wikipedia.org/wiki/Wilks_Coefficient
    // Online Wilks calculator http://wilkscalculator.com/kg 
    public decimal CalculateWilks(double bodyWeight, bool isMale)
    {
        var a = -216.0475144;
        var b = 16.2606339;
        var c = -0.002388645;
        var d = -0.00113732;
        var e = 0.00000701863;
        var f = -0.00000001291;

        if(!isMale) {
            a = 594.31747775582;
            b = -27.23842536447;
            c = 0.82112226871;
            d = -0.00930733913;
            e = 0.00004731582;
            f = -0.00000009054;
        }

        var coefficient = (500 / 
			(
				a + 
				b * bodyWeight + 
				c * Math.Pow(bodyWeight, 2) + 
				d * Math.Pow(bodyWeight, 3) + 
				e * Math.Pow(bodyWeight, 4) + 
				f * Math.Pow(bodyWeight, 5)
			)
		);

        return coefficient;
    }
}