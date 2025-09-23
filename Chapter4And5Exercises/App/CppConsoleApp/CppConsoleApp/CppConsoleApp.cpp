#include <cmath>
#include <iostream>
using namespace std;
int main() {
    int n;
    do {
        cout << "Nhap n (10 < n < 50): ";
        cin >> n;
    } while ((n <= 10)
        || (n >= 50));
    double S = 0;
    for (int i = 1; i <= n;
        i++) {
        S += 1.0 / sqrt(i * (i + 1));
    }
    cout << "Tong S = " << S << endl;
    // above is exercise 13
    return 0;
}