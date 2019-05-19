// 2.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
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

void searchDigit(int arr[], int digit, int leftSide, int rightSide, bool &check)
{
	int middle = (leftSide + rightSide) / 2;
	if (arr[middle] == digit)
	{
		check = true;
	}
	else if (abs(leftSide - rightSide) == 1)
	{
		if (arr[leftSide] == digit || arr[rightSide] == digit)
		{
			check = true;
		}
	}
	else
	{
		if (arr[middle] > digit)
		{
			rightSide = middle;
			searchDigit(arr, digit, leftSide, rightSide, check);
		}
		if (arr[middle] < digit)
		{
			leftSide = middle;
			searchDigit(arr, digit, leftSide, rightSide, check);
		}
	}	
}
int main()
{
	int n = 0;
	cout << "input sizeArray: ";
	cin >> n;
	int const scale = 1000000000;
	int const maxSizeArray = 5000;
	int arr[maxSizeArray];
	srand(time(nullptr));
	cout << "generatedArray: ";
	for (int i = 0; i < n; ++i)
	{
		arr[i] = (int)((double)rand() / RAND_MAX * scale);
		cout << arr[i] << " ";
	}
	cout << endl;
	int leftSide = 0;
	int rightSide = n - 1;
	quicksort(arr, leftSide, rightSide);

	cout << "input k: ";
	int k = 0;
	cin >> k;
	int digit = 0;
	cout << "generatedDigit: ";
	for (int i = 0; i < k; ++i)
	{
		bool check = false;
		digit = (int)((double)rand() / RAND_MAX * scale);

		cout << digit << ": ";
		searchDigit(arr, digit, leftSide, rightSide, check);
		if (check)
		{
			cout << "found";
		}
		else
		{
			cout << "not found";
		}
		cout << endl;
	}
	return 0;
}

