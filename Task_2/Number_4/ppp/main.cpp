#include <iostream>
#include <cstdlib>
using namespace std;

int main()
{
    cout << "input sizeArray: ";
    int sizeArray = 0;
    cin >> sizeArray;

    cout << "input array: ";
    int const maxSizeArray = 100;
    int *arr = new int[maxSizeArray]{};
    int firstElement = 0;
    int largeCounter = 0;
    for (int i = 0; i < sizeArray; ++i)
    {
        arr[i] = 5 + (rand() % 10);
        //cin >> arr[i];
        cout << arr[i] << " ";
        if (i == 0)
        {
            firstElement  = arr[i];
        }else
        {
            if (arr[i] >= firstElement)
            {
                largeCounter++;
            }
        }

    }
    int rearIndex = sizeArray - 1;
    bool checkExit = false;
    for (int i = 1; i < sizeArray - largeCounter; ++i)
    {
        if (arr[i] < firstElement)
        {
            swap(arr[i], arr[i - 1]);
        }else
        {
            while (arr[rearIndex] >= firstElement)
            {
                if ((sizeArray - 1) - rearIndex == largeCounter)
                {
                    checkExit = true;
                    break;
                }
                rearIndex--;
            }
            if (checkExit == false)
            {
                swap(arr[rearIndex], arr[i]);
                swap(arr[i], arr[i - 1]);
            }
        }
    }
    cout << endl << "output array: ";
    for (int i = 0; i < sizeArray; ++i)
    {
        cout << arr[i] << " ";
    }
    delete[] arr;
    return 0;
}
