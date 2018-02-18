#include <iostream>
#include <vector>

using namespace std;

void versionOne()
{
    cout << endl << "versionOne:" << endl;
    cout << "input sizeArray: ";
    int sizeArray = 0;
    cin >> sizeArray;

    cout << "input array: ";
    int const maxSizeArray = 100;
    int *arr = new int[maxSizeArray]{};
    for (int i = 0; i < sizeArray; ++i)
    {
        cin >> arr[i];
    }

    for (int i = 0; i < sizeArray; ++i)
    {
        for (int j = 1; j < sizeArray; ++j)
        {
            if (arr[j - 1] > arr[j])
            {
                swap(arr[j - 1], arr[j]);
            }
        }
    }

    cout << "sorted array: ";
    for (int i = 0; i < sizeArray; ++i)
    {
        cout << arr[i] << " ";
    }
    delete[] arr;
}

void versionTwo()
{
    cout << endl << "versionTwo:" << endl;
    cout << "input sizeArray: ";
    int sizeArray = 0;
    cin >> sizeArray;

    cout << "input array: ";
    int const maxSizeArray = 100;
    int *arr = new int[maxSizeArray]{};
    int minNegativeDigit = 0;
    int maxDigit = -10000;
    for (int i = 0; i < sizeArray; ++i)
    {
        cin >> arr[i];
        maxDigit = max(arr[i], maxDigit);
        if (arr[i] < minNegativeDigit)
        {
            minNegativeDigit = min(arr[i], minNegativeDigit);
        }
    }
    minNegativeDigit = -minNegativeDigit;
    int const maxSize = 1000000;
    vector<int> countingArray(maxSize,0);
    for (int i = 0; i < sizeArray; ++i)
    {
        countingArray[arr[i] + minNegativeDigit]++;
    }
    delete[] arr;

    cout << "sorted array: ";
    for (int i = 0; i <= maxDigit + minNegativeDigit; ++i)
    {
        for (int j = 0; j < countingArray[i]; ++j)
        {
            cout << i - minNegativeDigit << " ";
        }
    }
}

int main()
{
    cout << "Task: sort the array" << endl << endl;
    cout << "Version sort the array:" << endl;
    cout << "1: bubble" << endl;
    cout << "2: counting" << endl;
    cout << "input 0: close the program" << endl;
    char version = '0';
    cout << endl << "input number of version: ";
    cin >> version;
    while (version != '0')
    {
        switch (version)
        {
            case '1':
            {
                versionOne();
                break;
            }
            case '2':
            {
                versionTwo();
                break;
            }
        }
        cout << endl << endl << "input number of version: ";
        cin >> version;
    }
    return 0;
}
