//Martynas Kuliešius IFF-1/9
#include <string>
#include <iostream>
#include <omp.h>
#include <vector>

using namespace std;

double execute(double a, double b);

int main() {
    double Sum = 0;
    const int size = 100;
    double filler = 0;
    double ArrayA[size] = {};
    double ArrayB[size] = {};


    for (int i = 0; i < size; i++) {
        ArrayA[i] = i + 1;
    }
    for (int i = 0; i < size; i++) {
        ArrayB[i] = filler;
        filler += 0.1;
    }

    
    omp_set_num_threads(20);
#pragma omp parallel shared(thread_names) default(none)
    {
        auto threadNumber = omp_get_thread_num();
      
#pragma omp critical
        {
            for (int i = 0; i < size; i++) 
            {
                Sum+=execute(ArrayA[i], ArrayB[i]);
            
            }
        }
    }
    cout << "Program finished execution, Suma: " << Sum << endl;
}

double execute(double a, double b) {
    cout << a * b << endl;
    return a * b;
}

