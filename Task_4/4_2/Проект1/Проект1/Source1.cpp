#include <iostream>
#include <cstdlib>
#include <time.h>
#include "MySort.h"

using namespace std;

void searchAndCount(int arr[], int sizeArray, int &result)
{
	int maxCounter = 0;
	int counter = 0;
	int digit = arr[0];
	for (int i = 0; i < sizeArray; ++i)
	{
		if (arr[i] == digit)
		{
			counter++;
		}
		else
		{
			if (counter > maxCounter)
			{
				result = digit;
				maxCounter = counter;
			}
			digit = arr[i];
			counter = 1;
		}
	}
	if (counter > maxCounter)
	{
		result = digit;
		maxCounter = counter;
	}
}

int main()
{
	int sizeArray = 0;
	cout << "sizeArray: ";
	cin >> sizeArray;
	int const maxSizeArray = 100;
	int const range = 40;
	int arr[maxSizeArray];
	cout << "generatedArray: ";
	for (int i = 0; i < sizeArray; ++i)
	{
		arr[i] = rand() % range;
		cout << arr[i] << " ";
	}
	cout << endl;
	int leftSide = 0;
	int rightSide = sizeArray - 1;
	quicksort(arr, leftSide, rightSide);
	int result = 0;
	searchAndCount(arr, sizeArray, result);
	cout << "result: " << result;
	system("pause");
	return 0;
}