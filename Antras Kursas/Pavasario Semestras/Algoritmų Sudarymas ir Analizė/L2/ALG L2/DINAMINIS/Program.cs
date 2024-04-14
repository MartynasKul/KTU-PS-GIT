using System;

class Program {
  static void Main() {
    int n = 4; // Keiciamas skaicius, kurio kombinacijas norime gauti

    Console.WriteLine($"Skirtingos kombinacijos padaryti {n} su 1, 3, 4:");

    int[] arr = new int[n + 1];
    arr[0] = 1;

    for (int i = 1; i <= n; i++) {
      if (i >= 1)
        arr[i] += arr[i - 1];
      if (i >= 3)
        arr[i] += arr[i - 3];
      if (i >= 4)
        arr[i] += arr[i - 4];
    }

    for (int i = 1; i <= n; i++) {
      Console.Write($"{i}=");
      int count = 0;
      if (i >= 1) {
        Console.Write("1");
        count++;
      }
      if (i >= 3) {
        Console.Write($"{(count > 0 ? "+" : "")}3");
        count++;
      }
      if (i >= 4) {
        Console.Write($"{(count > 0 ? "+" : "")}4");
        count++;
      }
      Console.WriteLine();
    }
  }
}
