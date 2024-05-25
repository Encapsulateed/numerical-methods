using System;

class Program
{
    static void Main()
    {
        double eps = 0.001;
        Tuple<double, double> analyticalMin = AnalyticalMin();
        Console.WriteLine("Аналитический минимум: ({0}, {1})", analyticalMin.Item1, analyticalMin.Item2);

        int k = 0;
        double xk = 0.0, yk = 0.0;

        while (Math.Max(DerivativeX(xk, yk), DerivativeY(xk, yk)) >= eps)
        {
            double phi1 = -Math.Pow(DerivativeX(xk, yk), 2) - Math.Pow(DerivativeY(xk, yk), 2);
            double phi2 = Derivative2X(xk, yk) * Math.Pow(DerivativeX(xk, yk), 2) +
                          2 * DerivativeXY(xk, yk) * DerivativeX(xk, yk) * DerivativeY(xk, yk) +
                          Derivative2Y(xk, yk) * Math.Pow(DerivativeY(xk, yk), 2);
            double tStar = -phi1 / phi2;

            xk = xk - tStar * DerivativeX(xk, yk);
            yk = yk - tStar * DerivativeY(xk, yk);
            k++;
        }

        Console.WriteLine($"Метод наискорейшего спуска: ({xk}, {yk})");
        Console.WriteLine($"Абсолютная разница ({Math.Abs(analyticalMin.Item1 - xk)}, {Math.Abs(analyticalMin.Item2 - yk)})");
    }

    static double Function(double x, double y)
    {
        return (x*x)+2*(y*y)+2*x+0.3*Math.Atan(x*y);
    }

    static double DerivativeX(double x, double y)
    {
        double h = 1e-5;
        return (Function(x + h, y) - Function(x, y)) / h;
    }

    static double DerivativeY(double x, double y)
    {
        double h = 1e-5;
        return (Function(x, y + h) - Function(x, y)) / h;
    }

    static double Derivative2X(double x, double y)
    {
        double h = 1e-5;
        return (DerivativeX(x + h, y) - DerivativeX(x, y)) / h;
    }

    static double Derivative2Y(double x, double y)
    {
        double h = 1e-5;
        return (DerivativeY(x, y + h) - DerivativeY(x, y)) / h;
    }

    static double DerivativeXY(double x, double y)
    {
        double h = 1e-5;
        return (DerivativeX(x, y + h) - DerivativeX(x, y)) / h;
    }

    static Tuple<double, double> AnalyticalMin()
    {
        return Tuple.Create(-1.01124535766275, 0.0754049591595788);
    }
}
