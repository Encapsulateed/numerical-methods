using System;
using System.Collections.Generic;

class Program
{
    static (List<double>, List<double>) Direct(List<double> b, List<double> a, List<double> c, List<double> d, int size)
    {
        List<double> alpha = new List<double>() { -c[0] / b[0] };
        List<double> beta = new List<double>() { d[0] / b[0] };

        double y;
        for (int i = 1; i < size - 1; i++)
        {
            y = a[i - 1] * alpha[i - 1] + b[i];
            alpha.Add(-c[i] / y);
            beta.Add((d[i] - a[i - 1] * beta[i - 1]) / y);
        }
        y = a[size - 2] * alpha[size - 2] + b[size - 1];
        beta.Add((d[size - 1] - a[size - 2] * beta[size - 2]) / y);
        return (alpha, beta);
    }

    static List<double> Reverse(List<double> alpha, List<double> beta, int size)
    {
        List<double> x = new List<double>(new double[size]);
        x[size - 1] = beta[size - 1];
        for (int i = size - 2; i >= 0; i--)
        {
            x[i] = alpha[i] * x[i + 1] + beta[i];
        }
        return x;
    }

    static double F(double x)
    {
        return 6 * (x*x) + 1;
    }

    static double de_solution(double x)
    {
        return -0.5*(x*x*x) - 0.375* (x*x) - 0.4375*x + 0.999375*Math.Exp(4*x) - 0.999375;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("МЕТОД ПРОГОНКИ");
        double p = -4;
        double q = 0;
        double a = de_solution(0);
        double b = de_solution(1);

        int n = 40;
        double h = 1.0 / n;
        List<double> xs = new List<double>();
        for (int i = 0; i < n + 1; i++)
        {
            xs.Add(i * h);
        }

        List<double> a_s = new List<double>();
        List<double> bs = new List<double>();
        List<double> cs = new List<double>();
        List<double> ds = new List<double>();

        for (int i = 1; i < n - 1; i++)
        {
            a_s.Add(1 - h / 2 * p);
            cs.Add(1 + h / 2 * p);
        }

        for (int i = 1; i < n; i++)
        {
            bs.Add(h * h * q - 2);
        }

        ds.Add(h * h * F(0) - a * (1 - h / 2 * p));
        for (int i = 2; i < n; i++)
        {
            ds.Add(h * h * F(i * h));
        }
        ds[ds.Count - 1] = h * h * F(ds.Count - 1) - b * (1 + h / 2 * p);

        (List<double> alpha, List<double> beta) = Direct(bs, a_s, cs, ds, ds.Count);
        List<double> ys = new List<double> { a };
        ys.AddRange(Reverse(alpha, beta, ds.Count));
        ys.Add(b);

        Console.WriteLine();
        double maxInaccuracy = 0.0;
        for (int i = 0; i < ys.Count; i+=4)
        {
            double inaccuracy = Math.Abs(ys[i] - de_solution(xs[i]));
            Console.WriteLine($"x={i * h}, y={de_solution(xs[i])}, y*={ys[i]}  |y-y*|={inaccuracy}\n");
            if (inaccuracy > maxInaccuracy)
            {
                maxInaccuracy = inaccuracy;
            }
        }
        Console.WriteLine($"||y-y*||={maxInaccuracy}");
    }
}
