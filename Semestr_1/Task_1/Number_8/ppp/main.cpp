#include <iostream>

using namespace std;

int main()
{
    int n = 0;
     cout << "Dlina massiva: ";
      cin >> n;
    int al = 0; //������� �������
    int kol = 0; //���-�� ������� ���������
     cout << "Vvod massiva: ";
     for (int i = 0; i < n; i++){
        cin >> al;
        if (al == 0)kol++;
     }
     cout << "Otvet: " << kol;
    return 0;
}
