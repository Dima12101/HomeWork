#include <iostream>
#include <string>


using namespace std;

int main()
{
    string skobky;
     cout << "Vvod skobok: ";
      cin >> skobky;
    int ballans = 0;
    bool uslovie = true;
     for (int i = 0; i < skobky.size(); i++){
        if (skobky[i] == '(')ballans++;
        if (skobky[i] == ')')ballans--;
         if (ballans < 0)uslovie = false;
     }
     if (ballans == 0){
        cout << "Ballans: True" << endl;
        if (uslovie == true){
              cout << "Uslovie: True";
        }else cout << "Uslovie: False";
     }else cout << "Ballans: False";
    return 0;
}
