#pragma once

struct Notepad
{
	char name[30]{};
	char numerPhone[12]{};
};

Notepad* editPad(Notepad* Obj, const int amount);
