#pragma once

// элемент списка
struct strList
{
	// €чейка дл€ числа
	double value;
	// €чейка дл€ указател€ на предыдущий элемент
	strList* prev;
	// €чейка дл€ указател€ на последующий элемент
	strList* next;
};

// указатели на начало и конец списка
struct headAndTail
{
	// €чейка дл€ указател€ на начало списка
	strList* head;
	// €чейка дл€ указател€ на конец списка
	strList* tail;
};

// итератор дл€ хождени€ по списку
struct iter
{
	// €чейка дл€ указател€ на поле
	strList* it;
};

// функци€, позвол€юща€ положить элемент в список
void pushDig(headAndTail* border, strList* afterIndex, double dig);

// функци€, кладующа€ элемент в начало списка
void newHead(headAndTail* border, double dig);

// функци€, кладующа€ элемент в конец списка
void newTail(headAndTail* border, double dig);

// функци€, позвол€юща€ удалить элемент из списока
void deleteDig(strList* index, headAndTail* border);

// функци€, перестраивающа€ интератор на следующий элемент в списке
strList* step(strList* in);

