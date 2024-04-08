
import math
import numpy as np
from tabulate import tabulate

def write_array_to_file(out, filename):
    formatted_out = map(lambda x: list(map(str, x)), out)

    # Форматируем таблицу с помощью tabulate
    table = tabulate(formatted_out, tablefmt="grid")

    # Открываем файл для записи
    with open(filename, "w") as file:
        # Записываем таблицу в файл
        file.write(table)

def calculate_table(count, a: int, h: int, x):
    out= []
    aa = [[]]
    b, c, d = cubic_spline_interpolation(xs, ys,count)

    x = a
    for i in range(count):
        new_y = evaluate_cubic_spline(xs,ys,b,c,d,x,count)
        f =  2*x*math.cos(x / 2)
        g = new_y
        out[i] = [x,f,g,abs(f-g)]
        print(i)
        x += h
        aa.append(out)
        print(aa)
   # write_array_to_file(aa,'table.txt')
def cubic_spline_interpolation(x, y,n):
    h = np.diff(x)
    delta_y = np.diff(y)
    
    # Вычисляем вторые производные
    alpha = np.zeros(n)
    for i in range(1, n-1):
        alpha[i] = (3/h[i])*(delta_y[i]/h[i] - delta_y[i-1]/h[i-1])
        
    l = np.zeros(n)
    mu = np.zeros(n)
    z = np.zeros(n)
    l[0] = 1

    for i in range(1, n-1):
        l[i] = 2*(x[i+1] - x[i-1]) - h[i-1]*mu[i-1]
        mu[i] = h[i]/l[i]
        z[i] = (alpha[i] - h[i-1]*z[i-1])/l[i]
        
    l[n-1] = 1
    z[n-1] = 0
    c = np.zeros(n)
    b = np.zeros(n)
    d = np.zeros(n)
    
    for j in range(n-2, -1, -1):
        c[j] = z[j] - mu[j]*c[j+1]
        b[j] = (delta_y[j]/h[j]) - h[j]*(c[j+1] + 2*c[j])/3
        d[j] = (c[j+1] - c[j])/(3*h[j])
        
    return b, c, d

def evaluate_cubic_spline(x, y, b, c, d, x_eval,count):
    """
    Вычисляет значение кубического сплайна в произвольной точке.

    Параметры:
        x: список или массив значений x.
        y: список или массив соответствующих значений y.
        b, c, d: коэффициенты сплайнов.
        x_eval: значение x, для которого нужно вычислить значение y.

    Возвращает:
        Значение y в точке x_eval.
    """
    n = count
    for i in range(n-1):
        if x[i] <= x_eval <= x[i+1]:
            dx = x_eval - x[i]
            return y[i] + b[i]*dx + c[i]*dx**2 + d[i]*dx**3

if __name__ == "__main__":
    # func = 2xcos(x/2)

    a = int(input('a = '))
    b = int(input('b = '))
    
    len = abs(b-a)
    
    dot_count = int(input('число точек = '))
    
    h = len/dot_count
    
    xs = []
    ys = []

    x_pos = a
    for  i in range(0,dot_count+1):
        xs.append(x_pos)
        ys.append(2*x_pos*math.cos(x_pos / 2))
        x_pos+=h
    # Вычисляем коэффициенты сплайнов
    b, c, d = cubic_spline_interpolation(xs, ys,dot_count)
   # new_x_pos = 28

    #new_y = evaluate_cubic_spline(xs,ys,b,c,d,new_x_pos,dot_count)
   # print(new_y)
    calculate_table(dot_count,a,h,xs)






