#include <iostream>
#include <fstream>
#include <clocale>
#include "Work.h"

using namespace std;
void listComand()
{
	cout << "The list of commands:" << endl;
	{
		cout << "0 - to close the program" << endl;
		cout << "1 - add entry(name and phone number)" << endl;
		cout << "2 - to print all records" << endl;
		cout << "3 - find phone by name" << endl;
		cout << "4 - find name by phone" << endl;
		cout << "5 - save records to file" << endl;
	}
}
void execution1(Notepad* &input, int &sizePad)
{
	cout << "input name: ";
	cin >> input[sizePad - 1].name;
	cout << "input phone number: ";
	cin >> input[sizePad - 1].numerPhone;
	input = editPad(input, sizePad);
	sizePad++;
}

void execution2(Notepad* input, int sizePad)
{
	for (int i = 0; i < sizePad - 1; ++i)
	{
		cout << input[i].name << " " << input[i].numerPhone;
		cout << endl;
	}
}

void execution3(Notepad* input, int sizePad)
{
	char keyName[30]{};
	cout << "input name: ";
	cin >> keyName;
	int index = 0;
	bool check = true;
	for (int i = 0; i < sizePad - 1; ++i)
	{
		check = true;
		for (int j = 0; j < 30; ++j)
		{
			if (keyName[j] != input[i].name[j])
			{
				check = false;
				break;
			}
		}
		if (check)
		{
			index = i;
			break;
		}
	}
	if (check)
	{
		cout << "search result: " << input[index].numerPhone;
	}
	else
	{
		cout << "search result: error";
	}
	cout << endl;
}

void execution4(Notepad* input, int sizePad)
{
	char keyPhone[12]{};
	cout << "input phone number: ";
	cin >> keyPhone;
	int index = 0;
	bool check = true;
	for (int i = 0; i < sizePad - 1; ++i)
	{
		check = true;
		for (int j = 0; j < 12; ++j)
		{
			if (keyPhone[j] != input[i].numerPhone[j])
			{
				check = false;
				break;
			}
		}
		if (check)
		{
			index = i;
			break;
		}
	}
	if (check)
	{
		cout << "search result: " << input[index].name;
	}
	else
	{
		cout << "search result: error";
	}
	cout << endl;
}

void execution5(Notepad* input, int sizePad)
{
	ofstream fout("output.txt");
	for (int i = 0; i < sizePad - 1; ++i)
	{
		fout << input[i].name << " " << input[i].numerPhone;
		fout << endl;
	}
	fout.close();
}

int main()
{
	ifstream fin("input.txt");
	Notepad* input = new Notepad[1];
	int sizePad = 1;
	while (fin >> input[sizePad - 1].name >> input[sizePad - 1].numerPhone)
	{
		input = editPad(input, sizePad);
		sizePad++;
	}
	fin.close();
	listComand();
	char command = '0';
	cout << endl << "Input command: ";
	cin >> command;
	while (command != '0')
	{
		switch (command)
		{
			case '1':
			{
				execution1(input, sizePad);
				break;
			}
			case '2':
			{
				execution2(input, sizePad);
				break;
			}
			case '3':
			{
				execution3(input, sizePad);
				break;
			}
			case '4':
			{
				execution4(input, sizePad);
				break;
			}
			case '5':
			{
				execution5(input, sizePad);
				break;
			}
		}
		system("pause");
		system("cls");
		listComand();
		cout << endl << "Input command: ";
		cin >> command;
	}

	
	ofstream out("output.txt");
	

}