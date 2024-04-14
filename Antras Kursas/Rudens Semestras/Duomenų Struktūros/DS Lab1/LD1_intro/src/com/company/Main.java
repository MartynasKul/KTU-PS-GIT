package com.company;
import models.Student;
import utils.*;

public class Main {

    public static void main(String[] args) {
        String filePath = "students_data.txt";

        Array<Student> students_arr = new Array<Student>();

        DataReader.readFromFile(filePath, students_arr);

        System.out.println("============Tests with array implementation============");
        performTests(students_arr);

        LinkedList<Student> students_llist = new LinkedList<Student>();

// Realizavus LinkedList klase ir atkomentavus turetume gauti tokius
// pacius rezultatus kaip ir klases Array atveju
        DataReader.readFromFile(filePath, students_llist);

        System.out.println("============Tests with linked list implementation============");
       performTests(students_llist);
    }

    private static void performTests(List<Student> students) {
        System.out.println("Initial students list:");

        for(Student student : students){
            System.out.println(student);
        }


        System.out.println(students.remove(new Student("Tomas", "Tomaitis", 22, "IF-7/1")));
        System.out.println(students.remove(new Student("Jonas", "Jonaitis", 19, "IFF-9/3")));
        System.out.println(students.remove(new Student("Vardenis", "Pavardenis", 99, "XX-XX")));



        System.out.println("\nAfter removal:");

        for(Student student : students){
            System.out.println(student);
        }

        students.add(new Student("Ona", "Onaite", 20,"IFF-8/1"));



        System.out.println("\nAfter addition at the end and insertion at the start:");


        for(Student student : students){
            System.out.println(student);
        }

        System.out.println("\nTesting get() method:");

        for(int i = 0; i < 2; i++){
            System.out.println(students.get(i));
        }

    }
}
