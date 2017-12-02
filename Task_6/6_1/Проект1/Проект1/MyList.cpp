#include <iostream>
#include "MyList.h"

strList* step(strList* it)
{
	// возвращаем указатель на следующий элемент в списке
	return it->next;
}

void pushDig(headAndTail* border, strList* afterIndex, double dig)
{
	// создаём новое поле
	strList* newDig = new strList;

	
	if (afterIndex != border->head)
	{
		// если нужно вставить внутрь списка:

		/* заполняем новое поле и переопределяем указатели prev и next 
		 предыдущего и последующего элемента */

		// ставим указать next прерыдущего элемента на новое поле
		afterIndex->prev->next = newDig;

		// ставим указатель prev нового поля на предыдущий элемент
		newDig->prev = afterIndex->prev;

		// ставим указатель prev последующего элемента на новое поле
		afterIndex->prev = newDig;

		// ставим указатель next нового поля на последующий
		newDig->next = afterIndex;

		// помещаем число в новое поле
		newDig->value = dig;
	}
	else
	{
		// если нужно вставить в начало списка:
		newHead(border, dig);
	}
	
}

void newHead(headAndTail* border, double dig)
{
	// создаём новое поле
	strList* newDig = new strList;

	// проверка на пустоту списка
	if (border->head == nullptr)
	{
		// если список пуст:

		// заполняем новое поле 

		// ставим указатель prev нового поля на пустой элемент
		newDig->prev = nullptr;
		// ставим указатель next нового поля на пустой элемент
		newDig->next = nullptr;
		// помещаем число в новое поле
		newDig->value = dig;

		// указатель на конец ставим на новое поле
		border->tail = newDig;
	}
	else
	{
		// если список не пуст:

		// заполняем новое поле

		// ставим указатель prev нового поля на пустой элемент
		newDig->prev = nullptr;
		// ставим указатель нового поля на последующий элемент
		newDig->next = border->head;
		// помещаем число в новое поле
		newDig->value = dig;

		// ставим указатель последующего элемента на новое поле
		border->head->prev = newDig;
	}

	// указатель на начало ставим на новое поле
	border->head = newDig;

}

void newTail(headAndTail* border, double dig)
{
	// создаём новое поле
	strList* newDig = new strList;

	// заполняем новое поле

	// ставим указатель prev нового поля на предыдущий элемент
	newDig->prev = border->tail;
	// ставим указатель next нового поля на пустой элемент
	newDig->next = nullptr;
	// помещаем число в новое поле
	newDig->value = dig;

	// ставим указатель next предыдущего элемента на новое поле
	border->tail->next = newDig;

	// указатель на конец ставим на новое поле
	border->tail = newDig;
}

void deleteDig(strList* index, headAndTail* border)
{

	if (index != border->head && index != border->tail)
	{
		// если удаляем элемент внутри списка

		// ставим указатель prev последующего элемента на предыдущий элемент
		index->next->prev = index->prev;
		// ставим указатель next предыдущего элемента на последующий элемент
		index->prev->next = index->next;

	}
	else 
	{
	
		if (border->head != border->tail)
		{
			// если в списке больше одного элемента
			if (index == border->head)
			{
				// если удаляем элемент в начале списка

				// ставим указатель prev последующего элемента на пустой элемент
				index->next->prev = nullptr;
				// указатель на начало списка ставим на последующий элемент
				border->head = index->next;
			}
			else
			{
				// если удаляем элмент в конце списка

				// ставим указатель next предыдущего элемента на пустой элемент
				index->prev->next = nullptr;
				// указатель на конец списка ставим на предыдущий элемент
				border->tail = index->prev;
			}
		}
		else
		{
			// если в списке один элемент

			// указатель на начало списка ставим на пустой элемент
			border->tail = nullptr;
			// указатель на конец списка ставим на пустой элемент
			border->head = nullptr;
		}
		
	}
	// удаляем поле
	delete index;
}
