#include <iostream>
using namespace std;

int fun(int count)
{
    if(count > 0)
    {
      cout<<fun(count--);
    }
    cout<<endl;
}

int main()
{
    int myCount = 5;
    fun(myCount);
    return 0;
}

