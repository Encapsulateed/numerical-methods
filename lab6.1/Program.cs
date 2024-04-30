namespace lab6._1
{
    public class Program
    {
        static decimal eps = 0.001m;

        static decimal[] coeffs = { 2.0m, 0.0m, -9.0m, 1.0m };

        static decimal f(decimal x)
        {
            return coeffs[0] * (decimal)Math.Pow((double)x, 3.0) +
                   coeffs[1] * (decimal)Math.Pow((double)x, 2.0) +
                   coeffs[2] * x +
                   coeffs[3];
        }

        static decimal derivative_f(decimal x)
        {
            return 3 * coeffs[0] * (decimal)Math.Pow((double)x, 2.0) +
                   2 * coeffs[1] * x +
                   coeffs[2];
        }

        static decimal second_derivative_f(decimal x)
        {
            return 6 * coeffs[0] * x +
                   2 * coeffs[1];
        }

        static int sgn(decimal x)
        {
            if (x > 0)
                return 1;
            else if (x < 0)
                return -1;
            return 0;
        }

        static (decimal, int) BisectionMethod(Func<decimal, decimal> f, (decimal, decimal) segment)
        {
            decimal left = segment.Item1;
            decimal right = segment.Item2;
            decimal mid = (left + right) / 2.0m;
            int i = 0;
            while (Math.Abs(f(mid)) > eps)
            {
                if (f(left) * f(mid) < 0)
                    right = mid;
                else if (f(right) * f(mid) < 0)
                    left = mid;
                else
                    return (mid, i);
                i++;
                mid = (left + right) / 2.0m;
            }
            return (mid, i);
        }

        static (decimal, int) NewtonMethod(Func<decimal, decimal> f, (decimal, decimal) segment)
        {
            decimal start = segment.Item1;
            decimal end = segment.Item2;
            if (f(end) * second_derivative_f(end) > 0)
                start = end;
            decimal prev = start;
            decimal cur = start;
            int i = 0;
            while (f(cur) * f(cur + sgn(cur - prev) * eps) >= 0)
            {
                prev = cur;
                cur = cur - f(cur) / derivative_f(cur);
                i++;
            }
            return (cur, i);
        }

        public static void Main(string[] args)
        {
            (decimal, decimal)[] segments = { (-3.0m, -2.0m), (0.05m, 0.5m), (1.0m, 3.0m) };

            foreach (var segment in segments)
            {
                var bisectionResult = BisectionMethod(f, segment);
                var newtonResult = NewtonMethod(f, segment);
                Console.WriteLine("bisection: " + bisectionResult.Item1 + " iters: " + bisectionResult.Item2);
                Console.WriteLine("newton: " + newtonResult.Item1 + " iters: " + newtonResult.Item2);
                Console.WriteLine("diff: " + Math.Abs(bisectionResult.Item1 - newtonResult.Item1));
            }
        }
    }
}