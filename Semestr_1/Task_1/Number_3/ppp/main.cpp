#include <iostream>

using namespace std;

int main()
{
    int m = 0, n = 0;
    cout << "Size cut_M: ";
    cin >> m;
    cout << "Size cut_N: ";
    cin >> n;

    int inputArray[10000];
    cout << "Elements inputArray: ";
    for (int i = 0; i < m + n; i++)
    {
        cin >> inputArray[i];
    }

    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < m + n - 1; j++)
        {
            swap(inputArray[j],inputArray[j+1]);
        }
    }
    cout << "Elements outputArray: ";
    for (int i = 0; i < m + n; i++)
    {
        cout << inputArray[i] << " ";
    }
    return 0;
}
