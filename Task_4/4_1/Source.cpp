#include <iostream>
#include <clocale>
#include <string>

using namespace std;

int calculationBit(int reg)
{
	int maxBit = 1;
	for (int i = 1; i < reg; ++i)
	{
		maxBit *= 2;
	}
	return maxBit;
}

string invertDigitToBinary(int dig, int reg)
{
	int maxBit = calculationBit(reg);
	string binaryCod;
	if (dig <= 0)
	{
		dig = -dig;
		dig = (maxBit * 2) - dig;
	}
	while (maxBit != 0)
	{
		if ((dig & maxBit) == 0)
		{
			binaryCod += '0';
		}
		else
		{
			binaryCod += '1';
		}
		maxBit /= 2;
	}
	return binaryCod;
}

int invertDigitToDecimal(string binaryCod, int sign)
{
	int decimalCod = 0;
	int bit = 1;
	for (int i = binaryCod.size() - 1; i >= 0; --i)
	{
		if (binaryCod[i] == '1')
		{
			decimalCod += bit;
		}
		bit *= 2;
	}
	if (sign == 1)
	{
		decimalCod = bit - decimalCod;
		decimalCod = -decimalCod;
	}
	return decimalCod;
}

string sumDigits(int dig1, int dig2, int reg)
{
	string binaryCod1 = invertDigitToBinary(dig1, reg);
	string binaryCod2 = invertDigitToBinary(dig2, reg);
	string sum(reg, '0');
	int transfer = 0;
	for (int i = reg - 1; i >= 0; --i)
	{
		int sumi = (binaryCod1[i] - '0') + (binaryCod2[i] - '0') + transfer;
		if (sumi >= 2)
		{
			sum[i] = (sumi % 2 + '0');
			transfer = sumi / 2;
		}
		else
		{
			sum[i] = (sumi + '0');
			transfer = 0;
		}
	}
	return sum;
}

int resultSign(int dig1, int dig2)
{
	int sign;
	if ((dig1 < 0 && dig2 < 0) || (dig1 < 0 && -dig1 > dig2) || (dig2 < 0 && -dig2 > dig1))
	{
		sign = 1;
	}
	else
	{
		sign = 0;
	}
	return sign;
}

int main()
{
	setlocale(LC_ALL, "Russian");
	cout << "Входные данные:" << endl;
	cout << "Разрядность регистра: ";
	int reg = 0;
	cin >> reg;
	cout << "Ввод первого числа: ";
	int dig1 = 0;
	cin >> dig1;
	cout << "Ввод второго числа: ";
	int dig2 = 0;
	cin >> dig2;
	cout << "__________________________________________" << endl;
	cout << "Двочное представление введённых чисел" << endl;
	cout << dig1 << " : " << invertDigitToBinary(dig1, reg) << endl;
	cout << dig2 << " : " << invertDigitToBinary(dig2, reg) << endl << endl;
	cout << "Сумма чисел" << endl;
	string sum = sumDigits(dig1, dig2, reg);
	cout << "Двоичное представление: " << sum << endl;
	int sign = resultSign(dig1, dig2);
	cout << "Десятичное представление: " << invertDigitToDecimal(sum, sign);
} 
