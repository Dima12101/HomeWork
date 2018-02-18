#include <iostream>

using namespace std;

int main()
{
    string S, S1;
     cout << "Stroka S: ";
      cin >> S;
     cout << "Stroka S1: ";
      cin >> S1;
    int kol = 0;
     if (S.size() >= S1.size()){
      for (int i = 0; i <= (S.size()-S1.size()); i++){
        for (int j = 0; j < S1.size(); j++){
            if (S1[j] != S[i+j])break;
            if (j == S1.size()-1)kol++;
        }
      }
     }
     cout << "kol. vhozdeniy S1 v S: " << kol;
    return 0;
}
