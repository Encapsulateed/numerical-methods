using System;

class Program
{
    static double F(double x)
    {
        return Math.Exp(x);
    }

    static double Analytical(double x)
    {
        return Math.Exp(x);
    }

    static int n = 10;
    static double p = 1.0;
    static double q = -1.0;
    static double a = Analytical(0); // 1.0
    static double b = Analytical(1); // Math.E
    static double[][] ys = new double[2][];

    static double GetC1()
    {
        return (b - ys[0][n]) / ys[1][n];
    }

    static double GetYi(int i)
    {
        return ys[0][i] + GetC1() * ys[1][i];
    }

    static void Main()
    {
        Console.WriteLine("МЕТОД СТРЕЛЬБЫ");
        Console.WriteLine($"y'' + {p}y' + {q}y = exp(x)");
        Console.WriteLine($"y(0) = {a}\ny(1) = {b}");
        Console.WriteLine($"Количество разбиений: {n}");
        double h = 1.0 / n;
        Console.WriteLine(h);
        double delta = h * 100;
        double[] xs = new double[n + 1];
        for (int i = 0; i <= n; i++)
        {
            xs[i] = i * h;
        }
        ys[0] = new double[n + 1];
        ys[1] = new double[n + 1];
        ys[0][0] = a;
        ys[0][1] = a + delta;
        ys[1][0] = 0;
        ys[1][1] = delta;

        for (int i = 1; i < n; i++)
        {
            ys[0][i + 1] = (h * h * F(xs[i]) + (2.0 - q * h * h) * ys[0][i] - (1.0 - h / 2 * p) * ys[0][i - 1]) / (1 + h / 2 * p);
            ys[1][i + 1] = ((2.0 - q * h * h) * ys[1][i] - (1.0 - h / 2 * p) * ys[1][i - 1]) / (1 + h / 2 * p);
        }

        double[] y = new double[n + 1];
        for (int i = 0; i <= n; i++)
        {
            y[i] = GetYi(i);
        }
        Console.WriteLine(y.Length);
        for (int i = 0; i < y.Length; i++)
        {
            Console.WriteLine($"x={xs[i]:F1}, y={Analytical(xs[i]):F6}, y*={y[i]:F6}  |y-y*|={Math.Abs(y[i] - Analytical(xs[i])):F6}");
        }
    }
}
