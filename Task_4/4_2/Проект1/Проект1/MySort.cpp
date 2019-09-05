#include <iostream>
#include "MySort.h" 


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