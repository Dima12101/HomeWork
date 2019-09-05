#include <iostream>
#include "Work.h"


Notepad* editPad(Notepad* Obj, const int amount)
{
	Notepad* tempObj = new Notepad[amount + 1];
	for (int i = 0; i < amount; i++)
	{
		tempObj[i] = Obj[i]; // копируем во временный объект
	}
	delete[] Obj;
	Obj = tempObj;
	return Obj;
}


