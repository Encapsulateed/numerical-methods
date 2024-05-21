using System;
using System.Numerics;

public class FftFunction
{
    private readonly Complex[] coefs;

    public FftFunction(Complex[] coefs)
    {
        this.coefs = coefs;
    }

    public Complex CalсAt(double x)
    {
        Complex res = Complex.Zero;

        for (int q = 0; q < coefs.Length; q++)
        {
            res += coefs[q] * Complex.Exp(-2 * Math.PI * Complex.ImaginaryOne * q * x);
        }

        return res;
    }
}

public class Program
{
    private  static int r = 7;
    private static int N = Convert.ToInt32(Math.Pow(2,r));

    private static double Func(double x)
    {
        return Math.Sin(Math.Abs(x - 0.5));
    }

    private static FftFunction FFT((double, double)[] points)
    {
        Complex[] Aq = new Complex[N];

        for (int q = 0; q < N; q++)
        {
            Complex[] APrev = new Complex[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                APrev[i] = points[i].Item2;
            }

            for (int m = 1; m <= r; m++)
            {
                Complex[] A = new Complex[APrev.Length / 2];

                for (int k = 0; k < APrev.Length / 2; k++)
                {
                    A[k] = 0.5 * (
                        Complex.Exp(-2 * Math.PI * Complex.ImaginaryOne * 0 * (1.0 / Math.Pow(2, m)) * (q % (int)Math.Pow(2, m))) * APrev[k] +
                        Complex.Exp(-2 * Math.PI * Complex.ImaginaryOne * 1 * (1.0 / Math.Pow(2, m)) * (q % (int)Math.Pow(2, m))) * APrev[k + APrev.Length / 2]);
                }

                APrev = A;
            }

            Aq[q] = APrev[0];
        }

        return new FftFunction(Aq);
    }

    public static void Main()
    {
        (double, double)[] points = new (double, double)[N];
        for (int j = 0; j < N; j++)
        {
            points[j] = (j / (double)N, Func(j / (double)N));
        }

        FftFunction fft = FFT(points);

        foreach ((double xj, _) in points)
        {
            var abs = Math.Abs(fft.CalсAt(xj).Real - Func(xj + 0.5 / N));
            Console.WriteLine($"{xj + 0.5 / N} | {fft.CalсAt(xj).Real:F15} | {Func(xj + 0.5 / N):F15} | {abs:F15}");
        }


    }
}
