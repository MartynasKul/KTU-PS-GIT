%%cu

#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <iostream>
#include <fstream>
#include <vector>
#include <sstream>
//kuo skiriasi device nuo global? = device funkcijos naudojasi tik global funkcijoje, kitur ju naudot negalima, jos vykdomos ant GPU, gali kviesti tik kitos funkcijos kurios naudoja GPU
// Device -funkcija kuri gali buti kvieciama ir vykdoma per GPU, negali buti kvieciama is CPU
// global funkcijos gali buti kvieciamos per CPU ir vykdomos tik per GPU

// Define the structure for an entry
struct Entry {
    char word[256];
    int randomiseamount;
    float filterdouble;
};


__device__ bool isAlphabet(char c) {
    return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
}

__device__ bool isUpperCase(char c) {
    return (c >= 'A' && c <= 'Z');
}
// Vykdomas duomenu apdorojimas su globalia funkcija
__global__ void processDataKernel(Entry* entries, Entry* results, int dataSize) {
    int numThreads = blockDim.x * gridDim.x;
    int tid = threadIdx.x + blockIdx.x * blockDim.x;
    int stride = numThreads;

    for (int threadIndex = tid; threadIndex < dataSize; threadIndex += stride) {
        char* word = entries[threadIndex].word;
        int wordLength = 0;

        while (word[wordLength] != '\0') {
            ++wordLength;
        }

        // patikrina ar susiapvalina i didesne puse, jei taip - irasomas irasas.
        if (static_cast<int>(entries[threadIndex].filterdouble + 0.5) > entries[threadIndex].filterdouble){
            for (int i = 0; i < wordLength; ++i) {
                if (isAlphabet(word[i])) {
                    char base = isUpperCase(word[i]) ? 'A' : 'a';
                    results[threadIndex].word[i] = static_cast<char>((static_cast<int>(word[i]) + entries[threadIndex].randomiseamount - static_cast<int>(base)) % 26 + static_cast<int>(base));
                }
            }
        }
    }
}


int main() {
    // Read data from the CSV file
    //std::ifstream inputFile("IFF19KuliesiusMartynas1.csv"); //puse atitinka
    //std::ifstream inputFile("IFF19KuliesiusMartynas2.csv"); //visi neatitinka
    std::ifstream inputFile("IFF19KuliesiusMartynas3.csv"); //visi atitinka
    if (!inputFile.is_open()) {
        std::cerr << "Error opening the input file." << std::endl;
        return 1;
    }

    // Convert CSV data to a vector of entries
    // Skip the header row
    std::string header;
    std::getline(inputFile, header);

    // Convert CSV data to a vector of entries
    std::vector<Entry> entries;
    std::string line;;
    while (std::getline(inputFile, line)) {
        std::istringstream ss(line);
        std::string token;

        Entry entry;

        // Read word
        std::getline(ss, token, ',');
        strncpy(entry.word, token.c_str(), sizeof(entry.word) - 1);
        entry.word[sizeof(entry.word) - 1] = '\0';  // Ensure null-termination

        // Read randomiseamount
        std::getline(ss, token, ',');
        entry.randomiseamount = std::stoi(token);

        // Read filterdouble
        std::getline(ss, token, ',');
        entry.filterdouble = std::stof(token);

        entries.push_back(entry);
    }

    inputFile.close();

    // Determine the size of data
    int dataSize = entries.size();
    if (dataSize == 0) {
        std::cerr << "No data found in the input file." << std::endl;
        return 1;
    }

    // Allocate memory for results on the host
    Entry* results = new Entry[dataSize];


    //Cuda configuration
    int threadsPerBlock = 2;
    int blocksPerGrid = (dataSize + threadsPerBlock) / threadsPerBlock;
    std::cout << "Total number of blocks on the grid: " << blocksPerGrid << std::endl;
    int totalThreads = blocksPerGrid * threadsPerBlock;
    std::cout << "Total number of threads: " << totalThreads << std::endl;
    dim3 blocks(blocksPerGrid, 1, 1);
    dim3 threads(threadsPerBlock, 1, 1);


    // Allocate memory for entries and results on the device
    Entry* d_entries;
    Entry* d_results;
    cudaMalloc((void**)&d_entries, dataSize * sizeof(Entry)); // kodel prie void dvi ** = kad duomenys pointerio grazintusi tarp metodu
    cudaMalloc((void**)&d_results, dataSize * sizeof(Entry));


    // Copy input data from CPU to GPU
    cudaMemcpy(d_entries, entries.data(), dataSize * sizeof(Entry), cudaMemcpyHostToDevice);
    cudaMemcpy(d_results, results, dataSize * sizeof(Entry), cudaMemcpyHostToDevice);


    // Launch the CUDA kernel
    processDataKernel <<<2, 32 >>> (d_entries, d_results, dataSize);

    // Copy results from GPU to CPU
    cudaMemcpy(results, d_results, dataSize * sizeof(Entry), cudaMemcpyDeviceToHost);

    std::ofstream outputFile("output.txt");
    if (!outputFile.is_open()) {
        std::cerr << "Error opening the output file." << std::endl;
        return 1;
    }
    for (int i = 0; i < dataSize; ++i) {
        if (results[i].word[0] != ' ' && results[i].word[0] != '\0')
        {
            outputFile << results[i].word << std::endl;
        }
    }
    // Write results to the file


    // Close the file
    outputFile.close();

    // Atlaisvinam atminti
    delete[] results;
    cudaFree(d_entries);
    cudaFree(d_results); // jei nenaudojam cuda free, alocated atmintis isliks GPU iki kol programa pabaigs darba, taip padidina atminties sunaudojima ir gali paveikti programos veikima (performance)

    return 0;
}



