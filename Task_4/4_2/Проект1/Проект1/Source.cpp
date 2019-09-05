#include <iostream>
#include <cstdlib>
#include <time.h>

using namespace std;

int section(int arr[], int leftSide, int rightSide)
{
	int pivot = arr[leftSide];
	int i = leftSide + 1;
	int j = rightSide;
	for (; i <= j; ++i)
	{
		if (arr[i] < pivot)
		{
			swap(arr[i], arr[i - 1]);
		}
		else
		{
			while (arr[j] > pivot && j != i)
			{
				j--;
			}
			if (j != i)
			{
				swap(arr[i], arr[j]);
				swap(arr[i], arr[i - 1]);
			}
		}
	}
	if (i == rightSide - 1)
	{
		i++;
	}
	return i - 2;
}

void sortChoice(int arr[], int leftSide, int rightSide)
{
	for (int i = leftSide + 1; i <= rightSide; ++i)
	{
		int j = i;
		while (j != leftSide && arr[j] < arr[j - 1])
		{
			swap(arr[j], arr[j - 1]);
			j--;
		}
	}
}

void quicksort(int arr[], int leftSide, int rightSide)
{
	if (leftSide < rightSide)
	{
		if (((rightSide + 1) - leftSide) >= 10)
		{
			int s = 0;
			s = section(arr, leftSide, rightSide);
			quicksort(arr, leftSide, s - 1);
			quicksort(arr, s + 1, rightSide);
		}
		else
		{
			sortChoice(arr, leftSide, rightSide);
		}
	}
}

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
	return 0;
}