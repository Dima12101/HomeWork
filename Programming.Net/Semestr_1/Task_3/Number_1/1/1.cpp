// 1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <iostream>

using namespace std;

int section(int arr[], int leftSide, int rightSide)
{
	int pivot = arr[leftSide];
	int i = leftSide + 1;
	int j = rightSide;
	for (i; i <= j; ++i)
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
		while (arr[j] < arr[j - 1] && j != leftSide)
		{
			swap(arr[j], arr[j - 1]);
			j--;
		}
	}
}

void quicksort(int arr[], int leftSide, int rightSide)
{
	int s = 0;
	if (leftSide < rightSide)
	{
		if (((rightSide + 1) - leftSide) >= 10)
		{
			s = section(arr, leftSide, rightSide);
			quicksort(arr, leftSide, s - 1);
			quicksort(arr, s + 1, rightSide);
		}
		else sortChoice(arr, leftSide, rightSide);
	}
}

int main()
{
	int sizeArray = 0;
	cout << "input sizeArray: ";
	cin >> sizeArray;
	int const maxSizeArray = 100;
	int arr[maxSizeArray];
	cout << "input Array: ";
	for (int i = 0; i < sizeArray; ++i)
	{
		cin >> arr[i];
	}
	int const leftSide = 0;
	int const rightSide = sizeArray - 1;
	quicksort(arr, leftSide, rightSide);
	cout << "sorted Array: ";
	for (int i = 0; i < sizeArray; ++i)
	{
		cout << arr[i] << " ";
	}
	return 0;
}