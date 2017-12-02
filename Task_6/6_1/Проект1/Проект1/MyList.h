#pragma once

// ������� ������
struct strList
{
	// ������ ��� �����
	double value;
	// ������ ��� ��������� �� ���������� �������
	strList* prev;
	// ������ ��� ��������� �� ����������� �������
	strList* next;
};

// ��������� �� ������ � ����� ������
struct headAndTail
{
	// ������ ��� ��������� �� ������ ������
	strList* head;
	// ������ ��� ��������� �� ����� ������
	strList* tail;
};

// �������� ��� �������� �� ������
struct iter
{
	// ������ ��� ��������� �� ����
	strList* it;
};

// �������, ����������� �������� ������� � ������
void pushDig(headAndTail* border, strList* afterIndex, double dig);

// �������, ��������� ������� � ������ ������
void newHead(headAndTail* border, double dig);

// �������, ��������� ������� � ����� ������
void newTail(headAndTail* border, double dig);

// �������, ����������� ������� ������� �� �������
void deleteDig(strList* index, headAndTail* border);

// �������, ��������������� ��������� �� ��������� ������� � ������
strList* step(strList* in);

