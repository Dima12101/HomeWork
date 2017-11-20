#include <iostream>
#include <cmath>

using namespace std;

int main()
{
    int dividend = 0;
    cout << "dividend: ";
    cin >> dividend;
    int divisor = 0;
    cout << "divisor: ";
    cin >> divisor;

    int sign = 1;
    if (dividend < 0)
    {
        dividend = abs(dividend);
        sign *= -1;
    }
    if (divisor < 0)
    {
        divisor = abs(divisor);
        sign *= -1;
    }

    int result = 0;
    while (dividend >= divisor)
    {
        dividend -= divisor;
        result++;
    }
    cout << "result: " << result * sign;
    return 0;
}
