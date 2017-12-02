#include <iostream>
#include "MyList.h"

strList* step(strList* it)
{
	// ���������� ��������� �� ��������� ������� � ������
	return it->next;
}

void pushDig(headAndTail* border, strList* afterIndex, double dig)
{
	// ������ ����� ����
	strList* newDig = new strList;

	
	if (afterIndex != border->head)
	{
		// ���� ����� �������� ������ ������:

		/* ��������� ����� ���� � �������������� ��������� prev � next 
		 ����������� � ������������ �������� */

		// ������ ������� next ����������� �������� �� ����� ����
		afterIndex->prev->next = newDig;

		// ������ ��������� prev ������ ���� �� ���������� �������
		newDig->prev = afterIndex->prev;

		// ������ ��������� prev ������������ �������� �� ����� ����
		afterIndex->prev = newDig;

		// ������ ��������� next ������ ���� �� �����������
		newDig->next = afterIndex;

		// �������� ����� � ����� ����
		newDig->value = dig;
	}
	else
	{
		// ���� ����� �������� � ������ ������:
		newHead(border, dig);
	}
	
}

void newHead(headAndTail* border, double dig)
{
	// ������ ����� ����
	strList* newDig = new strList;

	// �������� �� ������� ������
	if (border->head == nullptr)
	{
		// ���� ������ ����:

		// ��������� ����� ���� 

		// ������ ��������� prev ������ ���� �� ������ �������
		newDig->prev = nullptr;
		// ������ ��������� next ������ ���� �� ������ �������
		newDig->next = nullptr;
		// �������� ����� � ����� ����
		newDig->value = dig;

		// ��������� �� ����� ������ �� ����� ����
		border->tail = newDig;
	}
	else
	{
		// ���� ������ �� ����:

		// ��������� ����� ����

		// ������ ��������� prev ������ ���� �� ������ �������
		newDig->prev = nullptr;
		// ������ ��������� ������ ���� �� ����������� �������
		newDig->next = border->head;
		// �������� ����� � ����� ����
		newDig->value = dig;

		// ������ ��������� ������������ �������� �� ����� ����
		border->head->prev = newDig;
	}

	// ��������� �� ������ ������ �� ����� ����
	border->head = newDig;

}

void newTail(headAndTail* border, double dig)
{
	// ������ ����� ����
	strList* newDig = new strList;

	// ��������� ����� ����

	// ������ ��������� prev ������ ���� �� ���������� �������
	newDig->prev = border->tail;
	// ������ ��������� next ������ ���� �� ������ �������
	newDig->next = nullptr;
	// �������� ����� � ����� ����
	newDig->value = dig;

	// ������ ��������� next ����������� �������� �� ����� ����
	border->tail->next = newDig;

	// ��������� �� ����� ������ �� ����� ����
	border->tail = newDig;
}

void deleteDig(strList* index, headAndTail* border)
{

	if (index != border->head && index != border->tail)
	{
		// ���� ������� ������� ������ ������

		// ������ ��������� prev ������������ �������� �� ���������� �������
		index->next->prev = index->prev;
		// ������ ��������� next ����������� �������� �� ����������� �������
		index->prev->next = index->next;

	}
	else 
	{
	
		if (border->head != border->tail)
		{
			// ���� � ������ ������ ������ ��������
			if (index == border->head)
			{
				// ���� ������� ������� � ������ ������

				// ������ ��������� prev ������������ �������� �� ������ �������
				index->next->prev = nullptr;
				// ��������� �� ������ ������ ������ �� ����������� �������
				border->head = index->next;
			}
			else
			{
				// ���� ������� ������ � ����� ������

				// ������ ��������� next ����������� �������� �� ������ �������
				index->prev->next = nullptr;
				// ��������� �� ����� ������ ������ �� ���������� �������
				border->tail = index->prev;
			}
		}
		else
		{
			// ���� � ������ ���� �������

			// ��������� �� ������ ������ ������ �� ������ �������
			border->tail = nullptr;
			// ��������� �� ����� ������ ������ �� ������ �������
			border->head = nullptr;
		}
		
	}
	// ������� ����
	delete index;
}
