#include <iostream>
#include "MyList.h"

using namespace std;

void commandOne(headAndTail* border)
{
	cout << "input digit: ";
	double dig;
	cin >> dig;
	// �������� ������� ��������
	bool checkAdd = false;
	if (border->head != nullptr)
	{
		// ���� ������ �� ����:

		iter* in = new iter;
		for (in->it = border->head; in->it != nullptr; in->it = step(in->it))
		{
			// ���� ������ �������, ������� ��� ������ ���������
			if (dig <= in->it->value)
			{
				// ��������� �������� ������� ����� ��������� ���������
				pushDig(border, in->it, dig);
				// ��������, ��� ������ ��������
				checkAdd = true;
				break;
			}
		}
	}
	else
	{
		// ���� ������ ����:

		// �������� ������� � ������ 
		newHead(border, dig);
		// ��������, ��� ������ ��������
		checkAdd = true;
	}
	if (!checkAdd)
	{
		// ���� ������� �� ���� ����������� (�������� ������� ������ ���� ��������� ������)

		// �������� ������� � ����� ������
		newTail(border, dig);
	}
	cout << "info: " << dig << " - add in the list" << endl;
}

void commandTwo(headAndTail* border)
{
	cout << "input digit: ";
	double dig;
	cin >> dig;
	if (border->head != nullptr)
	{
		// ���� ������ �� ����:

		// �������� �� �������� ��������
		bool checkDel = false;
		iter* in = new iter;
		for (in->it = border->head; in->it != nullptr; in->it = step(in->it))
		{
			//���� �������, ������ ���������
			if (dig == in->it->value)
			{
				//������� ��������� �������
				deleteDig(in->it, border);
				// ��������, ��� ������ ��� �����
				checkDel = true;
				break;
			}
		}
		if (checkDel)
		{
			cout << "info: " << dig << " - delete in the list" << endl;
		}
		else
		{
			// ���� �������� �� ���� ����������� (������� �� ��� ������)
			cout << "info: " << "error" << ", because digit not found" << endl;
		}

	}
	else
	{
		// ���� �������� �� ���� ����������� (������ ����)
		cout << "info: " << "error" << ", because list empty"<< endl;
	}

}

void commandThree(headAndTail* border)
{	
	if (border->head != nullptr)
	{
		// ���� ������ �� ����:

		iter* in = new iter;
		cout << "info: ";
		for (in->it = border->head; in->it != nullptr; in->it = step(in->it))
		{
			cout << in->it->value << " ";
		}
		cout << endl;
	}
	else 
	{
		// ���� ������ ����:

		cout << "info: " << "list empty" << endl;
	}
}

int main()
{
	// ������ ��������� �� ������ � ����� ������
	headAndTail* border = new headAndTail;
	// ��������� �� ������ ������ ������ �� ������ �������
	border->head = nullptr;
	// ��������� �� ����� ������ ������ �� ������ �������
	border->tail = nullptr;

	cout << "list commands: " << endl << endl;
	{
		cout << "0: close the program" << endl;
		cout << "1: push digit in the list" << endl;
		cout << "2: delete digit in the list" << endl;
		cout << "3: print list" << endl << endl;
	}
	char command = '0';
	cout << "input command: ";
	cin >> command;
	while (command != '0')
	{
		// ����������� �� ���������� ��������� �������
		switch (command)
		{
			case '1':
			{
				commandOne(border);
				break;
			}
			case '2':
			{
				commandTwo(border);
				break;
			}
			case '3':
			{
				commandThree(border);
				break;
			}
		}
		cout << endl << "input command: ";
		cin >> command;
	}

}