#include <iostream>

using namespace std;

unsigned long long int recFact(int position)
{
    if (position == 0)
    {
        return 0;
    }
    if (position == 1 || position == 2)
    {
        return 1;
    }
    return (recFact(position - 1) + recFact(position - 2));
}

void decisionOne()
{
    cout << endl << "decisionOne:" << endl;
    int position = 0;
    cout << "input position: ";
    cin >> position;
    cout << "result: " << recFact(position) << endl;
}

unsigned long long int fibonacci(int position)
{
    unsigned long long result = 0;
    unsigned long long assistiveNumberOne = 0;
    unsigned long long assistiveNumberTwo = 0;
    for (int i = 0; i <= position; ++i)
    {
        if (i == 0)
        {
            assistiveNumberOne = i;
            result = assistiveNumberOne;
        }
        if (i == 1)
        {
            assistiveNumberTwo = i;
            result = assistiveNumberTwo;
        }
        if (i >= 2)
        {
            result = assistiveNumberOne + assistiveNumberTwo;
            assistiveNumberOne = assistiveNumberTwo;
            assistiveNumberTwo = result;
        }
    }
    return result;
}

void decisionTwo ()
{
    cout << endl << "decisionTwo:" << endl;
    int position = 0;
    cout << "input position: ";
    cin >> position;
    cout << "result: " << fibonacci(position) << endl;
}

int main()
{
    char number = '0';
    cout << "Task: Fibonacci numbers" << endl << endl;
    cout << "decision 1: recursion" << endl;
    cout << "decision 2: iterative" << endl;
    cout << "(number 3: close program)" << endl << endl;
    cout << "input number decision: ";
    cin >> number;
    while (number != '0')
    {
        switch (number)
        {
            case '1':
            {
                decisionOne ();
                break;
            }
            case '2':
            {
                decisionTwo ();
                break;
            }
        }
        cout << endl << "input number: ";
        cin >> number;
    }
    return 0;
}
