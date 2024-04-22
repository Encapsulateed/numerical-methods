using System;

class NewtonMethod
{
    static double f1(double x, double y)
    {
        return  Math.Sin(x+1) - y - 1; // sin(x+1) - y - 1 =0
    }

    static double f2(double x, double y)
    {
        return 2*x + Math.Cos(y) - 2; // 2x + cos(y) - 2 =0
    }

    static double df1_dx(double x, double y)
    {
        return Math.Cos(x+1);
    }

    static double df2_dx(double x, double y)
    {
        return 2;
    }

    static double df1_dy(double x, double y)
    {
        return -1;
    }

    static double df2_dy(double x, double y)
    {
        return -Math.Sin(y);
    }

    static void NewtonMethodSolver(double x0, double y0, double eps)
    {
        double x = x0;
        double y = y0;
        double dx, dy;

        for (int i = 0; i < 100; i++) // Ограничим количество итераций
        {
            double J11 = df1_dx(x, y);
            double J12 = df1_dy(x, y);
            double J21 = df2_dx(x, y);
            double J22 = df2_dy(x, y);

            double detJ = J11 * J22 - J12 * J21;

            double invJ11 = J22 / detJ;
            double invJ12 = -J12 / detJ;
            double invJ21 = -J21 / detJ;
            double invJ22 = J11 / detJ;

            dx = -(invJ11 * f1(x, y) + invJ12 * f2(x, y));
            dy = -(invJ21 * f1(x, y) + invJ22 * f2(x, y));

            x += dx;
            y += dy;

            // Проверяем на невалидные значения
            if (double.IsNaN(x) || double.IsNaN(y) || double.IsInfinity(x) || double.IsInfinity(y))
            {
                Console.WriteLine("Метод расходится. Выберите другие начальные значения.");
                return;
            }

            if (Math.Abs(dx) < eps && Math.Abs(dy) < eps)
            {
                Console.WriteLine($"x = {x}, y = {y}");
                Console.WriteLine($"Кол-во итераций: {i}");
                return;
            }
        }

        Console.WriteLine("Не удалось найти корни. Попробуйте другие начальные значения или увеличить количество итераций.");
    }

    static void Main(string[] args)
    {
        double x0 = 0; // Начальное приближение для x
        double y0 = 0; // Начальное приближение для y
        double eps = 1e-5; // Точность

        NewtonMethodSolver(x0, y0, eps);
    }
}
