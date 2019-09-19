#include <iostream>
#include <vector>


using namespace std;

int main()
{
    int n = 0;
    cout << "Input n: ";
    cin >> n;

    vector <bool> SostCh (n + 1, true); //Состояние чисел
    int primeNumber = 1;
    bool check = true; //показатель нахождения простого числа
    cout << "Prime numbers to n: ";
    while (primeNumber != n && check == true)
    {
        check = false;
        for (int j = primeNumber + 1; j <= n; j++)
        {
             if (SostCh[j] == true)
             {
                 cout << j << " ";
                 check = true;
                 primeNumber = j;
                 break;
             }

        }
        if (primeNumber * primeNumber <= n)
        {
            for (int i = primeNumber * primeNumber; i <= n; i+=primeNumber)
            {
                SostCh[i] = false;
            }
        }
    }
    return 0;
}
