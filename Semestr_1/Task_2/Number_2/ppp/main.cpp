#include <iostream>

using namespace std;
double versionOne(int num, int deg)
{
    double result = 1;
    bool sign = false;
    if (deg < 0)
    {
        deg = -deg;
        sign = true;
    }
    for (int i = 0; i < deg; ++i)
    {
        result *= num;
    }
    if (!sign)
    {
        return result;
    } else
    {
        return (1 / result);
    }
}

double versionTwo(long long num, int deg)
{
    double result = 1;
    bool sign = false;
    if (deg < 0)
    {
        deg = -deg;
        sign = true;
    }
    while (deg)
    {
        if (deg % 2 == 0)
        {
            deg /= 2;
            num *= num;
        } else
        {
            deg--;
            result *= num;
        }
    }
    if (!sign)
    {
        return result;
    } else
    {
        return (1 / result);
    }
}

int main()
{
    cout << "Task: the number of degree" << endl << endl;

    int num = 0;
    cout << "input number: ";
    cin >> num;
    int deg = 0;
    cout << "input degree: ";
    cin >> deg;

    {
        cout << endl << "version of the solution:" << endl;
        cout << "version 1: complexity = O(deg)" << endl;
        cout << "version 2: complexity = O(log(deg))" << endl;
        cout << "(input 0: close program)"  << endl;
    }

    char version = '0';
    cout << endl << "input version of the solution: ";
    cin >> version;
    while (version != '0')
    {
        switch (version)
        {
            case '1':
            {
                cout << "result: " << versionOne(num, deg) << endl;
                break;
            }
            case '2':
            {
                cout << "result: " << versionTwo(num, deg) << endl;
                break;
            }
        }
        cout << endl << "input version of the solution: ";
        cin >> version;
    }
    return 0;
}
