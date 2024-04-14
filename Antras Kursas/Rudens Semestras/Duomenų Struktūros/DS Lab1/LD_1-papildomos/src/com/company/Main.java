package com.company;


import java.util.ArrayList;
import java.util.Arrays;
import java.util.Locale;

public class Main {

    public static void main(String[] args) {
        System.out.println("==========================");
        System.out.println("Is balanced test");
        System.out.println("==========================");
        System.out.println("}" + " " + Test.isBalanced("}"));
        System.out.println("{()}{[]}" + " " + Test.isBalanced("{()}{[]}"));
        System.out.println("{{()}{[]}" + " " + Test.isBalanced("{{()}{[]}"));
        System.out.println("[{}}" + " " + Test.isBalanced("[{}}"));
        System.out.println("{()[{}]}{}" + " " + Test.isBalanced("{()[{}]}{}"));
        System.out.println("{(})" + " " + Test.isBalanced("{(})"));
        System.out.println("([(]{)})" + " " + Test.isBalanced("([(]{)})"));
        System.out.println("((" + " " + Test.isBalanced("(("));


        System.out.println("==========================");
        System.out.println("Max values test");
        System.out.println("==========================");

        ArrayList<Integer> arrayList = new ArrayList<>(Arrays.asList(2, 7, 3, 1, 5, 2, 6, 2));
        //ArrayList<Integer> arrayListNew= new ArrayList<>(Arrays.asList(10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));
        ArrayList<Integer> arrayListNew= new ArrayList<>(Arrays.asList(1, 2, 3, 4, 5, 4, 3, 2, 1, 6));

        //System.out.println(arrayListNew.size());

        System.out.println(Test.findMax(arrayListNew, 3));
        System.out.println(Test.findMax(arrayList, 4));
        System.out.println(Test.findMax(arrayList, 1));
        System.out.println(Test.findMax(arrayList, 8));
        System.out.println("==========================");

    }

}
