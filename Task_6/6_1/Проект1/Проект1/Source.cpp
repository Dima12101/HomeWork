#include <iostream>
#include "MyList.h"

using namespace std;

void commandOne(headAndTail* border)
{
	cout << "input digit: ";
	double dig;
	cin >> dig;
	// проверка вставки элемента
	bool checkAdd = false;
	if (border->head != nullptr)
	{
		// если список не пуст:

		iter* in = new iter;
		for (in->it = border->head; in->it != nullptr; in->it = step(in->it))
		{
			// ищем первый элемент, больший или равный введённому
			if (dig <= in->it->value)
			{
				// вставляем введённый элемент перед найденным элементом
				pushDig(border, in->it, dig);
				// отмечаем, что элмент вставлен
				checkAdd = true;
				break;
			}
		}
	}
	else
	{
		// если список пуст:

		// помещаем элемент в список 
		newHead(border, dig);
		// отмечаем, что элмент вставлен
		checkAdd = true;
	}
	if (!checkAdd)
	{
		// если вставка не была произведено (введённый элемент больше всех элементов списка)

		// помещаем элемент в конец списка
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
		// если список не пуст:

		// проверка на удаление элемента
		bool checkDel = false;
		iter* in = new iter;
		for (in->it = border->head; in->it != nullptr; in->it = step(in->it))
		{
			//ищем элемент, равный введённому
			if (dig == in->it->value)
			{
				//удаляем найденный элемент
				deleteDig(in->it, border);
				// отмечаем, что элмент был удалён
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
			// если удаление не было произведено (элемент не был найден)
			cout << "info: " << "error" << ", because digit not found" << endl;
		}

	}
	else
	{
		// если удаление не было произведено (список пуст)
		cout << "info: " << "error" << ", because list empty"<< endl;
	}

}

void commandThree(headAndTail* border)
{	
	if (border->head != nullptr)
	{
		// если список не пуст:

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
		// если список пуст:

		cout << "info: " << "list empty" << endl;
	}
}

int main()
{
	// создаём указатели на начало и конец списка
	headAndTail* border = new headAndTail;
	// указатель на начало списка ставим на пустой элемент
	border->head = nullptr;
	// указатель на конец списка ставим на пустой элемент
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
		// направление на исполнение выбранной команды
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